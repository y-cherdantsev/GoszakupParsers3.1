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
	/// Lot DB table object
	/// </summary>
	[Table("lot_goszakup")]
	public class LotGoszakup
	{
		[Key] [Column("id")] public long Id{get; set;}
		[Column("anno_id")] public long? AnnoId{get; set;}
		[Column("lot_number")] public string LotNumber{get; set;}
		[Column("name_ru")] public string NameRu{get; set;}
		[Column("description_ru")] public string DescriptionRu{get; set;}
		[Column("customer_bin")] public long? CustomerBin{get; set;}
		[Column("amount")] public double? Amount{get; set;}
		[Column("count")] public double? Count{get; set;}
		[Column("lot_status")] public string LotStatus{get; set;}
		[Column("tru_code")] public string TruCode{get; set;}
		[Column("tru_name")] public string TruName{get; set;}
		[Column("tru_description")] public string TruDescription{get; set;}
		[Column("supply_date_ru")] public string SupplyDateRu{get; set;}
		[Column("units")] public string Units{get; set;}
		[Column("delivery_place")] public string DeliveryPlace{get; set;}
	}
}
