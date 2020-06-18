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
            planGoszakup.RootrecordId = dto.rootrecord_id;
            planGoszakup.SubjectBiin = long.Parse(dto.subject_biin);
            planGoszakup.RefEnstruCode = dto.ref_enstru_code;
            planGoszakup.SupplyDateRu = dto.supply_date_ru;
            return planGoszakup;
        }
    }
}