using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 12:13:14
    /// <summary>
    /// Contract Unit Goszakup DB table object
    /// </summary>
    [Table("contract_unit_goszakup")]
    public class ContractUnitGoszakup
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("source_unique_id")] public long? SourceUniqueId { get; set; }
        [Column("contract_id")] public long? ContractId { get; set; }
        [Column("item_price")] public double? ItemPrice { get; set; }
        [Column("item_price_wnds")] public double? ItemPriceWnds { get; set; }
        [Column("quantity")] public double? Quantity { get; set; }
        [Column("total_sum")] public double? TotalSum { get; set; }
        [Column("total_sum_wnds")] public double? TotalSumWnds { get; set; }
    }
}