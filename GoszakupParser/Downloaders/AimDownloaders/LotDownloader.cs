using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GoszakupParser.Contexts.ProductionContexts;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace GoszakupParser.Downloaders.AimDownloaders
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.10.2020 16:53:43
    /// <summary>
    /// Downloader for announcement files
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class LotDownloader : AimDownloader
    {
        /// <inheritdoc />
        public LotDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
        }

        /// <inheritdoc />
        protected override List<DownloadAim> LoadAims()
        {
            using var tenderContext = new ProductionTenderContext();
            tenderContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var aims = tenderContext.LotDocumentations
                .Include(x => x.Lot)
                .Where(x => x.Lot.SourceId == 2 && x.SourceLink != null && x.Location == null)
                .Take(100000)
                .Select(x => new DownloadAim {Link = x.SourceLink, Name = x.Name})
                .Distinct()
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
            await using var tenderContext = new ProductionTenderContext();
            var location = $"goszakup/lots/{GenerateFileName(aim)}";
            var similarAims = tenderContext.LotDocumentations.Where(x => x.SourceLink == aim.Link);

            foreach (var lotDocumentationWeb in similarAims)
                lotDocumentationWeb.Location = location;

            tenderContext.LotDocumentations.UpdateRange(similarAims);
            await tenderContext.SaveChangesAsync();
        }
    }
}