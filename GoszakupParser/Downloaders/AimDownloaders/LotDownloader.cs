using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts.ParsingContexts;
using GoszakupParser.Contexts.ProductionContexts;
using GoszakupParser.Models.ProductionModels;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Downloaders.AimDownloaders
{
    public class LotDownloader : AimDownloader<LotDocumentationWeb>
    {
        /// <inheritdoc />
        public LotDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
        }

        /// <inheritdoc />
        protected override List<LotDocumentationWeb> LoadAims()
        {
            using var tenderContext = new ProductionTenderContext();
            tenderContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var aims = tenderContext.LotDocumentations
                .Include(x => x.Lot)
                .Where(x => x.Lot.SourceId == 2 && x.Location == null).ToList();
            return aims;
        }

        /// <inheritdoc />
        protected override string GenerateFileName(LotDocumentationWeb aim)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        protected override string GetLink(LotDocumentationWeb aim)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        protected override Task SavePathToDb(LotDocumentationWeb aim)
        {
            throw new System.NotImplementedException();
        }
    }
}