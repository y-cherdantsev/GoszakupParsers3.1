using System.Collections.Generic;

namespace GoszakupParser.Models
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:45:45
    /// @version 1.0
    /// <summary>
    /// Объект объявления возвращаемый из API
    /// </summary>
    public class AnnouncementDto
    {
        public int id { get; set; }
        public string number_anno { get; set; } = "";
        public string name_ru { get; set; } = "";
        public string name_kz { get; set; } = "";
        public double total_sum { get; set; }
        public int ref_trade_methods_id { get; set; }
        public int ref_subject_type_id { get; set; }
        public string customer_bin { get; set; } = "";
        public int customer_pid { get; set; }
        public string org_bin { get; set; } = "";
        public int org_pid { get; set; }
        public int ref_buy_status_id { get; set; }
        public string start_date { get; set; } = "";
        public string repeat_start_date { get; set; } = "";
        public string repeat_end_date { get; set; } = "";
        public string end_date { get; set; } = "";
        public string publish_date { get; set; } = "";
        public string itogi_date_public { get; set; } = "";
        public int ref_type_trade_id { get; set; }
        public byte disable_person_id { get; set; }
        public string discus_start_date { get; set; } = "";
        public string discus_end_date { get; set; } = "";
        public int id_supplier { get; set; }
        public string biin_supplier { get; set; } = "";
        public int parent_id { get; set; }
        public byte singl_org_sign { get; set; }
        public byte is_light_industry { get; set; }
        public byte is_construction_work { get; set; }
        public string customer_name_kz { get; set; } = "";
        public string customer_name_ru { get; set; } = "";
        public string org_name_kz { get; set; } = "";
        public string org_name_ru { get; set; } = "";
        public int system_id { get; set; }
        public string index_date { get; set; } = "";
    }

    
}