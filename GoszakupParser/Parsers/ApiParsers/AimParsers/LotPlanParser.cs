using System;
using System.Collections.Generic;
using System.Linq;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 03.08.2020 14:47:32
    /// <summary>
    /// Lot Plan Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class LotPlanParser : ApiAimParser<PlanDto, PlanGoszakup>
    {
        /// <inheritdoc />
        public LotPlanParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings,
            authToken)
        {
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

        /// <inheritdoc />
        protected override IEnumerable<string> LoadAims()
        {
            var lotContext = new AdataContext<LotGoszakup>(DatabaseConnections.ParsingAvroradata);
            var aims = lotContext.Models
                .FromSqlRaw(
                    "select * from avroradata.lot_goszakup l full outer join avroradata.plan_goszakup p on l.plan_point = p.id where p.id is null and l.ref_lot_status_id<360")
                .Select(x => x.PlanPoint.ToString()).ToList();
            lotContext.Dispose();
            return aims;
        }
    }
}