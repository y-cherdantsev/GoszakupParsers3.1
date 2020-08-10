using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 07.08.2020 16:47:59
    /// <summary>
    /// Tender Document Parsing DB table object
    /// </summary>
    [Table("tender_document_goszakup")]
    public class
        TenderDocumentGoszakup : BaseModel
    {
        [Key] [Column("id")] public int Id { get; set; }
        [Column("identity")] public string Identity { get; set; }
        [Column("number")] public string Number { get; set; }
        [Column("type")] public string Type { get; set; }
        [Column("title")] public string Title { get; set; }
        [Column("link")] public string Link { get; set; }
    }
}