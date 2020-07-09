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
    /// Parser DB context, 'avroradata' schema
    /// </summary>
    public class ParserContext<TModel> : DbContext where TModel : DbLoggerCategory.Model
    {
        public DbSet<TModel> Models { get; set; }

        public ParserContext(DbContextOptions options)
            : base(options)
        {
        }

        public ParserContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.1.25; Database = adata; Port=5432; User ID = data_parser; Password = Z4P6PjEHnJ5nPT; Search Path = avroradata; Integrated Security=true; Pooling=true;");
        }
    }
}