using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models.ParsingModels
{
	[Table("director_goszakup")]
	public class DirectorGoszakup: DbLoggerCategory.Model
	{
		[Key] [Column("id")] public int? Id{get; set;}
		[Column("bin")] public long? Bin{get; set;}
		[Column("iin")] public long? Iin{get; set;}
		[Column("rnn")] public long? Rnn{get; set;}
		[Column("fullname")] public string Fullname{get; set;}
		// [Column("relevance")] public DateTime? Relevance{get; set;}
		
	}
}
