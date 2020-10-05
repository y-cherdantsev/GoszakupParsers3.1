using System;
using System.Linq;
using GoszakupParser.Models.ParsingModels;
using Microsoft.EntityFrameworkCore;
using static GoszakupParser.Configuration;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable IdentifierTypo
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
        private DbConnectionCredential connectionCredentials { get; set; }

        public DbSet<ContractGoszakup> ContractsGoszakup { get; set; }
        public DbSet<ContractUnitGoszakup> ContractUnitsGoszakup { get; set; }
        public DbSet<PlanGoszakup> PlanGoszakup { get; set; }

        /// <inheritdoc />
        // ReSharper disable once UnusedMember.Global
        protected ContractContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public ContractContext(DatabaseConnections databaseConnections)
        {
            var connectionTitle = Enum.GetName(typeof(DatabaseConnections), databaseConnections);
            connectionCredentials =
                DbConnectionCredentialsStatic.FirstOrDefault(x =>
                    x.Title == connectionTitle);
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionCredentials != null)
                optionsBuilder.UseNpgsql(
                    $"Server = {connectionCredentials.Address}; " +
                    $"Database = {connectionCredentials.Name}; Port={connectionCredentials.Port}; " +
                    $"User ID = {connectionCredentials.Username}; " +
                    $"Password = {connectionCredentials.Password}; " +
                    $"Search Path = {connectionCredentials.SearchPath}; " +
                    $"Integrated Security=true; " +
                    $"Pooling=true; " +
                    $"Application Name={Title};");
            else
                throw new NullReferenceException("Cannot find such connection credentials");
        }
    }
}