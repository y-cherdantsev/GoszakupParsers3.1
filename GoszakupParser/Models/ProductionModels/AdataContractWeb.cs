using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ProductionModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.08.2020 16:48:23
    /// <summary>
    /// Contract Web DB table object
    /// </summary>
    [Table("contracts", Schema = "adata_tender")]
    public class AdataContractWeb : BaseModel
    {
        [Key] [Column("id")] public long Id { get; set; }
        [Column("source_id")] public int? SourceId { get; set; }
        [Column("announcement_number")] public string AnnouncementNumber { get; set; }
        [Column("supplier_biin")] public int? SupplierBiin { get; set; }
        [Column("customer_bin")] public int? CustomerBin { get; set; }
        [Column("supplier_iik")] public string SupplierIik { get; set; }
        [Column("customer_iik")] public string CustomerIik { get; set; }
        [Column("fin_year")] public int? FinYear { get; set; }
        [Column("sign_date")] public DateTime? SignDate { get; set; }
        [Column("ec_end_date")] public DateTime? EcEndDate { get; set; }
        [Column("description_ru")] public string DescriptionRu { get; set; }
        [Column("contract_sum_wnds")] public double? ContractSumWnds { get; set; }
        [Column("fakt_sum_wnds")] public double? FaktSumWnds { get; set; }
        [Column("supplier_bank_id")] public int? SupplierBankId { get; set; }
        [Column("customer_bank_id")] public int? CustomerBankId { get; set; }
        [Column("agr_form_id")] public int? AgrFormId { get; set; }
        [Column("year_type_id")] public int? YearTypeId { get; set; }
        [Column("method_id")] public int? MethodId { get; set; }
        [Column("status_id")] public int? StatusId { get; set; }
        [Column("type_id")] public int? TypeId { get; set; }
        [Column("source_number")] public string SourceNumber { get; set; }
        [Column("source_number_sys")] public string SourceNumberSys { get; set; }
        [Column("create_date")] public DateTime? CreateDate { get; set; }
        [Column("doc_link")] public string DocLink { get; set; }
        [Column("doc_name")] public string DocName { get; set; }
        [Column("doc_location")] public string DocLocation { get; set; }
    }
}