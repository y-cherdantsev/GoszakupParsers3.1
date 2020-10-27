using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ProductionModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 09:33:10
    /// <summary>
    /// participant Web DB table field
    /// </summary>
    [Table("all_participants_goszakup", Schema = "avroradata")]
    public class ParticipantGoszakupWeb : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("biin_companies")]
        public long? BiinCompanies { get; set; }

        [Column("pid")] public int Pid { get; set; }
        [Column("regdate")] public DateTime? Regdate { get; set; }
        [Column("last_update_date")] public DateTime? LastUpdateDate { get; set; }
        [Column("crdate")] public DateTime? Crdate { get; set; }
        [Column("index_date")] public DateTime? IndexDate { get; set; }
        [Column("number_reg")] public string NumberReg { get; set; }
        [Column("customer")] public bool? Customer { get; set; }
        [Column("organizer")] public bool? Organizer { get; set; }
        [Column("mark_national_company")] public bool? MarkNationalCompany { get; set; }
        [Column("is_single_org")] public bool? IsSingleOrg { get; set; }
        [Column("year")] public DateTime? Year { get; set; }
        [Column("status")] public bool? Status { get; set; }
        [Column("relevance_date")] public DateTime? RelevanceDate { get; set; }
        [Column("supplier")] public bool? Supplier { get; set; }
    }
}