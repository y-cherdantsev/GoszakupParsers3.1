using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:43:05
    /// <summary>
    /// Plan Parsing DB table field
    /// </summary>
    [Table("plan_goszakup")]
    public class PlanGoszakup : DbLoggerCategory.Model
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("plan_act_id")] public long? PlanActId { get; set; }
        [Column("plan_act_number")] public string PlanActNumber { get; set; }
        [Column("ref_plan_status_id")] public int? RefPlanStatusId { get; set; }
        [Column("plan_fin_year")] public int? PlanFinYear { get; set; }
        [Column("plan_preliminary")] public int? PlanPreliminary { get; set; }
        [Column("rootrecord_id")] public long? RootrecordId { get; set; }
        [Column("sys_subjects_id")] public long? SysSubjectsId { get; set; }
        [Column("subject_biin")] public long? SubjectBiin { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("ref_trade_methods_id")] public int? RefTradeMethodsId { get; set; }
        [Column("ref_units_code")] public int? RefUnitsCode { get; set; }
        [Column("count")] public double? Count { get; set; }
        [Column("price")] public double? Price { get; set; }
        [Column("amount")] public double? Amount { get; set; }
        [Column("ref_months_id")] public int? RefMonthsId { get; set; }
        [Column("ref_pln_point_status_id")] public int? RefPlnPointStatusId { get; set; }
        [Column("pln_point_year")] public int? PlnPointYear { get; set; }
        [Column("ref_subject_types_id")] public int? RefSubjectTypesId { get; set; }
        [Column("ref_enstru_code")] public string RefEnstruCode { get; set; }
        [Column("ref_finsource_id")] public int? RefFinsourceId { get; set; }
        [Column("ref_abp_code")] public int? RefAbpCode { get; set; }
        [Column("date_approved")] public DateTime? DateApproved { get; set; }
        [Column("is_qvazi")] public int? IsQvazi { get; set; }
        [Column("date_create")] public DateTime? DateCreate { get; set; }
        [Column("timestamp")] public DateTime? Timestamp { get; set; }
        [Column("system_id")] public int? SystemId { get; set; }
        [Column("ref_point_type_id")] public int? RefPointTypeId { get; set; }
        [Column("desc_ru")] public string DescRu { get; set; }
        [Column("desc_kz")] public string DescKz { get; set; }
        [Column("extra_desc_ru")] public string ExtraDescRu { get; set; }
        [Column("extra_desc_kz")] public string ExtraDescKz { get; set; }
        [Column("sum_1")] public long? Sum1 { get; set; }
        [Column("sum_2")] public long? Sum2 { get; set; }
        [Column("sum_3")] public long? Sum3 { get; set; }
        [Column("supply_date_ru")] public string SupplyDateRu { get; set; }
        [Column("prepayment")] public double? Prepayment { get; set; }
        [Column("ref_justification_id")] public int? RefJustificationId { get; set; }
        [Column("ref_amendment_agreem_type_id")] public int? RefAmendmentAgreemTypeId { get; set; }
        [Column("ref_amendm_agreem_justif_id")] public int? RefAmendmAgreemJustifId { get; set; }
        [Column("contract_prev_point_id")] public int? ContractPrevPointId { get; set; }
        [Column("disable_person_id")] public int? DisablePersonId { get; set; }
        [Column("transfer_sys_subjects_id")] public int? TransferSysSubjectsId { get; set; }
        [Column("transfer_type")] public int? TransferType { get; set; }
        [Column("ref_budget_type_id")] public int? RefBudgetTypeId { get; set; }
        [Column("subject_name_kz")] public string SubjectNameKz { get; set; }
        [Column("subject_name_ru")] public string SubjectNameRu { get; set; }
    }
}