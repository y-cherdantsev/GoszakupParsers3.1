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
    /// Announcement Web DB table object
    /// </summary>
    [Table("announcements", Schema = "adata_tender")]
    public class AdataAnnouncementWeb : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("source_number")] public string SourceNumber { get; set; }
        [Column("title")] public string Title { get; set; }
        [Column("status_id")] public long? StatusId { get; set; }
        [Column("method_id")] public long? MethodId { get; set; }
        [Column("source_id")] public long SourceId { get; set; }
        [Column("application_start_date")] public DateTime? ApplicationStartDate { get; set; }
        [Column("application_finish_date")] public DateTime? ApplicationFinishDate { get; set; }
        [Column("customer_bin")] public long? CustomerBin { get; set; }
        [Column("source_link")] public string SourceLink { get; set; }
        [Column("relevance_date")] public DateTime? RelevanceDate { get; set; } = DateTime.Now;
        [Column("lots_quantity")] public long LotsQuantity { get; set; }
        [Column("lots_amount")] public double LotsAmount { get; set; }
        [Column("email_address")] public string EmailAddress { get; set; }
        [Column("phone_number")] public string PhoneNumber { get; set; }
        [Column("flag_prequalification")] public bool FlagPrequalification { get; set; }
        [Column("tender_priority_id")] public long? TenderPriorityId { get; set; }
    }

    /// @author Yevgeniy Cherdantsev
    /// @date 28.10.2020 10:52:46
    /// <summary>
    /// Announcement Documentations Web DB table object
    /// </summary>
    [Table("announcement_documentations", Schema = "adata_tender")]
    public class AnnouncementDocumentationWeb
    {
        [Key] [Column("id")] public int? Id { get; set; }
        [Column("documentation_type_id")] public int? DocumentationTypeId { get; set; }
        [Column("location")] public string Location { get; set; }
        [Column("relevance_date")] public DateTime? RelevanceDate { get; set; }
        [Column("announcement_id")] public long AnnouncementId { get; set; }
        [Column("source_link")] public string SourceLink { get; set; }
        [Column("name")] public string Name { get; set; }
        public AdataAnnouncementWeb Announcement { get; set; }
    }
    
    /// @author Yevgeniy Cherdantsev
    /// @date 10.08.2020 16:48:52
    /// <summary>
    /// Statuses Web DB table object
    /// </summary>
    [Table("statuses")]
    public class StatusWeb : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")] public new string Name { get; set; }
        [Column("combined_id")] public long? CombinedId { get; set; }
    }
}