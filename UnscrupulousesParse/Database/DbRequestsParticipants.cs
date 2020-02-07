using System;
using System.Linq;
using NLog;
using Npgsql;
using UnscrupulousesParse.Units;

namespace UnscrupulousesParse.Database
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.12.2019 18:46:18
@version 1.0
@brief Работа с таблицей недобросовестных участников госзакупа
    
    */
    public static class DbRequestsUnscrupulouses
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */


        private static string UnscrupulousesTable { get; } =
            $"{Configuration.DbScheme}.{Configuration.DbUnscrupulouses}"; /*!< Адрес таблицы unscrupulouses_goszakup */


/*!

@author Yevgeniy Cherdantsev
@date 03.12.2019 17:53:18
@version 1.0
@brief Добавляет нового недобросовестного участника в таблицу
@param[in] unscrupulous - недобросовестный участник
@param[in] connection - соединение с базой данных

     */
        public static void AddUnscrupulous(UnscrupulousDb unscrupulous, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand($"INSERT INTO {UnscrupulousesTable} (" +
                                        "pid, " +
                                        "supplier_biin, " +
                                        "supplier_innunp, " +
                                        "supplier_name_ru, " +
                                        "supplier_name_kz, " +
                                        "system_id, " +
                                        "index_date" +
                                        ") VALUES (" +
                                        "@pid, " +
                                        "@supplier_biin, " +
                                        "@supplier_innunp, " +
                                        "@supplier_name_ru, " +
                                        "@supplier_name_kz, " +
                                        "@system_id, " +
                                        "@index_date " +
                                        ") ON CONFLICT (pid) DO UPDATE SET " +
                                        $"relevance = CURRENT_TIMESTAMP",
                connection);
            cmd.Parameters.AddWithValue("@pid", unscrupulous.pid);
            cmd.Parameters.AddWithValue("@supplier_biin",
                unscrupulous.supplier_biin != 0 ? (object) unscrupulous.supplier_biin : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_innunp",
                !string.IsNullOrEmpty(unscrupulous.supplier_innunp)
                    ? (object) unscrupulous.supplier_innunp
                    : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_name_ru",
                !string.IsNullOrEmpty(unscrupulous.supplier_name_ru)
                    ? (object) unscrupulous.supplier_name_ru
                    : DBNull.Value);
            cmd.Parameters.AddWithValue("@supplier_name_kz",
                !string.IsNullOrEmpty(unscrupulous.supplier_name_kz)
                    ? (object) unscrupulous.supplier_name_kz
                    : DBNull.Value);
            cmd.Parameters.AddWithValue("@system_id", unscrupulous.system_id);
            cmd.Parameters.AddWithValue("@index_date", unscrupulous.index_date);
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
                var parameters = cmd.Statements[0].InputParameters.Aggregate("\n", (current, npgsqlParameter) => current + $"{npgsqlParameter.ParameterName}:'{npgsqlParameter.Value}'\n");
                Logger.Fatal(
                    $"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|; [Command]: |{cmd.Statements[0].SQL}|; [Parameters]: |{parameters}|");
                Environment.Exit(1);
            }
        }
    }
}