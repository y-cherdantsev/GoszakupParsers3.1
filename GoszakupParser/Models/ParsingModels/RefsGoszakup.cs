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


    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 15:59:23
    /// <summary>
    /// RefContractStatus DB table field
    /// </summary>
    [Table("ref_contract_status_goszakup")]
    public class RefContractStatusGoszakup : BaseModel
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("code")] public string Code { get; set; }
    }


    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 15:59:23
    /// <summary>
    /// RefContractType DB table field
    /// </summary>
    [Table("ref_contract_type_goszakup")]
    public class RefContractTypeGoszakup : BaseModel
    {
        [Key] [Column("id")] public long? Id { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
    }

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

    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 15:59:23
    /// <summary>
    /// RefTradeMethod DB table field
    /// </summary>
    [Table("ref_trade_method_goszakup")]
    public class RefTradeMethodGoszakup : BaseModel
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