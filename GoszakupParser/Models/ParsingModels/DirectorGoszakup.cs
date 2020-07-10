using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:43:05
    /// <summary>
    /// Director Parsing DB table object
    /// </summary>
    [Table("director_goszakup")]
    public class DirectorGoszakup : DbLoggerCategory.Model
    {
        [Key] [Column("id")] public int? Id { get; set; }
        [Column("bin")] public long? Bin { get; set; }
        [Column("iin")] public long? Iin { get; set; }
        [Column("rnn")] public long? Rnn { get; set; }
        [Column("fullname")] public string Fullname { get; set; }
    }
}