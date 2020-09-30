using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
	/// @author Yevgeniy Cherdantsev
	/// @date 27.09.2020 11:42:22
	/// <summary>
	/// Lot File DB table object
	/// </summary>
	[Table("lot_file_goszakup")]
	public class LotFileGoszakup
	{
		[Key][Column("id")] public long Id{get; set;}
		[Column("lot_id")] public long? LotId{get; set;}
		[Column("original_name")] public string OriginalName{get; set;}
		[Column("name_ru")] public string NameRu{get; set;}
		[Column("link")] public string Link{get; set;}
	}
}
