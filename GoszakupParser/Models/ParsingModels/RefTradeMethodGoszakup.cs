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
    /// @date 10.07.2020 15:59:23
    /// <summary>
    /// RefTradeMethod DB table field
    /// </summary>
    [Table("ref_trade_method_goszakup")]
    public class RefTradeMethodGoszakup : DbLoggerCategory.Model
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("symbol_code")] public string SymbolCode { get; set; }
        [Column("code")] public int Code { get; set; }
        [Column("is_active")] public bool IsActive { get; set; }
        [Column("type")] public int Type { get; set; }
        [Column("f1")] public int F1 { get; set; }
        [Column("ord")] public int Ord { get; set; }
        [Column("f2")] public int F2 { get; set; }
    }
}