using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models
{


	/// @author Yevgeniy Cherdantsev
	/// @date 26.02.2020 18:43:05
	/// @version 1.0
	/// <summary>
	/// INPUT
	/// </summary>
	[Table("contract_goszakup")]
	public class ContractGoszakup: DbLoggerCategory.Model
	{
		[Key] [Column("id")] public int? Id{get; set;}
		[Column("parent_id")] public int? ParentId{get; set;}
		[Column("root_id")] public int? RootId{get; set;}
		[Column("trd_buy_id")] public int? TrdBuyId{get; set;}
		[Column("trd_buy_number_anno")] public string TrdBuyNumberAnno{get; set;}
		[Column("ref_contract_status_id")] public int? RefContractStatusId{get; set;}
		[Column("deleted")] public bool? Deleted{get; set;}
		[Column("crdate")] public DateTime? Crdate{get; set;}
		[Column("last_update_date")] public DateTime? LastUpdateDate{get; set;}
		[Column("supplier_id")] public int? SupplierId{get; set;}
		[Column("supplier_biin")] public long? SupplierBiin{get; set;}
		[Column("supplier_bik")] public string SupplierBik{get; set;}
		[Column("supplier_iik")] public string SupplierIik{get; set;}
		[Column("supplier_bank_name_kz")] public string SupplierBankNameKz{get; set;}
		[Column("supplier_bank_name_ru")] public string SupplierBankNameRu{get; set;}
		[Column("contract_number")] public string ContractNumber{get; set;}
		[Column("sign_reason_doc_name")] public string SignReasonDocName{get; set;}
		[Column("sign_reason_doc_date")] public DateTime? SignReasonDocDate{get; set;}
		[Column("trd_buy_itogi_date_public")] public DateTime? TrdBuyItogiDatePublic{get; set;}
		[Column("customer_id")] public int? CustomerId{get; set;}
		[Column("customer_bin")] public long? CustomerBin{get; set;}
		[Column("customer_bik")] public string CustomerBik{get; set;}
		[Column("customer_iik")] public string CustomerIik{get; set;}
		[Column("customer_bank_name_kz")] public string CustomerBankNameKz{get; set;}
		[Column("customer_bank_name_ru")] public string CustomerBankNameRu{get; set;}
		[Column("contract_number_sys")] public string ContractNumberSys{get; set;}
		[Column("fin_year")] public DateTime? FinYear{get; set;}
		[Column("ref_contract_agr_form_id")] public int? RefContractAgrFormId{get; set;}
		[Column("ref_contract_year_type_id")] public int? RefContractYearTypeId{get; set;}
		[Column("ref_finsource_id")] public int? RefFinsourceId{get; set;}
		[Column("ref_currency_code")] public string RefCurrencyCode{get; set;}
		[Column("contract_sum_wnds")] public double? ContractSumWnds{get; set;}
		[Column("sign_date")] public DateTime? SignDate{get; set;}
		[Column("ec_end_date")] public DateTime? EcEndDate{get; set;}
		[Column("plan_exec_date")] public DateTime? PlanExecDate{get; set;}
		[Column("fakt_sum_wnds")] public double? FaktSumWnds{get; set;}
		[Column("contract_end_date")] public DateTime? ContractEndDate{get; set;}
		[Column("ref_contract_cancel_id")] public int? RefContractCancelId{get; set;}
		[Column("ref_contract_type_id")] public int? RefContractTypeId{get; set;}
		[Column("description_kz")] public string DescriptionKz{get; set;}
		[Column("description_ru")] public string DescriptionRu{get; set;}
		[Column("fakt_trade_methods_id")] public int? FaktTradeMethodsId{get; set;}
		[Column("ec_customer_approve")] public bool? EcCustomerApprove{get; set;}
		[Column("ec_supplier_approve")] public bool? EcSupplierApprove{get; set;}
		[Column("contract_ms")] public double? ContractMs{get; set;}
		[Column("supplier_legal_address")] public string SupplierLegalAddress{get; set;}
		[Column("customer_legal_address")] public string CustomerLegalAddress{get; set;}
		[Column("payments_terms_ru")] public string PaymentsTermsRu{get; set;}
		[Column("payments_terms_kz")] public string PaymentsTermsKz{get; set;}
		[Column("is_gu")] public bool? IsGu{get; set;}
		[Column("exchange_rate")] public double? ExchangeRate{get; set;}
		[Column("system_id")] public int? SystemId{get; set;}
		[Column("index_date")] public DateTime? IndexDate{get; set;}
		[Column("fakt_exec_date")] public DateTime? FaktExecDate{get; set;}
		// [Column("relevance")] public DateTime? Relevance{get; set;}
		
	}
}
