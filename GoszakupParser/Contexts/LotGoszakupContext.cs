using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 19.02.2020 19:49:36
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'lot_goszakup'
    /// </summary>
    public class LotGoszakupContext : ParserContext
    {
        public DbSet<LotGoszakup> LotsGoszakup { get; set; }
    }
}