using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// <summary>
    /// Parent context 
    /// </summary>
    public class ParserContext<TModel>: DbContext where TModel : DbLoggerCategory.Model
    {
        public DbSet<TModel> Models { get; set; }
        public ParserContext(DbContextOptions<ParserContext<TModel>> options)
            : base(options)
        {
        }

        public ParserContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.2.25; Database = adata; Port=5432; User ID = data_parser; Password = Z4P6PjEHnJ5nPT; Search Path = avroradata; Integrated Security=true; Pooling=true;");
        }
    }
}