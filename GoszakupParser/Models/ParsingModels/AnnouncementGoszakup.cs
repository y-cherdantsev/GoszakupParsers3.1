using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
	/// @author Yevgeniy Cherdantsev
	/// @date 27.09.2020 11:42:22
	/// <summary>
	/// Announcement DB table object
	/// </summary>
	[Table("announcement_goszakup")]
	public class AnnouncementGoszakup
	{
		[Key] [Column("id")] public long Id{get; set;}
		[Column("number_anno")] public string NumberAnno{get; set;}
		[Column("name_ru")] public string NameRu{get; set;}
		[Column("organizator_biin")] public long? OrganizatorBiin{get; set;}
		[Column("count_lots")] public int? CountLots{get; set;}
		[Column("total_sum")] public double? TotalSum{get; set;}
		[Column("start_date")] public DateTime? StartDate{get; set;}
		[Column("end_date")] public DateTime? EndDate{get; set;}
		[Column("publish_date")] public DateTime? PublishDate{get; set;}
		[Column("type_trade")] public string TypeTrade{get; set;}
		[Column("trade_method")] public string TradeMethod{get; set;}
		[Column("subject_type")] public string SubjectType{get; set;}
		[Column("buy_status")] public string BuyStatus{get; set;}
	}
}
