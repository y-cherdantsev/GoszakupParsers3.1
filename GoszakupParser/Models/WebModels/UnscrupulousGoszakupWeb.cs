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

namespace GoszakupParser.Models.WebModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 09:33:46
    /// <summary>
    /// unscrupulous Web DB table field
    /// </summary>
    [Table("unscrupulous_goszakup")]
    public class UnscrupulousGoszakupWeb : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("biin_companies")]
        public long? BiinCompanies { get; set; }

        [Column("status")] public bool? Status { get; set; }
        [Column("relevance_date")] public DateTime? RelevanceDate { get; set; }
    }
}