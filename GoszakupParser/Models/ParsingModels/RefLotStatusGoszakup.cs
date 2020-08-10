using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 15:59:23
    /// <summary>
    /// RefLotStatus DB table field
    /// </summary>
    [Table("ref_lot_status_goszakup")]
    public class RefLotStatusGoszakup : BaseModel
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("code")] public string Code { get; set; }
    }
}