using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 09:33:10
    /// <summary>
    /// parser_monitoring Parsing DB table field
    /// </summary>
    [Table("parser_monitoring")]
    public class ParserMonitoring
    {
        [Key] [Column("id")] public int? Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("parsed")] public bool Parsed { get; set; }
        [Column("last_migrated")] public DateTime? LastMigrated { get; set; }
        [Column("last_parsed")] public DateTime? LastParsed { get; set; }
        [Column("owner")] public int? Owner { get; set; }
        [Column("active")] public bool Active { get; set; }
    }
}