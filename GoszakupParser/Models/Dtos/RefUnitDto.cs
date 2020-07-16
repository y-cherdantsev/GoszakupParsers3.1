// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
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