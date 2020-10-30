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
	/// @date 27.09.2020 11:42:22
	/// <summary>
	/// Announcement File DB table object
	/// </summary>
	[Table("announcement_file_goszakup", Schema = "avroradata")]
	public class AnnouncementFileGoszakup
	{
		[Key] [Column("id")] public long Id{get; set;}
		[Column("anno_id")] public long? AnnoId{get; set;}
		[Column("original_name")] public string OriginalName{get; set;}
		[Column("name_ru")] public string NameRu{get; set;}
		[Column("link")] public string Link{get; set;}
	}
}
