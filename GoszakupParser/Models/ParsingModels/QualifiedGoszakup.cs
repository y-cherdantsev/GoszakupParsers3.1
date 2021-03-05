using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 05.03.2021 16:51:32
    /// <summary>
    /// Qualified Participant Parsing DB table object
    /// </summary>
    [Table("qualified_goszakup", Schema = "avroradata")]
    public class QualifiedGoszakup : BaseModel
    {
        // ReSharper disable once UnusedMember.Global
        [Key] [Column("id")] public int Id { get; set; }
        [Column("biin")] public long? Biin { get; set; }
        [Column("company_name")] public string CompanyName { get; set; }
        [Column("doc_number")] public long DocNumber { get; set; }
        [Column("doc_issue_date")] public DateTime? DocIssueDate { get; set; }
    }
}