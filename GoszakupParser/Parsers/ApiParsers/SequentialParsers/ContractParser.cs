using System;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 17:54:23
    /// <summary>
    /// Contract Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class ContractParser : ApiSequentialParser<ContractDto, ContractGoszakup>
    {
        /// <inheritdoc />
        public ContractParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <inheritdoc />
        protected override ContractGoszakup DtoToModel(ContractDto dto)
        {
            long.TryParse(dto.supplier_biin, out var supplierBiin);
            long.TryParse(dto.customer_bin, out var customerBin);
            DateTime.TryParse(dto.crdate, out var crdate);
            DateTime.TryParse(dto.last_update_date, out var lastUpdateDate);
            DateTime.TryParse(dto.sign_reason_doc_date, out var signReasonDocDate);
            DateTime.TryParse(dto.trd_buy_itogi_date_public, out var trdBuyItogiDatePublic);
            DateTime.TryParse(dto.sign_date, out var signDate);
            DateTime.TryParse(dto.ec_end_date, out var ecEndDate);
            DateTime.TryParse(dto.plan_exec_date, out var planExecDate);
            DateTime.TryParse(dto.fakt_exec_date, out var faktExecDate);
            DateTime.TryParse(dto.contract_end_date, out var contractEndDate);
            DateTime.TryParse(dto.index_date, out var indexDate);
            DateTime.TryParse($"{dto.fin_year.ToString()}-01-01 00:00:00", out var finYear);

            var contract = new ContractGoszakup
            {
                Id = dto.id,
                ParentId = dto.parent_id != 0 ? dto.parent_id : null,
                RootId = dto.root_id != 0 ? dto.root_id : (int?) null,
                TrdBuyId = dto.trd_buy_id != 0 ? dto.trd_buy_id : null,
                TrdBuyNumberAnno = !string.IsNullOrEmpty(dto.trd_buy_number_anno) ? dto.trd_buy_number_anno : null,
                RefContractStatusId = dto.ref_contract_status_id,
                Deleted = dto.deleted == 1,
                SupplierId = dto.supplier_id != 0 ? dto.supplier_id : null,
                SupplierBiin = supplierBiin,
                SupplierBik = !string.IsNullOrEmpty(dto.supplier_bik) ? dto.supplier_bik : null,
                SupplierIik = !string.IsNullOrEmpty(dto.supplier_iik) ? dto.supplier_iik : null,
                SupplierBankNameRu =
                    !string.IsNullOrEmpty(dto.supplier_bank_name_kz) ? dto.supplier_bank_name_kz : null,
                SupplierBankNameKz =
                    !string.IsNullOrEmpty(dto.supplier_bank_name_ru) ? dto.supplier_bank_name_ru : null,
                ContractNumber = dto.contract_number,
                SignReasonDocName = dto.sign_reason_doc_name,
                CustomerId = dto.customer_id != 0 ? dto.customer_id : null,
                CustomerBin = customerBin,
                CustomerBik = !string.IsNullOrEmpty(dto.customer_bik) ? dto.customer_bik : null,
                CustomerIik = !string.IsNullOrEmpty(dto.customer_iik) ? dto.customer_iik : null,
                CustomerBankNameKz =
                    !string.IsNullOrEmpty(dto.customer_bank_name_kz) ? dto.customer_bank_name_kz : null,
                CustomerBankNameRu =
                    !string.IsNullOrEmpty(dto.customer_bank_name_ru) ? dto.customer_bank_name_ru : null,
                ContractNumberSys = dto.contract_number_sys,
                RefContractAgrFormId = dto.ref_contract_agr_form_id,
                RefContractYearTypeId = dto.ref_contract_year_type_id,
                RefFinsourceId = dto.ref_finsource_id != 0 ? dto.ref_finsource_id : null,
                RefCurrencyCode = dto.ref_currency_code,
                ContractSumWnds = dto.contract_sum_wnds,
                // ReSharper disable CompareOfFloatsByEqualityOperator
                FaktSumWnds = dto.fakt_sum_wnds != 0 ? dto.fakt_sum_wnds : null,
                ContractMs = dto.contract_ms != 0f ? dto.contract_ms : null,
                // ReSharper restore CompareOfFloatsByEqualityOperator
                RefContractCancelId = dto.ref_contract_cancel_id != 0 ? dto.ref_contract_cancel_id : null,
                RefContractTypeId = dto.ref_contract_type_id != 0 ? dto.ref_contract_type_id : null,
                DescriptionKz = !string.IsNullOrEmpty(dto.description_kz) ? dto.description_kz : null,
                DescriptionRu = !string.IsNullOrEmpty(dto.description_ru) ? dto.description_ru : null,
                FaktTradeMethodsId = dto.fakt_trade_methods_id != 0 ? dto.fakt_trade_methods_id : null,
                EcCustomerApprove = dto.ec_customer_approve == 1,
                EcSupplierApprove = dto.ec_supplier_approve == 1,
                SupplierLegalAddress = !string.IsNullOrEmpty(dto.supplier_legal_address)
                    ? dto.supplier_legal_address
                    : null,
                CustomerLegalAddress = !string.IsNullOrEmpty(dto.customer_legal_address)
                    ? dto.customer_legal_address
                    : null,
                PaymentsTermsRu = !string.IsNullOrEmpty(dto.payments_terms_ru) ? dto.payments_terms_ru : null,
                PaymentsTermsKz = !string.IsNullOrEmpty(dto.payments_terms_kz) ? dto.payments_terms_kz : null,
                IsGu = dto.is_gu == 1,
                ExchangeRate = dto.exchange_rate,
                SystemId = dto.system_id,
                LastUpdateDate = lastUpdateDate,
                SignReasonDocDate = signReasonDocDate,
                Crdate = crdate,
                TrdBuyItogiDatePublic = trdBuyItogiDatePublic,
                SignDate = signDate,
                EcEndDate = ecEndDate,
                PlanExecDate = planExecDate,
                FaktExecDate = faktExecDate,
                ContractEndDate = contractEndDate,
                IndexDate = indexDate,
                FinYear = finYear
            };

            return contract;
        }
    }
}