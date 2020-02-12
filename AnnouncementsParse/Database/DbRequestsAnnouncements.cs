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
        public static void AddContract(AnnouncementDb announcement, NpgsqlConnection connection)
        {
            //TODO(IMPLEMENT)
        }
    }
}