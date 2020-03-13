using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.WebModels;
using Newtonsoft.Json;
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
        public RnuReferenceParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings,
            authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override RnuReferenceGoszakup DtoToDb(RnuReferenceDto dto)
        {
            var rnuReferenceGoszakup = new RnuReferenceGoszakup();

            return rnuReferenceGoszakup;
        }

        protected override List<string> LoadAims()
        {
            var tempCtx = new WebContext<UnscrupulousGoszakupWeb>();
            var tempList = tempCtx.Models.Select(x => x.BiinCompanies).ToList();
            var stringList = tempList.Select(l => l.ToString().PadLeft(12, '0')).ToList();
            return stringList;
        }

        protected override async Task ParseArray(string[] list)
        {
            foreach (var biin in list)
            {
                var response = GetApiPageResponse($"{Url}/{biin}", 0);
                if (response == null)
                    continue;
                if (!response.Contains("\"founders\":null}"))
                {
                    Console.WriteLine(response);
                }
                // var element = JsonConvert.DeserializeObject<>(response);
            }
        }
    }
}