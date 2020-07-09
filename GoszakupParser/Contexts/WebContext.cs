using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 09:58:18
    /// <summary>
    /// Web DB context, 'avroradata' schema
    /// </summary>
    public class WebContext<TModel> : DbContext where TModel : DbLoggerCategory.Model
    {
        public DbSet<TModel> Models { get; set; }

        public WebContext(DbContextOptions options)
            : base(options)
        {
        }

        public WebContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.1.158; Database = avroradata; Port=5432; User ID = data_migrator; Password = Z4P6PjEHnJ5nPT; Search Path = avroradata; Integrated Security=true; Pooling=true;");
        }
    }
}