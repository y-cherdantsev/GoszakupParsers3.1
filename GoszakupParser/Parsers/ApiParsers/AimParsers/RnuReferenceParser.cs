using System.Collections.Generic;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.WebModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 16:16:57
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class RnuReferenceParser : ApiAimParser<RnuReferenceDto, RnuReferenceGoszakup, UnscrupulousGoszakupWeb>
    {
        public RnuReferenceParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings, authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override RnuReferenceGoszakup DtoToDb(RnuReferenceDto dto)
        {
            throw new System.NotImplementedException();
        }

        protected override List<string> LoadAims()
        {
            throw new System.NotImplementedException();
        }

        protected override Task ParseArray(string[] list)
        {
            throw new System.NotImplementedException();
        }
    }
}