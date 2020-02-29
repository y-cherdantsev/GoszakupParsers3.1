using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models.ParsingModels
{


	/// @author Yevgeniy Cherdantsev
	/// @date 26.02.2020 18:43:26
	/// @version 1.0
	/// <summary>
	/// INPUT
	/// </summary>
	[Table("unscrupulous_goszakup")]
	public class UnscrupulousGoszakup : DbLoggerCategory.Model
	{
		[Key] [Column("pid")] public int? Pid{get; set;}
		[Column("supplier_biin")] public long? SupplierBiin{get; set;}
		[Column("supplier_innunp")] public string SupplierInnunp{get; set;}
		[Column("supplier_name_ru")] public string SupplierNameRu{get; set;}
		[Column("supplier_name_kz")] public string SupplierNameKz{get; set;}
		[Column("index_date")] public DateTime? IndexDate{get; set;}
		[Column("system_id")] public int? SystemId{get; set;}
		// [Column("relevance")] public DateTime? Relevance{get; set;}
		
	}
}
