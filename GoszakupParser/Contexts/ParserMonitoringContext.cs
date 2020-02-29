using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
namespace GoszakupParser.Contexts
{

    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 17:50:16
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'parser_monitoring'
    /// </summary>

    public class ParserMonitoringContext : DbContext
    {
    
        public DbSet<ParserMonitoring> ParserMonitorings { get; set; }
        public DbSet<Proxy> Proxies { get; set; }
        public ParserMonitoringContext(DbContextOptions<ParserMonitoringContext> options)
            : base(options)
        {

        }

        public ParserMonitoringContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.2.25; Database = adata; Port=5432; User ID = administrator; Password = Z4P6PjEHnJ5nPT; Search Path = monitoring; Integrated Security=true; Pooling=true;");

        }
    }
}