﻿using System;
using NpgsqlTypes;

namespace ContractsParse.Units
{
/*!
@author Yevgeniy Cherdantsev
@date 10.02.2020 19:39:42
@version 1.0
@brief Объект договора с корректными типами данных 
    
    */

    public class ContractDb
    {
        public ContractDb(Contract contract)
        {
            id = contract.id;
            parent_id = contract.parent_id ?? 0;
            root_id = contract.root_id;
            trd_buy_id = contract.trd_buy_id ?? 0;
            trd_buy_number_anno = contract.trd_buy_number_anno;
            ref_contract_status_id = contract.ref_contract_status_id;
            deleted = contract.deleted == 1;
            supplier_id = contract.supplier_id;

            supplier_bik = contract.supplier_bik;
            supplier_iik = contract.supplier_iik;
            supplier_bank_name_kz = contract.supplier_bank_name_kz;
            supplier_bank_name_ru = contract.supplier_bank_name_ru;
            contract_number = contract.contract_number;
            sign_reason_doc_name = contract.sign_reason_doc_name;
            customer_id = contract.customer_id;
            
            customer_bik = contract.customer_bik;
            customer_iik = contract.customer_iik;
            customer_bank_name_kz = contract.customer_bank_name_kz;
            customer_bank_name_ru = contract.customer_bank_name_ru;
            contract_number_sys = contract.contract_number_sys;
            fin_year = contract.fin_year;
            ref_contract_agr_form_id = contract.ref_contract_agr_form_id;
            ref_contract_year_type_id = contract.ref_contract_year_type_id;
            ref_finsource_id = contract.ref_finsource_id ?? 0;
            ref_currency_code = contract.ref_currency_code;
            contract_sum_wnds = contract.contract_sum_wnds;
            fakt_sum_wnds = contract.fakt_sum_wnds ?? 0;
        }

        public int id { get; set; }
        public int parent_id { get; set; }
        public int root_id { get; set; }
        public int trd_buy_id { get; set; }
        public string trd_buy_number_anno { get; set; }
        public int ref_contract_status_id { get; set; }
        public bool deleted { get; set; }
        public NpgsqlDateTime crdate { get; set; }
        public NpgsqlDateTime last_update_date { get; set; }
        public int supplier_id { get; set; }
        public long supplier_biin { get; set; }
        public string supplier_bik { get; set; }
        public string supplier_iik { get; set; }
        public string supplier_bank_name_kz { get; set; }
        public string supplier_bank_name_ru { get; set; }
        public string contract_number { get; set; }
        public string sign_reason_doc_name { get; set; }
        public NpgsqlDateTime sign_reason_doc_date { get; set; }
        public NpgsqlDateTime trd_buy_itogi_date_public { get; set; }
        public int customer_id { get; set; }
        public long customer_bin { get; set; }
        public string customer_bik { get; set; }
        public string customer_iik { get; set; }
        public string customer_bank_name_kz { get; set; }
        public string customer_bank_name_ru { get; set; }
        public string contract_number_sys { get; set; }
        public int fin_year { get; set; }
        public int ref_contract_agr_form_id { get; set; }
        public int ref_contract_year_type_id { get; set; }
        public int ref_finsource_id { get; set; }
        public string ref_currency_code { get; set; }
        public double contract_sum_wnds { get; set; }
        public NpgsqlDateTime sign_date { get; set; }
        public NpgsqlDateTime ec_end_date { get; set; }
        public NpgsqlDateTime plan_exec_date { get; set; }
        public NpgsqlDateTime fakt_exec_date { get; set; }
        public double fakt_sum_wnds { get; set; }
        public NpgsqlDateTime contract_end_date { get; set; }
        public int ref_contract_cancel_id { get; set; }
        public int ref_contract_type_id { get; set; }
        public string description_kz { get; set; }
        public string description_ru { get; set; }
        public int fakt_trade_methods_id { get; set; }
        public bool ec_customer_approve { get; set; }
        public bool ec_supplier_approve { get; set; }
        public double contract_ms { get; set; }
        public string supplier_legal_address { get; set; }
        public string customer_legal_address { get; set; }
        public string payments_terms_ru { get; set; }
        public string payments_terms_kz { get; set; }
        public bool is_gu { get; set; }
        public double exchange_rate { get; set; }
        public int system_id { get; set; }
        public NpgsqlDateTime index_date { get; set; }
    }
}