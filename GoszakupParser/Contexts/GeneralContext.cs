using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Base DB context - used for creating contexts
    /// </summary>
    public class GeneralContext<TModel> : DbContext where TModel : BaseModel, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Set of models loaded by created context
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<TModel> Models { get; set; }

        /// <inheritdoc />
        protected GeneralContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public GeneralContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                $"{ConnectionString}" +
                $"Application Name={Configuration.Title};");
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TModel().BuildModel(modelBuilder);
        }
    }
}