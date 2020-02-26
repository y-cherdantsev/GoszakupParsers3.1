using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 21.02.2020 13:23:50
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'announcement_goszakup'
    /// </summary>
    public class AnnouncementGoszakupContext : ParserContext
    {
        public DbSet<AnnouncementGoszakup> AnnouncementsGoszakupDtos { get; set; } 
    }
}