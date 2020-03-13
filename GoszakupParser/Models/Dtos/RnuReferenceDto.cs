namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 16:17:39
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class RnuReferenceDto
    {
        public string id { get; set; }
        public int? pid { get; set; }
        public string customer_name_ru { get; set; }
        public string customer_name_kz { get; set; }
        public string customer_biin { get; set; }
        public string supplier_name_ru { get; set; }
        public string supplier_name_kz { get; set; }
        public string supplier_biin { get; set; }
        public string supplier_innunp { get; set; }
        public string supplier_head_name_kz { get; set; }
        public string supplier_head_name_ru { get; set; }
        public string supplier_head_biin { get; set; }
        public string court_decision { get; set; }
        public string court_decision_date { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string ref_reason_id { get; set; }
        public string index_date { get; set; }
        public int system_id { get; set; }
    }
}