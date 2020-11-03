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
	/// @date 02.11.2020 12:04:10
	/// <summary>
	/// Tender Document Goszakup DB table object
	/// </summary>
	[Table("tender_document_goszakup", Schema = "avroradata")]
	public class TenderDocumentGoszakup : BaseModel
	{
		[Key] [Column("id")] public int? Id { get; set; }
		[Column("identity")] public string Identity { get; set; }
		[Column("number")] public string Number { get; set; }
		[Column("type")] public string Type { get; set; }
		[Column("title")] public string Title { get; set; }
		[Column("link")] public string Link { get; set; }
		[Column("relevance")] public DateTime? Relevance { get; set; }
	}
}