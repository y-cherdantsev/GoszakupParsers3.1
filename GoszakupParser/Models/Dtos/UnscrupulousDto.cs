// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:29:40
    /// <summary>
    /// API unscrupulous object
    /// </summary>
    public class UnscrupulousDto
    {
        public int pid { get; set; }
        public string supplier_biin { get; set; }
        public string supplier_innunp { get; set; }
        public string supplier_name_ru { get; set; }
        public string supplier_name_kz { get; set; }
        public int system_id { get; set; }
        public string index_date { get; set; }
    }
}