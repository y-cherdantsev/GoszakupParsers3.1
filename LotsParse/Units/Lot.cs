﻿﻿ namespace LotsParse.Units
{
    /*!

@author Yevgeniy Cherdantsev
@date 05.02.2020 19:08:27
@version 1.0
@brief Объект возвращаемый из API
    
    */
    public class Lot
    {
        //TODO()
        public int id { get; set; }
        public string lot_number { get; set; } = "";
        public int ref_lot_status_id { get; set; }
        public string last_update_date { get; set; } = "";
        public int union_lots { get; set; }
        public double count { get; set; }
        public double amount { get; set; }
        public string name_ru { get; set; } = "";
        public string name_kz { get; set; } = "";
        public string description_ru { get; set; } = "";
        public string description_kz { get; set; } = "";
        public int customer_id { get; set; }
        public string customer_bin { get; set; } = "";
        public string trd_buy_number_anno { get; set; } = "";
        public int trd_buy_id { get; set; }
        public int dumping { get; set; }
        public int dumping_lot_price { get; set; }
        public byte psd_sign { get; set; }
        public int consulting_services { get; set; }
        public byte singl_org_sign { get; set; }
        public byte is_light_industry { get; set; }
        public byte is_construction_work { get; set; }
        public byte disable_person_id { get; set; }
        public string customer_name_kz { get; set; } = "";
        public string customer_name_ru { get; set; } = "";
        public int ref_trade_methods_id { get; set; }
        public string index_date { get; set; } = "";
        public int system_id { get; set; }
    }
}