using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 16:52:27
    /// <summary>
    /// RefUnit DB table field
    /// </summary>
    [Table("ref_unit_goszakup")]
    public class RefUnitGoszakup : BaseModel
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("code")] public int Code { get; set; }
        [Column("code2")] public string Code2 { get; set; }
        [Column("alpha_code")] public string AlphaCode { get; set; }
    }
}