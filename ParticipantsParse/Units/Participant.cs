﻿ namespace ParticipantsParse.Units
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:43:27
@version 1.0
@brief Объект возвращаемый из API
    
    */
    public class Participant
    {
        public int pid { get; set; }
        public string bin { get; set; } = "";
        public string iin { get; set; } = "";
        public string inn { get; set; } = "";
        public string unp { get; set; } = "";
        public string regdate { get; set; } = "";
        public int crdate { get; set; }
        public string index_date { get; set; } = "";
        public string number_reg { get; set; } = "";
        public string series { get; set; } = "";
        public string name_ru { get; set; } = "";
        public string name_kz { get; set; } = "";
        public string full_name_ru { get; set; } = "";
        public string full_name_kz { get; set; } = "";
        public string country_code { get; set; } = "";
        public byte customer { get; set; }
        public byte organizer { get; set; }
        public byte mark_national_company { get; set; }
        public string ref_kopf_code { get; set; }
        public byte mark_assoc_with_disab { get; set; }
        public int system_id { get; set; }
        public byte supplier { get; set; }
        public byte type_supplier { get; set; }
        public int krp_code { get; set; }
        public int oked_list { get; set; }
        public int kse_code { get; set; }
        public byte mark_world_company { get; set; }
        public byte mark_state_monopoly { get; set; }
        public byte mark_natural_monopoly { get; set; }
        public byte mark_patronymic_producer { get; set; }
        public byte mark_patronymic_supplyer { get; set; }
        public byte mark_small_employer { get; set; }
        public byte is_single_org { get; set; }
        public string email { get; set; } = "";
        public string phone { get; set; } = "";
        public string website { get; set; } = "";
        public string last_update_date { get; set; } = "";
        public int qvazi { get; set; }
        public int year { get; set; }
        public byte mark_resident { get; set; }

    }
}