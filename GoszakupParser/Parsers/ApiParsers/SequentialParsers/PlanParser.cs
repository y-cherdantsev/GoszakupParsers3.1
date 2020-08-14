using System;
using System.Collections.Generic;
using System.Linq;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 09.07.2020 15:39:44
    /// <summary>
    /// Plan Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class PlanParser : ApiSequentialParser<PlanDto, PlanGoszakup>
    {
        /// <summary>
        /// List of plans that are already in table
        /// </summary>
        private readonly List<long> _existingPlans;

        private readonly int _apiTotal;

        /// <inheritdoc />
        public PlanParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
            var planContext = new AdataContext<PlanGoszakup>(DatabaseConnections.ParsingAvroradata);
            _existingPlans = planContext.Models.Select(x => x.Id ?? 0).ToList();
            planContext.Dispose();
            _apiTotal = Total;
        }

        /// <inheritdoc />
        protected override PlanGoszakup DtoToModel(PlanDto dto)
        {
            DateTime.TryParse(dto.date_approved, out var dateApproved);
            DateTime.TryParse(dto.date_create, out var dateCreate);
            DateTime.TryParse(dto.timestamp, out var timestamp);

            var plan = new PlanGoszakup
            {
                Id = dto.id,
                RootrecordId = dto.rootrecord_id,
                SubjectBiin = long.Parse(dto.subject_biin),
                RefEnstruCode = dto.ref_enstru_code,
                PlanActNumber = dto.plan_act_number,
                SupplyDateRu = dto.supply_date_ru,
                PlanActId = dto.plan_act_id,
                RefPlanStatusId = dto.ref_plan_status_id,
                PlanFinYear = dto.plan_fin_year,
                PlanPreliminary = dto.plan_preliminary,
                SysSubjectsId = dto.sys_subjects_id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                RefSubjectTypesId = dto.ref_subject_types_id,
                RefTradeMethodsId = dto.ref_trade_methods_id,
                RefUnitsCode = int.Parse(dto.ref_units_code),
                Count = dto.count,
                Price = dto.price,
                Amount = dto.amount,
                RefMonthsId = dto.ref_months_id,
                RefFinsourceId = dto.ref_finsource_id,
                RefPlnPointStatusId = dto.ref_pln_point_status_id,
                PlnPointYear = dto.pln_point_year,
                RefAbpCode = dto.ref_abp_code,
                IsQvazi = dto.is_qvazi,
                SystemId = dto.system_id,
                RefPointTypeId = dto.ref_point_type_id,
                DescRu = dto.desc_ru,
                DescKz = dto.desc_kz,
                ExtraDescRu = dto.extra_desc_ru,
                ExtraDescKz = dto.extra_desc_kz,
                Sum1 = dto.sum_1,
                Sum2 = dto.sum_2,
                Sum3 = dto.sum_3,
                Prepayment = dto.prepayment,
                RefJustificationId = dto.ref_justification_id,
                RefAmendmentAgreemTypeId = dto.ref_amendment_agreem_type_id,
                RefAmendmAgreemJustifId = dto.ref_amendm_agreem_justif_id,
                ContractPrevPointId = dto.contract_prev_point_id,
                DisablePersonId = dto.disable_person_id,
                TransferSysSubjectsId = dto.transfer_sys_subjects_id,
                TransferType = dto.transfer_type,
                RefBudgetTypeId = dto.ref_budget_type_id,
                SubjectNameKz = dto.subject_name_kz,
                SubjectNameRu = dto.subject_name_ru,
                DateApproved = dateApproved,
                DateCreate = dateCreate,
                Timestamp = timestamp
            };

            return plan;
        }

        /// <summary>
        /// Returns true if all lot elements are older than 60 days
        /// </summary>
        /// \todo(Not optimized, need logic reworking => { if (apiResponse.items.All(x => _existingPlans.Contains(x.id))) return true; }) 
        /// \todo(Don't work, fix) 
        protected override bool StopCondition(object checkElement)
        {
            var apiResponse = (ApiResponse<PlanDto>) checkElement;
            // If all plans are already in table, parsing can be stopped
            if (apiResponse.items.All(x => _existingPlans.Contains(x.id)))
                return true;

            //If all plans are older than 60 days
            return apiResponse.items.All(x =>
            {
                DateTime.TryParse(x.date_create, out var dateCreate);
                return dateCreate < DateTime.Now.Subtract(TimeSpan.FromDays(270));
            });
        }

        /// <inheritdoc />
        protected override string LogMessage(object obj = null)
        {
            var apiResponse = (ApiResponse<PlanDto>) obj;
            return $"Parsed:[{_apiTotal - Total}] Date:[{apiResponse.items[0].date_create}]";
        }
    }
}