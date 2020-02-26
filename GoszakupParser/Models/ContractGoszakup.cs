using System.ComponentModel.DataAnnotations.Schema;

namespace GoszakupParser.Models
{


	/// @author Yevgeniy Cherdantsev
	/// @date 26.02.2020 18:43:05
	/// @version 1.0
	/// <summary>
	/// INPUT
	/// </summary>
	[Table("contract_goszakup")]
	public class ContractGoszakup
	{
		[Column("id_contract")] public long? IdContract{get; set;}
		[Column("description_kz")] public string DescriptionKz{get; set;}
		[Column("description_ru")] public string DescriptionRu{get; set;}
		[Column("fakt_sum_wnds")] public double? FaktSumWnds{get; set;}
		[Column("sign_reason_doc_name")] public string SignReasonDocName{get; set;}
		[Column("supplier_bank_name_ru")] public string SupplierBankNameRu{get; set;}
		[Column("supplier_bank_name_kz")] public string SupplierBankNameKz{get; set;}
		[Column("customer_bank_name_ru")] public string CustomerBankNameRu{get; set;}
		[Column("customer_bank_name_kz")] public string CustomerBankNameKz{get; set;}
		
	}
}
