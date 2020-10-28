using GoszakupParser.Models.ParsingModels;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Contexts.ParsingContexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Tender Parsing DB context - used for parsing tenders
    /// </summary>
    public class ParsingTenderContext : DbContext
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
        protected internal ParsingTenderContext()
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