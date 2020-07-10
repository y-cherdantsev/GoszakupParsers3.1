using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.WebModels;

namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 16:16:57
    /// <summary>
    /// Rnu References Parser
    /// </summary>
    public sealed class RnuReferenceParser : ApiAimParser<RnuReferenceDto, RnuReferenceGoszakup>
    {
        /// <inheritdoc />
        public RnuReferenceParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override RnuReferenceGoszakup DtoToDb(RnuReferenceDto dto)
        {
            long.TryParse(dto.customer_biin, out var customerBin);
            long.TryParse(dto.supplier_biin, out var supplierBin);
            long.TryParse(dto.supplier_head_biin, out var supplierHeadBiin);
            DateTime.TryParse(dto.start_date, out var startDate);
            DateTime.TryParse(dto.end_date, out var endDate);
            DateTime.TryParse(dto.court_decision_date, out var courtDecisionDate);
            DateTime.TryParse(dto.index_date, out var indexDate);

            var rnuReference = new RnuReferenceGoszakup
            {
                Id = Convert.ToInt32(dto.id),
                Pid = Convert.ToInt32(dto.pid),
                CustomerNameRu = !string.IsNullOrEmpty(dto.customer_name_ru) ? dto.customer_name_ru : null,
                CustomerNameKz = !string.IsNullOrEmpty(dto.customer_name_kz) ? dto.customer_name_kz : null,
                SupplierNameRu = !string.IsNullOrEmpty(dto.supplier_name_ru) ? dto.supplier_name_ru : null,
                SupplierNameKz = !string.IsNullOrEmpty(dto.supplier_name_kz) ? dto.supplier_name_kz : null,
                CustomerBiin = customerBin,
                SupplierBiin = supplierBin,
                CourtDecision = !string.IsNullOrEmpty(dto.court_decision) ? dto.court_decision : null,
                SupplierInnunp = dto.supplier_innunp,
                SystemId = dto.system_id,
                RefReasonId = Convert.ToInt32(dto.ref_reason_id),
                SupplierHeadBiin = supplierHeadBiin != 0 ? supplierHeadBiin : (long?) null,
                SupplierHeadNameKz =
                    !string.IsNullOrEmpty(dto.supplier_head_name_kz) ? dto.supplier_head_name_kz : null,
                SupplierHeadNameRu =
                    !string.IsNullOrEmpty(dto.supplier_head_name_ru) ? dto.supplier_head_name_ru : null,
                StartDate = startDate,
                EndDate = endDate,
                CourtDecisionDate = courtDecisionDate,
                IndexDate = indexDate
            };

            return rnuReference;
        }

        /// <inheritdoc />
        protected override IEnumerable<string> LoadAims()
        {
            var tempCtx = new WebContext<UnscrupulousGoszakupWeb>();
            var tempList = tempCtx.Models.Select(x => x.BiinCompanies).ToList();
            var stringList = tempList.Select(l => l.ToString().PadLeft(12, '0')).ToList();
            return stringList;
        }
    }
}