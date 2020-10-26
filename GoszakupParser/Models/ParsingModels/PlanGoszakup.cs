using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 12:13:14
    /// <summary>
    /// Plan Goszakup DB table object
    /// </summary>
    [Table("plan_goszakup")]
    public class PlanGoszakup
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("contract_unit_id")] public long? ContractUnitId { get; set; }
        [Column("source_unique_id")] public long? SourceUniqueId { get; set; }
        [Column("act_number")] public string ActNumber { get; set; }
        [Column("fin_year")] public int? FinYear { get; set; }
        [Column("date_approved")] public DateTime? DateApproved { get; set; }
        [Column("status")] public string Status { get; set; }
        [Column("method")] public string Method { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("description")] public string Description { get; set; }
        [Column("measure")] public string Measure { get; set; }
        [Column("count")] public double? Count { get; set; }
        [Column("price")] public double? Price { get; set; }
        [Column("amount")] public double? Amount { get; set; }
        [Column("month_id")] public int? MonthId { get; set; }
        [Column("is_qvazi")] public bool? IsQvazi { get; set; }
        [Column("tru_code")] public string TruCode { get; set; }
        [Column("date_create")] public DateTime? DateCreate { get; set; }
        [Column("extra_description")] public string ExtraDescription { get; set; }
        [Column("supply_date")] public string SupplyDate { get; set; }
        [Column("prepayment")] public double? Prepayment { get; set; }
        [Column("subject_biin")] public long? SubjectBiin { get; set; }
    }
}