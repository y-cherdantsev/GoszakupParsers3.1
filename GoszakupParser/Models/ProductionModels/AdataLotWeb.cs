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
    /// Lot Web DB table object
    /// </summary>
    [Table("lots", Schema = "adata_tender")]
    public class AdataLotWeb : BaseModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("source_number")] public string SourceNumber { get; set; }
        [Column("title")] public string Title { get; set; }
        [Column("announcement_id")] public long AnnouncementId { get; set; }
        [Column("status_id")] public long? StatusId { get; set; }
        [Column("method_id")] public long? MethodId { get; set; }
        [Column("source_id")] public long SourceId { get; set; }
        [Column("application_start_date")] public DateTime? ApplicationStartDate { get; set; }
        [Column("application_finish_date")] public DateTime? ApplicationFinishDate { get; set; }
        [Column("customer_bin")] public long? CustomerBin { get; set; }
        [Column("supply_location")] public string SupplyLocation { get; set; }
        [Column("tender_location")] public string TenderLocation { get; set; }
        [Column("tru_code")] public string TruCode { get; set; }
        [Column("characteristics")] public string Characteristics { get; set; }
        [Column("quantity")] public double Quantity { get; set; }
        [Column("measure_id")] public long? MeasureId { get; set; }
        [Column("unit_price")] public double UnitPrice { get; set; }
        [Column("total_amount")] public double TotalAmount { get; set; }
        [Column("terms")] public string Terms { get; set; }
        [Column("source_link")] public string SourceLink { get; set; }
        [Column("relevance_date")] public DateTime RelevanceDate { get; set; } = DateTime.Now;
        [Column("flag_prequalification")] public bool FlagPrequalification { get; set; }
    }
    
    /// @author Yevgeniy Cherdantsev
    /// @date 28.10.2020 10:37:43
    /// <summary>
    /// Lot Documentations Web DB table object
    /// </summary>
    [Table("lot_documentations", Schema = "adata_tender")]
    public class LotDocumentationWeb
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)] [Column("id")] public int? Id { get; set; }
        [Column("documentation_type_id")] public int? DocumentationTypeId { get; set; }
        [Column("location")] public string Location { get; set; }
        [Column("relevance_date")] public DateTime? RelevanceDate { get; set; }
        [Column("lot_id")] public long LotId { get; set; }
        [Column("source_link")] public string SourceLink { get; set; }
        [Column("name")] public string Name { get; set; }
        public AdataLotWeb Lot { get; set; }
    }
}