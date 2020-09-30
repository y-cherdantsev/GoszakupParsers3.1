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
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Tender DB context - used for parsing tenders
    /// </summary>
    public class TenderContext : DbContext
    {
        private DbConnectionCredential connectionCredentials { get; set; }

        public DbSet<AnnouncementGoszakup> AnnouncementsGoszakup { get; set; }
        public DbSet<LotGoszakup> LotsGoszakup { get; set; }
        public DbSet<AnnouncementFileGoszakup> AnnouncementFilesGoszakups { get; set; }
        public DbSet<LotFileGoszakup> LotFilesGoszakup { get; set; }

        /// <inheritdoc />
        // ReSharper disable once UnusedMember.Global
        protected TenderContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public TenderContext(DatabaseConnections databaseConnections)
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