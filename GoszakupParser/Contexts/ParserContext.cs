using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// <summary>
    /// Parent context 
    /// </summary>
    public abstract class ParserContext: DbContext
    {
        public ParserContext(DbContextOptions<ParserContext> options)
            : base(options)
        {
        }

        public ParserContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server = 192.168.2.25; Database = adata; Port=5432; User ID = data_migrator; Password = Z4P6PjEHnJ5nPT; Search Path = avroradata; Integrated Security=true; Pooling=true;");
        }
    }
}