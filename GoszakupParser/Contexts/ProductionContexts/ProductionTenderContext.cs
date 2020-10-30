using Microsoft.EntityFrameworkCore;
using GoszakupParser.Models.ProductionModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Contexts.ProductionContexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Tender Production DB context
    /// </summary>
    public class ProductionTenderContext : DbContext
    {
        /// <summary>
        /// Announcements table models
        /// </summary>
        public DbSet<AdataAnnouncementWeb> Announcements { get; set; }
        
        /// <summary>
        /// Announcements documentations table models
        /// </summary>
        public DbSet<AnnouncementDocumentationWeb> AnnouncementDocumentations { get; set; }
        
        /// <summary>
        /// Lots table models
        /// </summary>
        public DbSet<AdataLotWeb> Lots { get; set; }
        
        /// <summary>
        /// Lot documentations table models
        /// </summary>
        public DbSet<LotDocumentationWeb> LotDocumentations { get; set; }
        
        /// <inheritdoc />
        protected internal ProductionTenderContext()
        {
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                $"{Configuration.ProductionDbConnectionString}" +
                $"Application Name={Configuration.Title};");
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnouncementDocumentationWeb>()
                .HasOne(x => x.Announcement)
                .WithMany()
                .HasForeignKey(x => x.AnnouncementId)
                .HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<LotDocumentationWeb>()
                .HasOne(x => x.Lot)
                .WithMany()
                .HasForeignKey(x => x.LotId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}