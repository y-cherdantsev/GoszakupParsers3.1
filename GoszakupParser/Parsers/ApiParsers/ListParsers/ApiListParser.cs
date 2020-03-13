using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.ListParsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 14:01:53
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    /// <code>
    /// 
    /// </code>


    public abstract class ApiListParser<TDto, TModel> : ApiParser<TDto, TModel>
        where TModel : DbLoggerCategory.Model, new()
    {
        public ApiListParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings, authToken)
        {
        }
        public override Task ParseAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}