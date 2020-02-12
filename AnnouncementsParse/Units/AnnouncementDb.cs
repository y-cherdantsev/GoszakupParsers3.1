using System;
using NpgsqlTypes;

namespace AnnouncementsParse.Units
{
/*!
@author Yevgeniy Cherdantsev
@date 12.02.2020 19.12.30
@version 1.0
@brief Объект объявления с корректными типами данных 
    
    */

    public class AnnouncementDb
    {
        public AnnouncementDb(Announcement announcement)
        {
            id = announcement.id;
            number_anno = announcement.number_anno;
            name_ru = announcement.name_ru;
            name_kz = announcement.name_kz;
            total_sum = announcement.total_sum;
            ref_trade_methods_id = announcement.ref_trade_methods_id;
            ref_subject_type_id = announcement.ref_subject_type_id;
            Int64.TryParse(announcement.customer_bin, out var customerBin);
            customer_bin = customerBin;
            customer_pid = announcement.customer_pid;
            Int64.TryParse(announcement.org_bin, out var orgBin);
            org_bin = orgBin;
            org_pid = announcement.org_pid;
            ref_buy_status_id = announcement.ref_buy_status_id;
            ref_type_trade_id = announcement.ref_type_trade_id;
            disable_person_id = announcement.disable_person_id == 1;
            id_supplier = announcement.id_supplier;
            Int64.TryParse(announcement.biin_supplier, out var biinSupplier);
            biin_supplier = biinSupplier;
            parent_id = announcement.parent_id;
            singl_org_sign = announcement.singl_org_sign == 1;
            is_light_industry = announcement.is_light_industry == 1;
            is_construction_work = announcement.is_construction_work == 1;
            customer_name_kz = announcement.customer_name_kz;
            customer_name_ru = announcement.customer_name_ru;
            org_name_kz = announcement.org_name_kz;
            org_name_ru = announcement.org_name_ru;
            system_id = announcement.system_id;
            try { start_date = NpgsqlDateTime.Parse(announcement.start_date); }catch (Exception) { }
            try { repeat_start_date = NpgsqlDateTime.Parse(announcement.repeat_start_date); }catch (Exception) { }
            try { repeat_end_date = NpgsqlDateTime.Parse(announcement.repeat_end_date); }catch (Exception) { }
            try { end_date = NpgsqlDateTime.Parse(announcement.end_date); }catch (Exception) { }
            try { publish_date = NpgsqlDateTime.Parse(announcement.publish_date); }catch (Exception) { }
            try { itogi_date_public = NpgsqlDateTime.Parse(announcement.itogi_date_public); }catch (Exception) { }
            try { discus_start_date = NpgsqlDateTime.Parse(announcement.discus_start_date); }catch (Exception) { }
            try { discus_end_date = NpgsqlDateTime.Parse(announcement.discus_end_date); }catch (Exception) { }
            try { index_date = NpgsqlDateTime.Parse(announcement.index_date); }catch (Exception) { }
        }

        public int id { get; set; }
        public string number_anno { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public double total_sum { get; set; }
        public int ref_trade_methods_id { get; set; }
        public int ref_subject_type_id { get; set; }
        public long customer_bin { get; set; }
        public int customer_pid { get; set; }
        public long org_bin { get; set; }
        public int org_pid { get; set; }
        public int ref_buy_status_id { get; set; }
        public NpgsqlDateTime start_date { get; set; }
        public NpgsqlDateTime repeat_start_date { get; set; }
        public NpgsqlDateTime repeat_end_date { get; set; }
        public NpgsqlDateTime end_date { get; set; }
        public NpgsqlDateTime publish_date { get; set; }
        public NpgsqlDateTime itogi_date_public { get; set; }
        public int ref_type_trade_id { get; set; }
        public bool disable_person_id { get; set; }
        public NpgsqlDateTime discus_start_date { get; set; }
        public NpgsqlDateTime discus_end_date { get; set; }
        public int id_supplier { get; set; }
        public long biin_supplier { get; set; }
        public int parent_id { get; set; }
        public bool singl_org_sign { get; set; }
        public bool is_light_industry { get; set; }
        public bool is_construction_work { get; set; }
        public string customer_name_kz { get; set; }
        public string customer_name_ru { get; set; }
        public string org_name_kz { get; set; }
        public string org_name_ru { get; set; }
        public int system_id { get; set; }
        public NpgsqlDateTime index_date { get; set; }
    }
}