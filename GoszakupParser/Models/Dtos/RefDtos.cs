// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 16:02:17
    /// <summary>
    /// API ref buy status object
    /// </summary>
    public class RefBuyStatusDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string code { get; set; }
    }

    /// @author Yevgeniy Cherdantsev
    /// @date 14.09.2020 14:04:30
    /// <summary>
    /// API ref contract status object
    /// </summary>
    public class RefContractStatusDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string code { get; set; }
    }

    /// @author Yevgeniy Cherdantsev
    /// @date 14.09.2020 14:04:30
    /// <summary>
    /// API ref contract type object
    /// </summary>
    public class RefContractTypeDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
    }

    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 16:02:17
    /// <summary>
    /// API ref lot status object
    /// </summary>
    public class RefLotStatusDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string code { get; set; }
    }
    
    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 16:02:17
    /// <summary>
    /// API ref trade method object
    /// </summary>
    public class RefTradeMethodDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string symbol_code { get; set; }
        public int code { get; set; }
        public bool is_active { get; set; }
        public int type { get; set; }
        public int f1 { get; set; }
        public int ord { get; set; }
        public int f2 { get; set; }
    }
    
    /// @author Yevgeniy Cherdantsev
    /// @date 10.07.2020 16:02:17
    /// <summary>
    /// API ref unit object
    /// </summary>
    public class RefUnitDto
    {
        public long id { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public int code { get; set; }
        public string code2 { get; set; }
        public string alpha_code { get; set; }
    }
    
}