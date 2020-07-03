using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoszakupParser.Models
{
	[Table("parser_monitoring")]
	public class ParserMonitoring
	{
		[Key] [Column("id")] public int? Id{get; set;}
		[Column("name")] public string Name{get; set;}
		[Column("parsed")] public bool Parsed{get; set;}
		[Column("last_migrated")] public DateTime? LastMigrated{get; set;}
		[Column("last_parsed")] public DateTime? LastParsed{get; set;}
		[Column("owner")] public int? Owner{get; set;}
		[Column("active")] public bool Active{get; set;}
		
	}
}
