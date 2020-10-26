using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static GoszakupParser.Configuration;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 10:15:03
    /// <summary>
    /// Contract DB context - used for parsing tenders
    /// </summary>
    public class ContractContext : DbContext
    {
        private DbConnectionCredential ConnectionCredentials { get; set; }

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
            ConnectionCredentials =
                DbConnectionCredentialsStatic.FirstOrDefault(x =>
                    x.Title == connectionTitle);
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionCredentials != null)
                optionsBuilder.UseNpgsql(
                    $"Server = {ConnectionCredentials.Address}; " +
                    $"Database = {ConnectionCredentials.Name}; Port={ConnectionCredentials.Port}; " +
                    $"User ID = {ConnectionCredentials.Username}; " +
                    $"Password = {ConnectionCredentials.Password}; " +
                    $"Search Path = {ConnectionCredentials.SearchPath}; " +
                    "Integrated Security=true; " +
                    "Pooling=true; " +
                    $"Application Name={Title};");
            else
                throw new NullReferenceException("Cannot find such connection credentials");
        }
    }
}