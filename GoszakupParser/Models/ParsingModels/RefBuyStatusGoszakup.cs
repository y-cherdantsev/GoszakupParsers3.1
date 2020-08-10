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
    /// RefBuyStatus DB table field
    /// </summary>
    [Table("ref_buy_status_goszakup")]
    public class RefBuyStatusGoszakup : BaseModel
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("code")] public string Code { get; set; }
    }
}