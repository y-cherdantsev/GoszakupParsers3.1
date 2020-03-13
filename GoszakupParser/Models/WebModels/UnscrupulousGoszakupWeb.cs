using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models.WebModels
{


	/// @author Yevgeniy Cherdantsev
	/// @date 25.02.2020 09:33:46
	/// @version 1.0
	/// <summary>
	/// 'unscrupulous_goszakup' web table
	/// </summary>
	[Table("unscrupulous_goszakup")]
	public class UnscrupulousGoszakupWeb : DbLoggerCategory.Model
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.None)][Column("biin_companies")] public long? BiinCompanies{get; set;}
		[Column("status")] public bool? Status{get; set;}
		[Column("relevance_date")] public DateTime? RelevanceDate{get; set;}
	}
}
