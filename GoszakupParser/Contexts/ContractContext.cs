using Microsoft.EntityFrameworkCore;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 10:15:03
    /// <summary>
    /// Contract DB context - used for parsing tenders
    /// </summary>
    public class ContractContext : DbContext
    {
        /// <summary>
        /// Contract table models
        /// </summary>
        public DbSet<ContractGoszakup> ContractsGoszakup { get; set; }
        
        /// <summary>
        /// Contract units table models
        /// </summary>
        public DbSet<ContractUnitGoszakup> ContractUnitsGoszakup { get; set; }
        
        /// <summary>
        /// Plan table models
        /// </summary>
        public DbSet<PlanGoszakup> PlanGoszakup { get; set; }

        /// <inheritdoc />
        protected internal ContractContext()
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