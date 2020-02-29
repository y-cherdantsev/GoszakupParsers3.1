using System;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 17:54:23
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public sealed class ContractParser : ApiSequentialParser<ContractDto, ContractGoszakup>
    {
        public ContractParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings, authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override ContractGoszakup DtoToDb(ContractDto dto)
        {
            var contract = new ContractGoszakup();
            contract.Id = dto.id;
            contract.ParentId = dto.parent_id ?? 0;
            contract.RootId = dto.root_id;
            contract.TrdBuyId = dto.trd_buy_id ?? 0;
            contract.TrdBuyNumberAnno = dto.trd_buy_number_anno;
            contract.RefContractStatusId = dto.ref_contract_status_id;
            contract.Deleted = dto.deleted == 1;
            contract.SupplierId = dto.supplier_id ?? 0;
            Int64.TryParse(dto.supplier_biin, out var supplierBiin); 
            contract.SupplierBiin = supplierBiin;
            contract.SupplierBik = dto.supplier_bik;
            contract.SupplierIik = dto.supplier_iik;
            contract.SupplierBankNameRu = dto.supplier_bank_name_kz;
            contract.SupplierBankNameKz = dto.supplier_bank_name_ru;
            contract.ContractNumber = dto.contract_number;
            contract.SignReasonDocName = dto.sign_reason_doc_name;
            contract.CustomerId = dto.customer_id ?? 0;
            long.TryParse(dto.customer_bin, out var customerBin); 
            contract.CustomerBin = customerBin;
            contract.CustomerBik = dto.customer_bik;
            contract.CustomerIik = dto.customer_iik;
            contract.CustomerBankNameKz = dto.customer_bank_name_kz;
            contract.CustomerBankNameRu = dto.customer_bank_name_ru;
            contract.ContractNumberSys = dto.contract_number_sys;
            contract.RefContractAgrFormId = dto.ref_contract_agr_form_id;
            contract.RefContractYearTypeId = dto.ref_contract_year_type_id;
            contract.RefFinsourceId = dto.ref_finsource_id ?? 0;
            contract.RefCurrencyCode = dto.ref_currency_code;
            contract.ContractSumWnds = dto.contract_sum_wnds;
            contract.FaktSumWnds = dto.fakt_sum_wnds ?? 0;
            contract.RefContractCancelId = dto.ref_contract_cancel_id ?? 0;
            contract.RefContractTypeId = dto.ref_contract_type_id ?? 0;
            contract.DescriptionKz = dto.description_kz;
            contract.DescriptionRu = dto.description_ru;
            contract.FaktTradeMethodsId = dto.fakt_trade_methods_id ?? 0;
            contract.EcCustomerApprove = dto.ec_customer_approve == 1;
            contract.EcSupplierApprove = dto.ec_supplier_approve == 1;
            contract.ContractMs = dto.contract_ms ?? 0;
            contract.SupplierLegalAddress = dto.supplier_legal_address;
            contract.CustomerLegalAddress = dto.customer_legal_address;
            contract.PaymentsTermsRu = dto.payments_terms_ru;
            contract.PaymentsTermsKz = dto.payments_terms_kz;
            contract.IsGu = dto.is_gu == 1;
            contract.ExchangeRate = dto.exchange_rate;
            contract.SystemId = dto.system_id;
            try { contract.Crdate = DateTime.Parse(dto.crdate); }catch (Exception) { }
            try { contract.LastUpdateDate = DateTime.Parse(dto.last_update_date); }catch (Exception) { }
            try { contract.SignReasonDocDate = DateTime.Parse(dto.sign_reason_doc_date); }catch (Exception) { }
            try { contract.TrdBuyItogiDatePublic = DateTime.Parse(dto.trd_buy_itogi_date_public); }catch (Exception) { }
            try { contract.SignDate = DateTime.Parse(dto.sign_date); }catch (Exception) { }
            try { contract.EcEndDate = DateTime.Parse(dto.ec_end_date); }catch (Exception) { }
            try { contract.PlanExecDate = DateTime.Parse(dto.plan_exec_date); }catch (Exception) { }
            try { contract.FaktExecDate = DateTime.Parse(dto.fakt_exec_date); }catch (Exception) { }
            try { contract.ContractEndDate = DateTime.Parse(dto.contract_end_date); }catch (Exception) { }
            try { contract.IndexDate = DateTime.Parse(dto.index_date); }catch (Exception) { }
            try { contract.FinYear = DateTime.Parse($"{dto.fin_year.ToString()}-01-01 00:00:00"); }catch (Exception) { }
            
            return contract;
        }
    }
}