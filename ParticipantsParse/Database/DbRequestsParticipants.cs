using System;
using NLog;
using Npgsql;
using ParticipantsParse.Units;

namespace ParticipantsParse.Database
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.12.2019 18:46:18
@version 1.0
@brief Работа с таблицей участников госзакупа
    
    */
    public static class DbRequestsParticipants
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */


        private static string ParticipantsTable { get; } =
            $"{Configuration.DbScheme}.{Configuration.DbParticipants}"; /*!< Адрес таблицы all_participants_goszakup */


/*!

@author Yevgeniy Cherdantsev
@date 03.12.2019 17:53:18
@version 1.0
@brief Добавляет нового участника в таблицу
@param[in] participant - участник
@param[in] connection - соединение с базой данных

     */
        public static void AddParticipant(ParticipantDb participant, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand($"INSERT INTO {ParticipantsTable} (" +
                                        "pid, " +
                                        "bin, " +
                                        "iin, " +
                                        "inn, " +
                                        "unp, " +
                                        "regdate, " +
                                        "crdate, " +
                                        "index_date, " +
                                        "number_reg, " +
                                        "series, " +
                                        "name_ru, " +
                                        "name_kz, " +
                                        "full_name_ru, " +
                                        "full_name_kz, " +
                                        "country_code, " +
                                        "customer, " +
                                        "organizer, " +
                                        "mark_national_company, " +
                                        "ref_kopf_code, " +
                                        "mark_assoc_with_disab, " +
                                        "system_id, " +
                                        "supplier, " +
                                        "type_supplier, " +
                                        "krp_code, " +
                                        "oked_list, " +
                                        "kse_code, " +
                                        "mark_world_company, " +
                                        "mark_state_monopoly, " +
                                        "mark_natural_monopoly, " +
                                        "mark_patronymic_producer, " +
                                        "mark_patronymic_supplier, " +
                                        "mark_small_employer, " +
                                        "is_single_org, " +
                                        "email, " +
                                        "phone, " +
                                        "website, " +
                                        "last_update_date, " +
                                        "qvazi, " +
                                        "year, " +
                                        "mark_resident " +
                                        ") VALUES (" +
                                        "@pid, " +
                                        "@bin, " +
                                        "@iin, " +
                                        "@inn, " +
                                        "@unp, " +
                                        "@regdate, " +
                                        "@crdate, " +
                                        "@index_date, " +
                                        "@number_reg, " +
                                        "@series, " +
                                        "@name_ru, " +
                                        "@name_kz, " +
                                        "@full_name_ru, " +
                                        "@full_name_kz, " +
                                        "@country_code, " +
                                        "@customer, " +
                                        "@organizer, " +
                                        "@mark_national_company, " +
                                        "@ref_kopf_code, " +
                                        "@mark_assoc_with_disab, " +
                                        "@system_id, " +
                                        "@supplier, " +
                                        "@type_supplier, " +
                                        "@krp_code, " +
                                        "@oked_list, " +
                                        "@kse_code, " +
                                        "@mark_world_company, " +
                                        "@mark_state_monopoly, " +
                                        "@mark_natural_monopoly, " +
                                        "@mark_patronymic_producer, " +
                                        "@mark_patronymic_supplier, " +
                                        "@mark_small_employer, " +
                                        "@is_single_org, " +
                                        "@email, " +
                                        "@phone, " +
                                        "@website, " +
                                        "@last_update_date, " +
                                        "@qvazi, " +
                                        "@year, " +
                                        "@mark_resident " +
                                        ") ON CONFLICT (pid) DO UPDATE SET " +
                                        $"relevance = CURRENT_TIMESTAMP",
                connection);
            cmd.Parameters.AddWithValue("@pid", participant.pid);
            cmd.Parameters.AddWithValue("@bin", participant.bin != 0 ? (object) participant.bin : DBNull.Value);
            cmd.Parameters.AddWithValue("@iin", participant.iin != 0 ? (object) participant.iin : DBNull.Value);
            cmd.Parameters.AddWithValue("@inn", !string.IsNullOrEmpty(participant.inn) ? (object) participant.inn : DBNull.Value);
            cmd.Parameters.AddWithValue("@unp", !string.IsNullOrEmpty(participant.unp) ? (object) participant.unp : DBNull.Value);
            cmd.Parameters.AddWithValue("@regdate", participant.regdate);
            cmd.Parameters.AddWithValue("@crdate", participant.crdate);
            cmd.Parameters.AddWithValue("@index_date", participant.index_date);
            cmd.Parameters.AddWithValue("@number_reg", !string.IsNullOrEmpty(participant.number_reg) ? (object) participant.number_reg : DBNull.Value);
            cmd.Parameters.AddWithValue("@series", !string.IsNullOrEmpty(participant.series) ? (object) participant.series : DBNull.Value);
            cmd.Parameters.AddWithValue("@name_ru", !string.IsNullOrEmpty(participant.name_ru) ? (object) participant.name_ru : DBNull.Value);
            cmd.Parameters.AddWithValue("@name_kz", !string.IsNullOrEmpty(participant.name_kz) ? (object) participant.name_kz : DBNull.Value);
            cmd.Parameters.AddWithValue("@full_name_ru", !string.IsNullOrEmpty(participant.full_name_ru) ? (object) participant.full_name_ru : DBNull.Value);
            cmd.Parameters.AddWithValue("@full_name_kz", !string.IsNullOrEmpty(participant.full_name_kz) ? (object) participant.full_name_kz : DBNull.Value);
            cmd.Parameters.AddWithValue("@country_code", participant.country_code);
            cmd.Parameters.AddWithValue("@customer", participant.customer);
            cmd.Parameters.AddWithValue("@organizer", participant.organizer);
            cmd.Parameters.AddWithValue("@mark_national_company", participant.mark_national_company);
            cmd.Parameters.AddWithValue("@ref_kopf_code", !string.IsNullOrEmpty(participant.ref_kopf_code) ? (object) participant.ref_kopf_code : DBNull.Value);
            cmd.Parameters.AddWithValue("@mark_assoc_with_disab", participant.mark_assoc_with_disab);
            cmd.Parameters.AddWithValue("@system_id", participant.system_id);
            cmd.Parameters.AddWithValue("@supplier", participant.supplier);
            cmd.Parameters.AddWithValue("@type_supplier", participant.type_supplier);
            cmd.Parameters.AddWithValue("@krp_code", participant.krp_code);
            cmd.Parameters.AddWithValue("@oked_list", participant.oked_list);
            cmd.Parameters.AddWithValue("@kse_code", participant.kse_code);
            cmd.Parameters.AddWithValue("@mark_world_company", participant.mark_world_company);
            cmd.Parameters.AddWithValue("@mark_state_monopoly", participant.mark_state_monopoly);
            cmd.Parameters.AddWithValue("@mark_natural_monopoly", participant.mark_natural_monopoly);
            cmd.Parameters.AddWithValue("@mark_patronymic_producer", participant.mark_patronymic_producer);
            cmd.Parameters.AddWithValue("@mark_patronymic_supplier", participant.mark_patronymic_supplier);
            cmd.Parameters.AddWithValue("@mark_small_employer", participant.mark_small_employer);
            cmd.Parameters.AddWithValue("@is_single_org", participant.is_single_org);
            cmd.Parameters.AddWithValue("@email", !string.IsNullOrEmpty(participant.email) ? (object) participant.email : DBNull.Value);
            cmd.Parameters.AddWithValue("@phone", !string.IsNullOrEmpty(participant.phone) ? (object) participant.phone : DBNull.Value);
            cmd.Parameters.AddWithValue("@website", !string.IsNullOrEmpty(participant.website) ? (object) participant.website : DBNull.Value);
            cmd.Parameters.AddWithValue("@last_update_date", participant.last_update_date);
            cmd.Parameters.AddWithValue("@qvazi", participant.qvazi);
            cmd.Parameters.AddWithValue("@year", participant.year != 0 ? (object) participant.year : DBNull.Value);
            cmd.Parameters.AddWithValue("@mark_resident", participant.mark_resident);
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
                Logger.Fatal(
                    $"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|; [Command]: |{cmd.CommandText}|");
                Environment.Exit(1);
            }
        }
    }
}