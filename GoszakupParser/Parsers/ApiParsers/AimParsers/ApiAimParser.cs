using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Parsers.ApiParsers.AimParsers
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
    public abstract class ApiAimParser<TDto, TModel, TSourceModel> : ApiParser<TDto, TModel>
        where TModel : DbLoggerCategory.Model where TSourceModel : DbLoggerCategory.Model
    {
        protected List<string> Aims { get; set; }
        private new int Total { get; set; }

        public ApiAimParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings,
            authToken)
        {
            Aims = LoadAims();
            Total = Aims.Count;
        }

        protected abstract List<string> LoadAims();
        
        public override Task ParseAsync()
        {
            throw new System.NotImplementedException();
        }

        protected abstract Task ParseArray(string[] list);
    }
}