// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
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
}