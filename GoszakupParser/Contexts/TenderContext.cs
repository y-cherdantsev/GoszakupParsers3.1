using Microsoft.EntityFrameworkCore;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Tender DB context - used for parsing tenders
    /// </summary>
    public class TenderContext : DbContext
    {
        /// <summary>
        /// Announcements table models
        /// </summary>
        public DbSet<AnnouncementGoszakup> AnnouncementsGoszakup { get; set; }

        /// <summary>
        /// Lots table models
        /// </summary>
        public DbSet<LotGoszakup> LotsGoszakup { get; set; }

        /// <summary>
        /// Announcement files table models
        /// </summary>
        public DbSet<AnnouncementFileGoszakup> AnnouncementFilesGoszakups { get; set; }

        /// <summary>
        /// Lot files table models
        /// </summary>
        public DbSet<LotFileGoszakup> LotFilesGoszakup { get; set; }

        /// <inheritdoc />
        protected internal TenderContext()
        {
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                $"{Configuration.ParsingDbConnectionString}" +
                $"Application Name={Configuration.Title};");
        }
    }
}