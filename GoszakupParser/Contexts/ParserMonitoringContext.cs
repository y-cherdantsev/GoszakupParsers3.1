using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// <summary>
    /// Parser DB context, 'monitoring' schema
    /// </summary>
    public class ParserMonitoringContext : DbContext
    {
        public DbSet<ParserMonitoring> ParserMonitorings { get; set; }
        public DbSet<Proxy> Proxies { get; set; }

        public ParserMonitoringContext(DbContextOptions options)
            : base(options)
        {
        }

        public ParserMonitoringContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.1.25; Database = adata; Port=5432; User ID = administrator; Password = Z4P6PjEHnJ5nPT; Search Path = monitoring; Integrated Security=true; Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proxy>().HasKey(proxy => new {proxy.Address, proxy.Port});
        }
    }
}