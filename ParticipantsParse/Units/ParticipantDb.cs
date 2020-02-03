using System;
using NpgsqlTypes;

namespace ParticipantsParse.Units
{
/*!
@author Yevgeniy Cherdantsev
@date 31.01.2020 18:40:33
@version 1.0
@brief Объект участника с корректными типами данных 
    
    */

    public class ParticipantDb
    {
        public ParticipantDb(Participant participant)
        {
            pid = participant.pid;
            Int64.TryParse(participant.bin, out var bin);
            this.bin = bin;
            Int64.TryParse(participant.iin, out var iin);
            this.iin = iin;
            inn = participant.inn;
            unp = participant.unp;
            try { regdate = NpgsqlDateTime.Parse(participant.regdate); }catch (Exception) { }
            try { index_date = NpgsqlDateTime.Parse(participant.index_date); }catch (Exception) { }
            try { last_update_date = NpgsqlDateTime.Parse(participant.last_update_date); }catch (Exception) { }
            crdate = participant.crdate;
            number_reg = participant.number_reg;
            series = participant.series;
            name_ru = participant.name_ru;
            name_kz = participant.name_kz;
            full_name_ru = participant.full_name_ru;
            full_name_kz = participant.full_name_kz;
            country_code = Convert.ToInt32(participant.country_code);
            customer = participant.customer == 1;
            organizer = participant.organizer == 1;
            mark_national_company = participant.mark_national_company == 1;
            ref_kopf_code = participant.ref_kopf_code;
            mark_assoc_with_disab = participant.mark_assoc_with_disab == 1;
            system_id = participant.system_id;
            supplier = participant.supplier == 1;
            type_supplier = participant.type_supplier;
            krp_code = participant.krp_code;
            oked_list = participant.oked_list;
            kse_code = participant.kse_code;
            mark_world_company = participant.mark_world_company == 1;
            mark_state_monopoly = participant.mark_state_monopoly == 1;
            mark_natural_monopoly = participant.mark_natural_monopoly == 1;
            mark_patronymic_producer = participant.mark_patronymic_producer == 1;
            mark_patronymic_supplier = participant.mark_patronymic_supplyer == 1;
            mark_small_employer = participant.mark_small_employer == 1;
            is_single_org = participant.is_single_org == 1;
            email = participant.email;
            phone = participant.phone;
            website = participant.website;
            qvazi = participant.qvazi == 1;
            year = participant.year;
            mark_resident = participant.mark_resident == 1;
        }

        public int pid { get; set; }
        public long? bin { get; set; }
        public long? iin { get; set; }
        public string inn { get; set; }
        public string unp { get; set; }
        public NpgsqlDateTime regdate { get; set; }
        public int crdate { get; set; }
        public NpgsqlDateTime index_date { get; set; }
        public string number_reg { get; set; }
        public string series { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string full_name_ru { get; set; }
        public string full_name_kz { get; set; }
        public int country_code { get; set; }
        public bool customer { get; set; }
        public bool organizer { get; set; }
        public bool mark_national_company { get; set; }
        public string ref_kopf_code { get; set; }
        public bool mark_assoc_with_disab { get; set; }
        public int system_id { get; set; }
        public bool supplier { get; set; }
        public byte type_supplier { get; set; }
        public int krp_code { get; set; }
        public int oked_list { get; set; }
        public int kse_code { get; set; }
        public bool mark_world_company { get; set; }
        public bool mark_state_monopoly { get; set; }
        public bool mark_natural_monopoly { get; set; }
        public bool mark_patronymic_producer { get; set; }
        public bool mark_patronymic_supplier { get; set; }
        public bool mark_small_employer { get; set; }
        public bool is_single_org { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public NpgsqlDateTime last_update_date { get; set; }
        public bool qvazi { get; set; }
        public int year { get; set; }
        public bool mark_resident { get; set; }
    }
}