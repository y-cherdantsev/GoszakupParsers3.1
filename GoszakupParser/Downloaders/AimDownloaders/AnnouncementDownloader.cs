using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GoszakupParser.Contexts.ProductionContexts;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace GoszakupParser.Downloaders.AimDownloaders
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class AnnouncementDownloader : AimDownloader
    {
        /// <inheritdoc />
        public AnnouncementDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
        }

        /// <inheritdoc />
        protected override List<DownloadAim> LoadAims()
        {
            using var tenderContext = new ProductionTenderContext();
            tenderContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var aims = tenderContext.AnnouncementDocumentations
                .Include(x => x.Announcement)
                .Where(x => x.Announcement.SourceId == 2 && x.SourceLink != null && x.Location == null)
                .Select(x => new DownloadAim {Link = x.SourceLink, Name = x.Name})
                .Distinct()
                .ToList();
            return aims;
        }

        /// <inheritdoc />
        protected override string GenerateFileName(DownloadAim aim)
        {
            var splitted = aim.Link.TrimEnd('/').Split("/");
            return splitted.Last().Contains(".") ? aim.Name : $"{splitted.Last()}_{aim.Name}";
        }

        /// <inheritdoc />
        protected override async Task SavePathToDb(DownloadAim aim)
        {
            await using var tenderContext = new ProductionTenderContext();
            var location = $"goszakup/announcements/{GenerateFileName(aim)}";
            var similarAims = tenderContext.AnnouncementDocumentations.Where(x => x.SourceLink == aim.Link);

            foreach (var announcementDocumentationWeb in similarAims)
                announcementDocumentationWeb.Location = location;

            tenderContext.AnnouncementDocumentations.UpdateRange(similarAims);
            await tenderContext.SaveChangesAsync();
        }
    }
}