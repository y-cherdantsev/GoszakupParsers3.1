using System;
using System.Linq;
using AnnouncementsParse.Units;
using NLog;
using Npgsql;

namespace AnnouncementsParse.Database
{
    /*!

@author Yevgeniy Cherdantsev
@date 12.02.2020 19:56:24
@version 1.0
@brief Работа с таблицей объявлений госзакупа
    
    */
    public static class DbRequestsAnnouncements
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */


        private static string AnnouncementsTable { get; } =
            $"{Configuration.DbScheme}.{Configuration.DbAnnouncements}"; /*!< Адрес таблицы announcements_goszakup */


/*!

@author Yevgeniy Cherdantsev
@date 12.02.2020 19:57:02
@version 1.0
@brief Добавляет новое объявление в таблицу
@param[in] AnnouncementDb - объявление
@param[in] connection - соединение с базой данных

     */
        public static void AddAnnouncement(AnnouncementDb announcement, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand($"INSERT INTO {AnnouncementsTable} (" +
                                        "id, " +
                                        "number_anno, " +
                                        "name_ru, " +
                                        "name_kz, " +
                                        "total_sum, " +
                                        "ref_trade_methods_id, " +
                                        "ref_subject_type_id, " +
                                        "customer_bin, " +
                                        "customer_pid, " +
                                        "org_bin, " +
                                        "org_pid, " +
                                        "ref_buy_status_id, " +
                                        "start_date, " +
                                        "repeat_start_date, " +
                                        "repeat_end_date, " +
                                        "end_date, " +
                                        "publish_date, " +
                                        "itogi_date_public, " +
                                        "ref_type_trade_id, " +
                                        "disable_person_id, " +
                                        "discus_start_date, " +
                                        "discus_end_date, " +
                                        "id_supplier, " +
                                        "biin_supplier, " +
                                        "parent_id, " +
                                        "single_org_sign, " +
                                        "is_light_industry, " +
                                        "is_construction_work, " +
                                        "customer_name_kz, " +
                                        "customer_name_ru, " +
                                        "org_name_kz, " +
                                        "org_name_ru, " +
                                        "system_id, " +
                                        "index_date " +
                                        ") VALUES (" +
                                        "@id, " +
                                        "@number_anno, " +
                                        "@name_ru, " +
                                        "@name_kz, " +
                                        "@total_sum, " +
                                        "@ref_trade_methods_id, " +
                                        "@ref_subject_type_id, " +
                                        "@customer_bin, " +
                                        "@customer_pid, " +
                                        "@org_bin, " +
                                        "@org_pid, " +
                                        "@ref_buy_status_id, " +
                                        "@start_date, " +
                                        "@repeat_start_date, " +
                                        "@repeat_end_date, " +
                                        "@end_date, " +
                                        "@publish_date, " +
                                        "@itogi_date_public, " +
                                        "@ref_type_trade_id, " +
                                        "@disable_person_id, " +
                                        "@discus_start_date, " +
                                        "@discus_end_date, " +
                                        "@id_supplier, " +
                                        "@biin_supplier, " +
                                        "@parent_id, " +
                                        "@single_org_sign, " +
                                        "@is_light_industry, " +
                                        "@is_construction_work, " +
                                        "@customer_name_kz, " +
                                        "@customer_name_ru, " +
                                        "@org_name_kz, " +
                                        "@org_name_ru, " +
                                        "@system_id, " +
                                        "@index_date " +
                                        ") " +
                                        "ON CONFLICT (id) DO NOTHING ",
                connection);

            cmd.Parameters.AddWithValue("@id", announcement.id);
            cmd.Parameters.AddWithValue("@number_anno",!string.IsNullOrEmpty(announcement.number_anno) ? (object) announcement.number_anno.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@name_ru",!string.IsNullOrEmpty(announcement.name_ru) ? (object) announcement.name_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@name_kz",!string.IsNullOrEmpty(announcement.name_kz) ? (object) announcement.name_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@total_sum",announcement.total_sum);
            cmd.Parameters.AddWithValue("@ref_trade_methods_id",announcement.ref_trade_methods_id);
            cmd.Parameters.AddWithValue("@ref_subject_type_id",announcement.ref_subject_type_id);
            cmd.Parameters.AddWithValue("@customer_bin",announcement.customer_bin != 0 ? (object)announcement.customer_bin : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_pid",announcement.customer_pid != 0 ? (object)announcement.customer_pid : DBNull.Value);
            cmd.Parameters.AddWithValue("@org_bin",announcement.org_bin);
            cmd.Parameters.AddWithValue("@org_pid",announcement.org_pid);
            cmd.Parameters.AddWithValue("@ref_buy_status_id",announcement.ref_buy_status_id);
            cmd.Parameters.AddWithValue("@start_date",announcement.start_date);
            cmd.Parameters.AddWithValue("@repeat_start_date",announcement.repeat_start_date);
            cmd.Parameters.AddWithValue("@repeat_end_date",announcement.repeat_end_date);
            cmd.Parameters.AddWithValue("@end_date",announcement.end_date);
            cmd.Parameters.AddWithValue("@publish_date",announcement.publish_date);
            cmd.Parameters.AddWithValue("@itogi_date_public",announcement.itogi_date_public);
            cmd.Parameters.AddWithValue("@ref_type_trade_id",announcement.ref_type_trade_id);
            cmd.Parameters.AddWithValue("@disable_person_id",announcement.disable_person_id);
            cmd.Parameters.AddWithValue("@discus_start_date",announcement.discus_start_date);
            cmd.Parameters.AddWithValue("@discus_end_date",announcement.discus_end_date);
            cmd.Parameters.AddWithValue("@id_supplier",announcement.id_supplier != 0 ? (object)announcement.id_supplier : DBNull.Value);
            cmd.Parameters.AddWithValue("@biin_supplier",announcement.biin_supplier != 0 ? (object)announcement.biin_supplier : DBNull.Value);
            cmd.Parameters.AddWithValue("@parent_id",announcement.parent_id != 0 ? (object)announcement.parent_id : DBNull.Value);
            cmd.Parameters.AddWithValue("@single_org_sign",announcement.single_org_sign);
            cmd.Parameters.AddWithValue("@is_light_industry",announcement.is_light_industry);
            cmd.Parameters.AddWithValue("@is_construction_work",announcement.is_construction_work);
            cmd.Parameters.AddWithValue("@customer_name_kz",!string.IsNullOrEmpty(announcement.customer_name_kz) ? (object) announcement.customer_name_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@customer_name_ru",!string.IsNullOrEmpty(announcement.customer_name_ru) ? (object) announcement.customer_name_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@org_name_kz",!string.IsNullOrEmpty(announcement.org_name_kz) ? (object) announcement.org_name_kz.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@org_name_ru",!string.IsNullOrEmpty(announcement.org_name_ru) ? (object) announcement.org_name_ru.Trim() : DBNull.Value);
            cmd.Parameters.AddWithValue("@system_id",announcement.system_id);
            cmd.Parameters.AddWithValue("@index_date",announcement.index_date);
            
            
            

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