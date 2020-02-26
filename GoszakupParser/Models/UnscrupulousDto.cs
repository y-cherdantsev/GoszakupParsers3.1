namespace GoszakupParser.Models
{

    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:29:40
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>


    public class UnscrupulousDto
    {
        public int pid { get; set; }
        public string supplier_biin { get; set; } = "";
        public string supplier_innunp { get; set; } = "";
        public string supplier_name_ru { get; set; } = "";
        public string supplier_name_kz { get; set; } = "";
        public int system_id { get; set; }
        public string index_date { get; set; } = "";
    }
}