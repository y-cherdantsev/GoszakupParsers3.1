using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Contexts
{

    /// @author Yevgeniy Cherdantsev
    /// @date 24.02.2020 17:52:27
    /// @version 1.0
    /// <summary>
    /// Контекст для работы с таблицей 'contract_goszakup'
    /// </summary>

    public class ContractGoszakupContext : ParserContext
    {
        public DbSet<ContractGoszakup> ContractsGoszakup { get; set; }
    }
}