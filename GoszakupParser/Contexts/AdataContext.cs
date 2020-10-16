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
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<TModel> Models { get; set; }

        private DbConnectionCredential ConnectionCredentials { get; set; }

        protected AdataContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public AdataContext(DatabaseConnections databaseConnections)
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

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TModel().BuildModel(modelBuilder);
        }
    }
}