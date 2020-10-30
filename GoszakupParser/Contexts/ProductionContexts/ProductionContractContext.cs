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
    /// Contract Production DB context
    /// </summary>
    public class ProductionContractContext : DbContext
    {
        /// <summary>
        /// Contracts table models
        /// </summary>
        public DbSet<AdataContractWeb> Contracts { get; set; }
        
        /// <inheritdoc />
        protected internal ProductionContractContext()
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
        }
    }
}