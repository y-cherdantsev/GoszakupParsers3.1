using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 16:18:25
    /// <summary>
    /// RnuReference Parsing DB table object
    /// </summary>
    [Table("rnu_reference_goszakup")]
    public class RnuReferenceGoszakup : BaseModel
    {
        [Key] [Column("id")] public int Id { get; set; }
        [Column("pid")] public int? Pid { get; set; }
        [Column("customer_biin")] public long? CustomerBiin { get; set; }
        [Column("customer_name_ru")] public string CustomerNameRu { get; set; }
        [Column("customer_name_kz")] public string CustomerNameKz { get; set; }
        [Column("supplier_biin")] public long? SupplierBiin { get; set; }
        [Column("supplier_name_ru")] public string SupplierNameRu { get; set; }
        [Column("supplier_name_kz")] public string SupplierNameKz { get; set; }
        [Column("supplier_innunp")] public string SupplierInnunp { get; set; }
        [Column("supplier_head_name_kz")] public string SupplierHeadNameKz { get; set; }
        [Column("supplier_head_name_ru")] public string SupplierHeadNameRu { get; set; }
        [Column("supplier_head_biin")] public long? SupplierHeadBiin { get; set; }
        [Column("court_decision")] public string CourtDecision { get; set; }
        [Column("court_decision_date")] public DateTime? CourtDecisionDate { get; set; }
        [Column("start_date")] public DateTime? StartDate { get; set; }
        [Column("end_date")] public DateTime? EndDate { get; set; }
        [Column("ref_reason_id")] public int RefReasonId { get; set; }
        [Column("index_date")] public DateTime? IndexDate { get; set; }
        [Column("system_id")] public byte SystemId { get; set; }
    }
}