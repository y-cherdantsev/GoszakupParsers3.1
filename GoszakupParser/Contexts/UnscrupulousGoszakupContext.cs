using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 22.02.2020 16:02:33
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'unscrupulous_goszakup'
    /// </summary>
    public class UnscrupulousGoszakupContext : ParserContext
    {
        public DbSet<UnscrupulousGoszakup> UnscrupulousGoszakup { get; set; }
    }
}