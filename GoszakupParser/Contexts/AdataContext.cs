﻿using System;
using System.Linq;
using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
using static GoszakupParser.Configuration;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Base DB context - used for creating contexts
    /// </summary>
    public class AdataContext<TModel> : DbContext where TModel : BaseModel, new()
    {
        /// <summary>
        /// Set of models loaded by created context
        /// </summary>
        public DbSet<TModel> Models { get; set; }

        private DbConnectionCredential connectionCredentials { get; set; }

        protected AdataContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public AdataContext(DatabaseConnections databaseConnections)
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
                    $"Application Name={Console.Title};");
            else
                throw new NullReferenceException("Cannot find such connection credentials");
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TModel().BuildModel(modelBuilder);
        }
    }

    public enum DatabaseConnections
    {
        ParsingAvroradata,
        ParsingMonitoring,
        WebAdataTender,
        WebAvroradata
    }
}