using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace GoszakupParser.Models.WebModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.08.2020 16:48:23
    /// <summary>
    /// Announcement Web DB table object
    /// </summary>
    [Table("announcements")]
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