using System;
using NLog;
using NpgsqlTypes;

namespace LotsParse.Units
{
/*!
@author Yevgeniy Cherdantsev
@date 31.01.2020 18:40:33
@version 1.0
@brief Объект участника с корректными типами данных 
    
    */

    public class LotDb
    {
        public LotDb(Lot lot)
        {
            id = lot.id;
            lot_number = lot.lot_number;
            ref_lot_status_id = lot.ref_lot_status_id;
            try { index_date = NpgsqlDateTime.Parse(lot.index_date); }catch (Exception) { }
            try { last_update_date = NpgsqlDateTime.Parse(lot.last_update_date); }catch (Exception) { }
            union_lots = lot.union_lots == 1;
            count = lot.count;
            amount = lot.amount;
            name_ru = lot.name_ru;
            name_kz = lot.name_kz;
            description_ru = lot.description_ru;
            description_kz = lot.description_kz;
            customer_id = customer_id;
            Int64.TryParse(lot.customer_bin, out var customerBin);
            customer_bin = customerBin;
            trd_buy_number_anno = lot.trd_buy_number_anno;
            trd_buy_id = lot.trd_buy_id;
            dumping = lot.dumping == 1;
            dumping_lot_price = lot.dumping_lot_price;
            psd_sign = lot.psd_sign;
            consulting_services = lot.consulting_services == 1;
            single_org_sign = lot.singl_org_sign;
            is_light_industry = lot.is_light_industry == 1;
            is_construction_work = lot.is_construction_work == 1;
            disable_person_id = lot.disable_person_id == 1;
            customer_name_kz = lot.customer_name_kz;
            customer_name_ru = lot.customer_name_ru;
            ref_trade_methods_id = lot.ref_trade_methods_id;
            system_id = lot.system_id;
        }
        
        public int id { get; set; }
        public string lot_number { get; set; }
        public int ref_lot_status_id { get; set; }
        public NpgsqlDateTime last_update_date { get; set; }
        public bool union_lots { get; set; }
        public double count { get; set; }
        public double amount { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string description_ru { get; set; }
        public string description_kz { get; set; }
        public int customer_id { get; set; }
        public long customer_bin { get; set; }
        public string trd_buy_number_anno { get; set; }
        public int trd_buy_id { get; set; }
        public bool dumping { get; set; }
        public int dumping_lot_price { get; set; }
        public byte psd_sign { get; set; }
        public bool consulting_services { get; set; }
        public byte single_org_sign { get; set; }
        public bool is_light_industry { get; set; }
        public bool is_construction_work { get; set; }
        public bool disable_person_id { get; set; }
        public string customer_name_kz { get; set; }
        public string customer_name_ru { get; set; }
        public int ref_trade_methods_id { get; set; }
        public NpgsqlDateTime index_date { get; set; }
        public int system_id { get; set; }
    }
}