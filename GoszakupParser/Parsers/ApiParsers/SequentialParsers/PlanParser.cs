using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    public sealed class PlanParser : ApiSequentialParser<PlanDto, PlanGoszakup>
    {
        public PlanParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings, authToken, proxy)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override PlanGoszakup DtoToDb(PlanDto dto)
        {
            var planGoszakup = new PlanGoszakup();
            planGoszakup.Id = dto.id;
            return planGoszakup;
        }
    }
}