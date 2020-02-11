using System;
using System.Linq;
using ContractsParse.Units;
using NLog;
using Npgsql;

namespace ContractsParse.Database
{
    /*!

@author Yevgeniy Cherdantsev
@date 11.02.2020 9:48:24
@version 1.0
@brief Работа с таблицей договоров госзакупа
    
    */
    public static class DbRequestsContracts
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */


        private static string ContractsTable { get; } =
            $"{Configuration.DbScheme}.{Configuration.DbContracts}"; /*!< Адрес таблицы contracts_goszakup */


/*!

@author Yevgeniy Cherdantsev
@date 11.02.2020 9:48:47
@version 1.0
@brief Добавляет нового лота в таблицу
@param[in] lot - лот
@param[in] connection - соединение с базой данных

     */
        public static void AddContract(ContractDb contract, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand($"INSERT INTO {ContractsTable} (" +
                                        "id, " +
                                        "parent_id, " +
                                        "root_id, " +
                                        "trd_buy_id, " +
                                        "trd_buy_number_anno, " +
                                        "ref_contract_status_id, " +
                                        "deleted, " +
                                        "crdate, " +
                                        "last_update_date, " +
                                        "supplier_id, " +
                                        "supplier_biin, " +
                                        "supplier_bik, " +
                                        "supplier_iik, " +
                                        "supplier_bank_name_kz, " +
                                        "supplier_bank_name_ru, " +
                                        "contract_number, " +
                                        "sign_reason_doc_name, " +
                                        "sign_reason_doc_date, " +
                                        "trd_buy_itogi_date_public, " +
                                        "customer_id, " +
                                        "customer_bin, " +
                                        "customer_bik, " +
                                        "customer_iik, " +
                                        "customer_bank_name_kz, " +
                                        "customer_bank_name_ru, " +
                                        "contract_number_sys, " +
                                        "fin_year, " +
                                        "ref_contract_agr_form_id, " +
                                        "ref_contract_year_type_id, " +
                                        "ref_finsource_id, " +
                                        "ref_currency_code, " +
                                        "contract_sum_wnds, " +
                                        "sign_date, " +
                                        "ec_end_date, " +
                                        "plan_exec_date, " +
                                        "fakt_exec_date, " +
                                        "fakt_sum_wnds, " +
                                        "contract_end_date, " +
                                        "ref_contract_cancel_id, " +
                                        "ref_contract_type_id, " +
                                        "description_kz, " +
                                        "description_ru, " +
                                        "fakt_trade_methods_id, " +
                                        "ec_customer_approve, " +
                                        "ec_supplier_approve, " +
                                        "contract_ms, " +
                                        "supplier_legal_address, " +
                                        "customer_legal_address, " +
                                        "payments_terms_ru, " +
                                        "payments_terms_kz, " +
                                        "is_gu, " +
                                        "exchange_rate, " +
                                        "system_id, " +
                                        "index_date " +
                                        ") VALUES (" +
                                        "@id, " +
                                        "@parent_id, " +
                                        "@root_id, " +
                                        "@trd_buy_id, " +
                                        "@trd_buy_number_anno, " +
                                        "@ref_contract_status_id, " +
                                        "@deleted, " +
                                        "@crdate, " +
                                        "@last_update_date, " +
                                        "@supplier_id, " +
                                        "@supplier_biin, " +
                                        "@supplier_bik, " +
                                        "@supplier_iik, " +
                                        "@supplier_bank_name_kz, " +
                                        "@supplier_bank_name_ru, " +
                                        "@contract_number, " +
                                        "@sign_reason_doc_name, " +
                                        "@sign_reason_doc_date, " +
                                        "@trd_buy_itogi_date_public, " +
                                        "@customer_id, " +
                                        "@customer_bin, " +
                                        "@customer_bik, " +
                                        "@customer_iik, " +
                                        "@customer_bank_name_kz, " +
                                        "@customer_bank_name_ru, " +
                                        "@contract_number_sys, " +
                                        "@fin_year, " +
                                        "@ref_contract_agr_form_id, " +
                                        "@ref_contract_year_type_id, " +
                                        "@ref_finsource_id, " +
                                        "@ref_currency_code, " +
                                        "@contract_sum_wnds, " +
                                        "@sign_date, " +
                                        "@ec_end_date, " +
                                        "@plan_exec_date, " +
                                        "@fakt_exec_date, " +
                                        "@fakt_sum_wnds, " +
                                        "@contract_end_date, " +
                                        "@ref_contract_cancel_id, " +
                                        "@ref_contract_type_id, " +
                                        "@description_kz, " +
                                        "@description_ru, " +
                                        "@fakt_trade_methods_id, " +
                                        "@ec_customer_approve, " +
                                        "@ec_supplier_approve, " +
                                        "@contract_ms, " +
                                        "@supplier_legal_address, " +
                                        "@customer_legal_address, " +
                                        "@payments_terms_ru, " +
                                        "@payments_terms_kz, " +
                                        "@is_gu, " +
                                        "@exchange_rate, " +
                                        "@system_id, " +
                                        "@index_date " +
                                        ") " +
                                        // "ON CONFLICT (id) DO UPDATE SET " +
                                        "ON CONFLICT (id) DO NOTHING ",
                // $"relevance = CURRENT_TIMESTAMP",
                connection);

            cmd.Parameters.AddWithValue("@id", contract.id);
            cmd.Parameters.AddWithValue("@parent_id", contract.parent_id != 0 ? (object)contract.parent_id : DBNull.Value);
            cmd.Parameters.AddWithValue("@root_id", contract.root_id);
            cmd.Parameters.AddWithValue("@trd_buy_id",contract.trd_buy_id != 0 ? (object)contract.trd_buy_id : DBNull.Value);
            cmd.Parameters.AddWithValue("@trd_buy_number_anno",!string.IsNullOrEmpty(contract.trd_buy_number_anno) ? (object) contract.trd_buy_number_anno.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@ref_contract_status_id",contract.ref_contract_status_id);
            cmd.Parameters.AddWithValue("@deleted", contract.deleted);
            cmd.Parameters.AddWithValue("@crdate", contract.crdate);
            cmd.Parameters.AddWithValue("@last_update_date",contract.last_update_date);
            cmd.Parameters.AddWithValue("@supplier_id",contract.supplier_id);
            cmd.Parameters.AddWithValue("@supplier_biin",contract.supplier_biin);
            cmd.Parameters.AddWithValue("@supplier_bik",!string.IsNullOrEmpty(contract.supplier_bik) ? (object) contract.supplier_bik.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_iik",!string.IsNullOrEmpty(contract.supplier_iik) ? (object) contract.supplier_iik.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_number",!string.IsNullOrEmpty(contract.contract_number) ? (object) contract.contract_number.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_bank_name_kz",!string.IsNullOrEmpty(contract.supplier_bank_name_kz) ? (object) contract.supplier_bank_name_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_bank_name_ru",!string.IsNullOrEmpty(contract.supplier_bank_name_ru) ? (object) contract.supplier_bank_name_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_number",!string.IsNullOrEmpty(contract.contract_number) ? (object) contract.contract_number.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@sign_reason_doc_name",!string.IsNullOrEmpty(contract.sign_reason_doc_name) ? (object) contract.sign_reason_doc_name.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@sign_reason_doc_date",contract.sign_reason_doc_date);
            cmd.Parameters.AddWithValue("@trd_buy_itogi_date_public",contract.trd_buy_itogi_date_public);
            cmd.Parameters.AddWithValue("@customer_id",contract.customer_id);
            cmd.Parameters.AddWithValue("@customer_bin",contract.customer_bin);
            cmd.Parameters.AddWithValue("@customer_bik",!string.IsNullOrEmpty(contract.customer_bik) ? (object) contract.customer_bik.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_iik",!string.IsNullOrEmpty(contract.customer_iik) ? (object) contract.customer_iik.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_bank_name_kz",!string.IsNullOrEmpty(contract.customer_bank_name_kz) ? (object) contract.customer_bank_name_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_bank_name_ru",!string.IsNullOrEmpty(contract.customer_bank_name_ru) ? (object) contract.customer_bank_name_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_number_sys",!string.IsNullOrEmpty(contract.contract_number_sys) ? (object) contract.contract_number_sys.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@fin_year",contract.fin_year);
            cmd.Parameters.AddWithValue("@ref_contract_agr_form_id",contract.ref_contract_agr_form_id);
            cmd.Parameters.AddWithValue("@ref_contract_year_type_id",contract.ref_contract_year_type_id);
            cmd.Parameters.AddWithValue("@ref_finsource_id",contract.ref_finsource_id);
            cmd.Parameters.AddWithValue("@ref_currency_code",!string.IsNullOrEmpty(contract.ref_currency_code) ? (object) contract.ref_currency_code.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_sum_wnds",contract.contract_sum_wnds);
            cmd.Parameters.AddWithValue("@sign_date",contract.sign_date);
            cmd.Parameters.AddWithValue("@ec_end_date",contract.ec_end_date);
            cmd.Parameters.AddWithValue("@plan_exec_date",contract.plan_exec_date);
            cmd.Parameters.AddWithValue("@fakt_exec_date",contract.fakt_exec_date);
            cmd.Parameters.AddWithValue("@fakt_sum_wnds",contract.fakt_sum_wnds);
            cmd.Parameters.AddWithValue("@contract_end_date",contract.contract_end_date);
            cmd.Parameters.AddWithValue("@ref_contract_cancel_id",contract.ref_contract_cancel_id != 0 ? (object)contract.ref_contract_cancel_id : DBNull.Value);
            cmd.Parameters.AddWithValue("@ref_contract_type_id",contract.ref_contract_type_id);
            cmd.Parameters.AddWithValue("@description_kz",!string.IsNullOrEmpty(contract.description_kz) ? (object) contract.description_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@description_ru",!string.IsNullOrEmpty(contract.description_ru) ? (object) contract.description_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@fakt_trade_methods_id",contract.fakt_trade_methods_id);
            cmd.Parameters.AddWithValue("@ec_customer_approve",contract.ec_customer_approve);
            cmd.Parameters.AddWithValue("@ec_supplier_approve",contract.ec_supplier_approve);
            cmd.Parameters.AddWithValue("@contract_ms",contract.contract_ms > 0 ? (object)contract.contract_ms : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_legal_address",!string.IsNullOrEmpty(contract.supplier_legal_address) ? (object) contract.supplier_legal_address.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_legal_address",!string.IsNullOrEmpty(contract.customer_legal_address) ? (object) contract.customer_legal_address.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@payments_terms_ru",!string.IsNullOrEmpty(contract.payments_terms_ru) ? (object) contract.payments_terms_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@payments_terms_kz",!string.IsNullOrEmpty(contract.payments_terms_kz) ? (object) contract.payments_terms_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@is_gu",contract.is_gu);
            cmd.Parameters.AddWithValue("@exchange_rate",contract.exchange_rate);
            cmd.Parameters.AddWithValue("@system_id",contract.system_id);
            cmd.Parameters.AddWithValue("@index_date",contract.index_date);


            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
                var parameters = cmd.Statements[0].InputParameters.Aggregate("\n",
                    (current, npgsqlParameter) =>
                        current + $"{npgsqlParameter.ParameterName}:'{npgsqlParameter.Value}'\n");
                Logger.Fatal(
                    $"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|; [Command]: |{cmd.Statements[0].SQL}|; [Parameters]: |{parameters}|");
                Environment.Exit(1);
            }
        }
    }
}