using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    public sealed class PlanParser : ApiSequentialParser<PlanDto, PlanGoszakup>
    {
        public PlanParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(
            parserSettings, authToken, proxy)
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
            planGoszakup.PlanActNumber = dto.plan_act_number;
            planGoszakup.SupplyDateRu = dto.supply_date_ru;
            planGoszakup.PlanActId = dto.plan_act_id;
            planGoszakup.RefPlanStatusId = dto.ref_plan_status_id;
            planGoszakup.PlanFinYear = dto.plan_fin_year;
            planGoszakup.PlanPreliminary = dto.plan_preliminary;
            planGoszakup.SysSubjectsId = dto.sys_subjects_id;
            planGoszakup.NameRu = dto.name_ru;
            planGoszakup.NameKz = dto.name_kz;
            planGoszakup.RefSubjectTypesId = dto.ref_subject_types_id;
            planGoszakup.RefTradeMethodsId = dto.ref_trade_methods_id;
            planGoszakup.RefUnitsCode = int.Parse(dto.ref_units_code);
            planGoszakup.Count = dto.count;
            planGoszakup.Price = dto.price;
            planGoszakup.Amount = dto.amount;
            planGoszakup.RefMonthsId = dto.ref_months_id;
            planGoszakup.RefFinsourceId = dto.ref_finsource_id;
            planGoszakup.RefPlnPointStatusId = dto.ref_pln_point_status_id;
            planGoszakup.PlnPointYear = dto.pln_point_year;
            planGoszakup.RefAbpCode = dto.ref_abp_code;
            planGoszakup.IsQvazi = dto.is_qvazi;
            planGoszakup.SystemId = dto.system_id;
            planGoszakup.RefPointTypeId = dto.ref_point_type_id;
            planGoszakup.DescRu = dto.desc_ru;
            planGoszakup.DescKz = dto.desc_kz;
            planGoszakup.ExtraDescRu = dto.extra_desc_ru;
            planGoszakup.ExtraDescKz = dto.extra_desc_kz;
            planGoszakup.Sum1 = dto.sum_1;
            planGoszakup.Sum2 = dto.sum_2;
            planGoszakup.Sum3 = dto.sum_3;
            planGoszakup.Prepayment = dto.prepayment;
            planGoszakup.RefJustificationId = dto.ref_justification_id;
            planGoszakup.RefAmendmentAgreemTypeId = dto.ref_amendment_agreem_type_id;
            planGoszakup.RefAmendmAgreemJustifId = dto.ref_amendm_agreem_justif_id;
            planGoszakup.ContractPrevPointId = dto.contract_prev_point_id;
            planGoszakup.DisablePersonId = dto.disable_person_id;
            planGoszakup.TransferSysSubjectsId = dto.transfer_sys_subjects_id;
            planGoszakup.TransferType = dto.transfer_type;
            planGoszakup.RefBudgetTypeId = dto.ref_budget_type_id;
            planGoszakup.SubjectNameKz = dto.subject_name_kz;
            planGoszakup.SubjectNameRu = dto.subject_name_ru;

            try
            {
                planGoszakup.DateApproved = DateTime.Parse(dto.date_approved);
            }
            catch (Exception)
            {
                planGoszakup.DateApproved = null;
            }
            
            try
            {
                planGoszakup.DateCreate = DateTime.Parse(dto.date_create);
            }
            catch (Exception)
            {
                planGoszakup.DateCreate = null;
            }
            
            try
            {
                planGoszakup.Timestamp = DateTime.Parse(dto.timestamp);
            }
            catch (Exception)
            {
                planGoszakup.Timestamp = null;
            }
            
            
            return planGoszakup;
        }
    }
}