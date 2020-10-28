using GoszakupParser.Models.ParsingModels;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Contexts.ParsingContexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 10:15:03
    /// <summary>
    /// Contract Parsing DB context - used for parsing tenders
    /// </summary>
    public class ParsingContractContext : DbContext
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
        protected internal ParsingContractContext()
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