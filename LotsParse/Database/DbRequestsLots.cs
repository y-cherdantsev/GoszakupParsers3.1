using System;
using System.Linq;
using LotsParse.Units;
using NLog;
using Npgsql;

namespace LotsParse.Database
{
    /*!

@author Yevgeniy Cherdantsev
@date 07.02.2020 12:41:18
@version 1.0
@brief Работа с таблицей лотов госзакупа
    
    */
    public static class DbRequestsLots
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */


        private static string LotsTable { get; } =
            $"{Configuration.DbScheme}.{Configuration.DbLots}"; /*!< Адрес таблицы lots_goszakup */


/*!

@author Yevgeniy Cherdantsev
@date 07.02.2020 12:42:51
@version 1.0
@brief Добавляет нового лота в таблицу
@param[in] lot - лот
@param[in] connection - соединение с базой данных

     */
        public static void AddLot(LotDb lot, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand($"INSERT INTO {LotsTable} (" +
                                        "id, " +
                                        "lot_number, " +
                                        "ref_lot_status_id, " +
                                        "last_update_date, " +
                                        "union_lots, " +
                                        "count, " +
                                        "amount, " +
                                        "name_ru, " +
                                        "name_kz, " +
                                        "description_ru, " +
                                        "description_kz, " +
                                        "customer_id, " +
                                        "customer_bin, " +
                                        "trd_buy_number_anno, " +
                                        "trd_buy_id, " +
                                        "dumping, " +
                                        "dumping_lot_price, " +
                                        "psd_sign, " +
                                        "consulting_services, " +
                                        "is_light_industry, " +
                                        "is_construction_work, " +
                                        "disable_person_id, " +
                                        "customer_name_kz, " +
                                        "customer_name_ru, " +
                                        "ref_trade_methods_id, " +
                                        "index_date, " +
                                        "system_id, " +
                                        "single_org_sign " +
                                        ") VALUES (" +
                                        "@id, " +
                                        "@lot_number, " +
                                        "@ref_lot_status_id, " +
                                        "@last_update_date, " +
                                        "@union_lots, " +
                                        "@count, " +
                                        "@amount, " +
                                        "@name_ru, " +
                                        "@name_kz, " +
                                        "@description_ru, " +
                                        "@description_kz, " +
                                        "@customer_id, " +
                                        "@customer_bin, " +
                                        "@trd_buy_number_anno, " +
                                        "@trd_buy_id, " +
                                        "@dumping, " +
                                        "@dumping_lot_price, " +
                                        "@psd_sign, " +
                                        "@consulting_services, " +
                                        "@is_light_industry, " +
                                        "@is_construction_work, " +
                                        "@disable_person_id, " +
                                        "@customer_name_kz, " +
                                        "@customer_name_ru, " +
                                        "@ref_trade_methods_id, " +
                                        "@index_date, " +
                                        "@system_id, " +
                                        "@single_org_sign " +
                                        ") ON CONFLICT (id) DO UPDATE SET " +
                                        $"relevance = CURRENT_TIMESTAMP",
                connection);

            cmd.Parameters.AddWithValue("@id", lot.id);
            cmd.Parameters.AddWithValue("@lot_number",
                !string.IsNullOrEmpty(lot.lot_number) ? (object) lot.lot_number : DBNull.Value);
            cmd.Parameters.AddWithValue("@ref_lot_status_id", lot.ref_lot_status_id);
            cmd.Parameters.AddWithValue("@last_update_date", lot.last_update_date);
            cmd.Parameters.AddWithValue("@union_lots", lot.union_lots);
            cmd.Parameters.AddWithValue("@count", lot.count);
            cmd.Parameters.AddWithValue("@amount", lot.amount);
            cmd.Parameters.AddWithValue("@name_ru", lot.name_ru);
            cmd.Parameters.AddWithValue("@name_kz", lot.name_kz);
            cmd.Parameters.AddWithValue("@description_ru", lot.description_ru);
            cmd.Parameters.AddWithValue("@description_kz", lot.description_kz);
            cmd.Parameters.AddWithValue("@customer_id", lot.customer_id);
            cmd.Parameters.AddWithValue("@customer_bin", lot.customer_bin);
            cmd.Parameters.AddWithValue("@trd_buy_number_anno", lot.trd_buy_number_anno);
            cmd.Parameters.AddWithValue("@trd_buy_id", lot.trd_buy_id);
            cmd.Parameters.AddWithValue("@dumping", lot.dumping);
            cmd.Parameters.AddWithValue("@dumping_lot_price", lot.dumping_lot_price);
            cmd.Parameters.AddWithValue("@psd_sign", lot.psd_sign);
            cmd.Parameters.AddWithValue("@consulting_services", lot.consulting_services);
            cmd.Parameters.AddWithValue("@is_light_industry", lot.is_light_industry);
            cmd.Parameters.AddWithValue("@is_construction_work", lot.is_construction_work);
            cmd.Parameters.AddWithValue("@disable_person_id", lot.disable_person_id);
            cmd.Parameters.AddWithValue("@customer_name_kz",
                !string.IsNullOrEmpty(lot.customer_name_kz) ? (object) lot.customer_name_kz : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_name_ru",
                !string.IsNullOrEmpty(lot.customer_name_ru) ? (object) lot.customer_name_ru : DBNull.Value);
            cmd.Parameters.AddWithValue("@ref_trade_methods_id", lot.ref_trade_methods_id);
            cmd.Parameters.AddWithValue("@index_date", lot.index_date);
            cmd.Parameters.AddWithValue("@system_id", lot.system_id);
            cmd.Parameters.AddWithValue("@single_org_sign", lot.single_org_sign);
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