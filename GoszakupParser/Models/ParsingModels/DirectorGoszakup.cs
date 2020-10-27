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
    /// @date 26.02.2020 18:43:05
    /// <summary>
    /// Director Parsing DB table object
    /// </summary>
    [Table("director_goszakup", Schema = "avroradata")]
    public class DirectorGoszakup : BaseModel
    {
        [Key] [Column("id")] public int? Id { get; set; }
        [Column("bin")] public long? Bin { get; set; }
        [Column("iin")] public long? Iin { get; set; }
        [Column("rnn")] public long? Rnn { get; set; }
        [Column("fullname")] public string Fullname { get; set; }
    }
}