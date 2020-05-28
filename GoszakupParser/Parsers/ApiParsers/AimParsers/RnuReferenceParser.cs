using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public RnuReferenceParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override RnuReferenceGoszakup DtoToDb(RnuReferenceDto dto)
        {
            var rnuReference = new RnuReferenceGoszakup();
            rnuReference.Id = Convert.ToInt32(dto.id);
            rnuReference.Pid = Convert.ToInt32(dto.pid);
            rnuReference.CustomerNameRu = !string.IsNullOrEmpty(dto.customer_name_ru) ? dto.customer_name_ru : null;
            rnuReference.CustomerNameKz = !string.IsNullOrEmpty(dto.customer_name_kz) ? dto.customer_name_kz : null;
            rnuReference.SupplierNameRu = !string.IsNullOrEmpty(dto.supplier_name_ru) ? dto.supplier_name_ru : null;
            rnuReference.SupplierNameKz = !string.IsNullOrEmpty(dto.supplier_name_kz) ? dto.supplier_name_kz : null;
            long.TryParse(dto.customer_biin, out var customerBin);
            rnuReference.CustomerBiin = customerBin;
            long.TryParse(dto.supplier_biin, out var supplierBin);
            rnuReference.SupplierBiin = supplierBin;
            rnuReference.CourtDecision = !string.IsNullOrEmpty(dto.court_decision) ? dto.court_decision : null;
            rnuReference.SupplierInnunp = dto.supplier_innunp;
            rnuReference.SystemId = dto.system_id;
            rnuReference.RefReasonId = Convert.ToInt32(dto.ref_reason_id);
            long.TryParse(dto.supplier_head_biin, out var supplierHeadBiin);
            rnuReference.SupplierHeadBiin = supplierHeadBiin != 0 ? supplierHeadBiin : (long?) null;
            rnuReference.SupplierHeadNameKz =
                !string.IsNullOrEmpty(dto.supplier_head_name_kz) ? dto.supplier_head_name_kz : null;
            rnuReference.SupplierHeadNameRu =
                !string.IsNullOrEmpty(dto.supplier_head_name_ru) ? dto.supplier_head_name_ru : null;
            try
            {
                rnuReference.StartDate = DateTime.Parse(dto.start_date);
            }
            catch (Exception)
            {
                rnuReference.StartDate = null;
            }

            try
            {
                rnuReference.EndDate = DateTime.Parse(dto.end_date);
            }
            catch (Exception)
            {
                rnuReference.EndDate = null;
            }

            try
            {
                rnuReference.CourtDecisionDate = DateTime.Parse(dto.court_decision_date);
            }
            catch (Exception)
            {
                rnuReference.CourtDecisionDate = null;
            }

            try
            {
                rnuReference.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
                rnuReference.IndexDate = null;
            }

            return rnuReference;
        }

        protected override List<string> LoadAims()
        {
            var tempCtx = new WebContext<UnscrupulousGoszakupWeb>();
            var tempList = tempCtx.Models.Select(x => x.BiinCompanies).ToList();
            var stringList = tempList.Select(l => l.ToString().PadLeft(12, '0')).ToList();
            return stringList;
        }
    }
}