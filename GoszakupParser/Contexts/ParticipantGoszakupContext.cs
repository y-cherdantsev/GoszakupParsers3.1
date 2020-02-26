using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{
    /// @author Yevgeniy Cherdantsev
    /// @date 22.02.2020 13:32:44
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'participant_goszakup'
    /// </summary>
    public class ParticipantGoszakupContext : ParserContext
    {
        public DbSet<ParticipantGoszakup> ParticipantsGoszakup { get; set; }
    }
}