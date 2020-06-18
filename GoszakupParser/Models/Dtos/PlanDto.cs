namespace GoszakupParser.Models.Dtos
{
    public class PlanDto
    {
        public long id { get; set; }
        public long plan_act_id { get; set; }
        public string plan_act_number { get; set; }
        public int ref_plan_status_id { get; set; }
        public int plan_fin_year { get; set; }
        public int plan_preliminary { get; set; }
        public long rootrecord_id { get; set; }
        public long sys_subjects_id { get; set; }
        public string subject_biin { get; set; } 
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public int ref_trade_methods_id { get; set; }
        public string ref_units_code { get; set; }
        public float count { get; set; } 
        public float price { get; set; }
        public float amount { get; set; } 
        public int ref_months_id { get; set; }
        public int ref_pln_point_status_id { get; set; }
        public int pln_point_year { get; set; }
        public int ref_subject_types_id { get; set; }
        public string ref_enstru_code { get; set; }
        public int ref_finsource_id { get; set; }
        public int ref_abp_code { get; set; }
        public string date_approved { get; set; }
        public int is_qvazi { get; set; } 
        public string date_create { get; set; }
        public string timestamp { get; set; }
        public int system_id { get; set; }
        public int ref_point_type_id { get; set; }
        public string desc_ru { get; set; }
        public string desc_kz { get; set; }
        public string extra_desc_ru { get; set; }
        public string extra_desc_kz { get; set; }
        public long sum_1 { get; set; }
        public long sum_2 { get; set; }
        public long sum_3 { get; set; }
        public string supply_date_ru { get; set; }
        public float prepayment { get; set; }
        public int ref_justification_id { get; set; }
        public int ref_amendment_agreem_type_id { get; set; }
        public int ref_amendm_agreem_justif_id { get; set; }
        public int contract_prev_point_id { get; set; }
        public int disable_person_id { get; set; }
        public int transfer_sys_subjects_id { get; set; }
        public int transfer_type { get; set; }
        public int ref_budget_type_id { get; set; }
        public string subject_name_kz { get; set; }
        public string subject_name_ru { get; set; }
    }
}