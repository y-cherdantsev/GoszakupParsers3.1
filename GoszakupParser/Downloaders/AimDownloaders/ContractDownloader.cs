using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts.ProductionContexts;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Downloaders.AimDownloaders
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.10.2020 16:53:43
    /// <summary>
    /// Downloader for contract files
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class ContractDownloader : AimDownloader
    {
        /// <inheritdoc />
        public ContractDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
        }

        /// <inheritdoc />
        protected override List<DownloadAim> LoadAims()
        {
            using var contractContext = new ProductionContractContext();
            contractContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var aims = contractContext.Contracts
                .Where(x => x.SourceId == 2 && x.DocLocation == null && x.DocLink != null)
                .Take(300000)
                .Select(x => new DownloadAim {Id = x.Id, Link = x.DocLink, Name = x.DocName})
                .ToList();
            return aims;
        }

        /// <inheritdoc />
        protected override string GenerateFileName(DownloadAim aim)
        {
            return $"{aim.Link.TrimEnd('/').Split("/").Last()}_{aim.Name}";
        }

        /// <inheritdoc />
        protected override async Task SavePathToDb(DownloadAim aim)
        {
            await using var contractContext = new ProductionContractContext();
            contractContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var location = $"goszakup/contracts/{GenerateFileName(aim)}";
            await contractContext.Database.ExecuteSqlRawAsync($"UPDATE adata_tender.contracts SET doc_location = '{location}' WHERE id = '{aim.Id}'");
            await contractContext.SaveChangesAsync();
        }
    }
}