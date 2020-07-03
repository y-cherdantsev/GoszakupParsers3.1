using System;
using System.Net;
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
        public ContractParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
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
            contract.ParentId = dto.parent_id != 0 ? dto.parent_id : null;
            contract.RootId = dto.root_id != 0 ? dto.root_id : (int?) null;
            contract.TrdBuyId = dto.trd_buy_id != 0 ? dto.trd_buy_id : null;
            contract.TrdBuyNumberAnno = !string.IsNullOrEmpty(dto.trd_buy_number_anno) ? dto.trd_buy_number_anno : null;
            contract.RefContractStatusId = dto.ref_contract_status_id;
            contract.Deleted = dto.deleted == 1;
            contract.SupplierId = dto.supplier_id != 0 ? dto.supplier_id : null;
            long.TryParse(dto.supplier_biin, out var supplierBiin);
            contract.SupplierBiin = supplierBiin;
            contract.SupplierBik = !string.IsNullOrEmpty(dto.supplier_bik) ? dto.supplier_bik : null;
            contract.SupplierIik = !string.IsNullOrEmpty(dto.supplier_iik) ? dto.supplier_iik : null;
            contract.SupplierBankNameRu =
                !string.IsNullOrEmpty(dto.supplier_bank_name_kz) ? dto.supplier_bank_name_kz : null;
            contract.SupplierBankNameKz =
                !string.IsNullOrEmpty(dto.supplier_bank_name_ru) ? dto.supplier_bank_name_ru : null;
            contract.ContractNumber = dto.contract_number;
            contract.SignReasonDocName = dto.sign_reason_doc_name;
            contract.CustomerId = dto.customer_id != 0 ? dto.customer_id : null;
            long.TryParse(dto.customer_bin, out var customerBin);
            contract.CustomerBin = customerBin;
            contract.CustomerBik = !string.IsNullOrEmpty(dto.customer_bik) ? dto.customer_bik : null;
            contract.CustomerIik = !string.IsNullOrEmpty(dto.customer_iik) ? dto.customer_iik : null;
            contract.CustomerBankNameKz =
                !string.IsNullOrEmpty(dto.customer_bank_name_kz) ? dto.customer_bank_name_kz : null;
            contract.CustomerBankNameRu =
                !string.IsNullOrEmpty(dto.customer_bank_name_ru) ? dto.customer_bank_name_ru : null;
            contract.ContractNumberSys = dto.contract_number_sys;
            contract.RefContractAgrFormId = dto.ref_contract_agr_form_id;
            contract.RefContractYearTypeId = dto.ref_contract_year_type_id;
            contract.RefFinsourceId = dto.ref_finsource_id != 0 ? dto.ref_finsource_id : null;
            contract.RefCurrencyCode = dto.ref_currency_code;
            contract.ContractSumWnds = dto.contract_sum_wnds;
            contract.FaktSumWnds = dto.fakt_sum_wnds != 0 ? dto.fakt_sum_wnds : null;
            contract.RefContractCancelId = dto.ref_contract_cancel_id != 0 ? dto.ref_contract_cancel_id : null;
            contract.RefContractTypeId = dto.ref_contract_type_id != 0 ? dto.ref_contract_type_id : null;
            contract.DescriptionKz = !string.IsNullOrEmpty(dto.description_kz) ? dto.description_kz : null;
            contract.DescriptionRu = !string.IsNullOrEmpty(dto.description_ru) ? dto.description_ru : null;
            contract.FaktTradeMethodsId = dto.fakt_trade_methods_id != 0 ? dto.fakt_trade_methods_id : null;
            contract.EcCustomerApprove = dto.ec_customer_approve == 1;
            contract.EcSupplierApprove = dto.ec_supplier_approve == 1;
            contract.ContractMs = dto.contract_ms != 0 ? dto.contract_ms : null;
            contract.SupplierLegalAddress =
                !string.IsNullOrEmpty(dto.supplier_legal_address) ? dto.supplier_legal_address : null;
            contract.CustomerLegalAddress =
                !string.IsNullOrEmpty(dto.customer_legal_address) ? dto.customer_legal_address : null;
            contract.PaymentsTermsRu = !string.IsNullOrEmpty(dto.payments_terms_ru) ? dto.payments_terms_ru : null;
            contract.PaymentsTermsKz = !string.IsNullOrEmpty(dto.payments_terms_kz) ? dto.payments_terms_kz : null;
            contract.IsGu = dto.is_gu == 1;
            contract.ExchangeRate = dto.exchange_rate;
            contract.SystemId = dto.system_id;
            try
            {
                contract.Crdate = DateTime.Parse(dto.crdate);
            }
            catch (Exception)
            {
                contract.Crdate = null;
            }

            try
            {
                contract.LastUpdateDate = DateTime.Parse(dto.last_update_date);
            }
            catch (Exception)
            {
                contract.LastUpdateDate = null;
            }

            try
            {
                contract.SignReasonDocDate = DateTime.Parse(dto.sign_reason_doc_date);
            }
            catch (Exception)
            {
                contract.SignReasonDocDate = null;
            }

            try
            {
                contract.TrdBuyItogiDatePublic = DateTime.Parse(dto.trd_buy_itogi_date_public);
            }
            catch (Exception)
            {
                contract.TrdBuyItogiDatePublic = null;
            }

            try
            {
                contract.SignDate = DateTime.Parse(dto.sign_date);
            }
            catch (Exception)
            {
                contract.SignDate = null;
            }

            try
            {
                contract.EcEndDate = DateTime.Parse(dto.ec_end_date);
            }
            catch (Exception)
            {
                contract.EcEndDate = null;
            }

            try
            {
                contract.PlanExecDate = DateTime.Parse(dto.plan_exec_date);
            }
            catch (Exception)
            {
                contract.PlanExecDate = null;
            }

            try
            {
                contract.FaktExecDate = DateTime.Parse(dto.fakt_exec_date);
            }
            catch (Exception)
            {
                contract.FaktExecDate = null;
            }

            try
            {
                contract.ContractEndDate = DateTime.Parse(dto.contract_end_date);
            }
            catch (Exception)
            {
                contract.ContractEndDate = null;
            }

            try
            {
                contract.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
                contract.IndexDate = null;
            }

            try
            {
                contract.FinYear = DateTime.Parse($"{dto.fin_year.ToString()}-01-01 00:00:00");
            }
            catch (Exception)
            {
                contract.FinYear = null;
            }

            return contract;
        }
    }
}