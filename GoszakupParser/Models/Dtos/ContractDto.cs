// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:45:10
    /// <summary>
    /// API contract object
    /// </summary>
    public class ContractDto
    {
        public int id { get; set; }
        public int? parent_id { get; set; }
        public int root_id { get; set; }
        public int? trd_buy_id { get; set; }
        public string trd_buy_number_anno { get; set; }
        public int ref_contract_status_id { get; set; }
        public byte deleted { get; set; }
        public string crdate { get; set; }
        public string last_update_date { get; set; }
        public int? supplier_id { get; set; }
        public string supplier_biin { get; set; }
        public string supplier_bik { get; set; }
        public string supplier_iik { get; set; }
        public string supplier_bank_name_kz { get; set; }
        public string supplier_bank_name_ru { get; set; }
        public string contract_number { get; set; }
        public string sign_reason_doc_name { get; set; }
        public string sign_reason_doc_date { get; set; }
        public string trd_buy_itogi_date_public { get; set; }
        public int? customer_id { get; set; }
        public string customer_bin { get; set; }
        public string customer_bik { get; set; }
        public string customer_iik { get; set; }
        public string customer_bank_name_kz { get; set; }
        public string customer_bank_name_ru { get; set; }
        public string contract_number_sys { get; set; }
        public int fin_year { get; set; }
        public int ref_contract_agr_form_id { get; set; }
        public int ref_contract_year_type_id { get; set; }
        public int? ref_finsource_id { get; set; }
        public string ref_currency_code { get; set; }
        public double contract_sum_wnds { get; set; }
        public string sign_date { get; set; }
        public string ec_end_date { get; set; }
        public string plan_exec_date { get; set; }
        public string fakt_exec_date { get; set; }
        public double? fakt_sum_wnds { get; set; }
        public string contract_end_date { get; set; }
        public int? ref_contract_cancel_id { get; set; }
        public int? ref_contract_type_id { get; set; }
        public string description_kz { get; set; }
        public string description_ru { get; set; }
        public int? fakt_trade_methods_id { get; set; }
        public byte ec_customer_approve { get; set; }
        public byte ec_supplier_approve { get; set; }
        public double? contract_ms { get; set; }
        public string supplier_legal_address { get; set; }
        public string customer_legal_address { get; set; }
        public string payments_terms_ru { get; set; }
        public string payments_terms_kz { get; set; }
        public byte is_gu { get; set; }
        public double exchange_rate { get; set; }
        public int system_id { get; set; }
        public string index_date { get; set; }
    }
}