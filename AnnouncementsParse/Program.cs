// ReSharper disable once InvalidXmlDocComment
/**
@mainpage Приложение для парсинга объявлений(тендеров) источника https://www.goszakup.gov.kz/

Состоит из следующих частей:
- @ref AnnouncementsParse.Program Запуск парсера
- @ref AnnouncementsParse.Database Классы для работы с базами данных
- @ref AnnouncementsParse.Database.DbRequestsAnnouncements Класс для работы с таблицей объявлений госзакупа
- @ref AnnouncementsParse.Parser Класс парсинга объявлений
- @ref AnnouncementsParse.Units Объекты объявлений
- @ref AnnouncementsParse.Configuration Глобальный статический класс конфигураций
- @ref AnnouncementsParse.Units.Announcement Класс для создания объектов объявлений
- @ref AnnouncementsParse.Units.AnnouncementDb Класс для преобразования типов и выгрузки в БД
- @ref AnnouncementsParse.Units.MainResponse Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace AnnouncementsParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 12.02.2020 19:02:53
@version 1.0
@brief Запуск программы
    
    */

    internal static class Program
    {
        private static Logger Logger { get; set; } /*!< Логгер текущего класса */

/*!

@author Yevgeniy Cherdantsev
@date 12.02.2020 19:03:12
@version 1.0
@brief Точка входа в программу
@param[in] args - аргументы (не используются)
@throw - Exception - непредвиденные ошибки
     
     */
        private static void Main(string[] args)
        {
            //Загрузка настроек и констант
            Configuration.LoadConfiguration();

            //Инициализация логгера
            Logger = LogManager.GetCurrentClassLogger();

            //Инициализация и запуск парсера
            var parser = new Parser();
            try
            {
                while (true)
                {
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    var task = new Task(parser.Parse, token);
                    task.Start();

                    var loaded = -1;
                    while (loaded != Parser.TotalLoaded)
                    {
                        loaded = Parser.TotalLoaded;
                        Thread.Sleep(15000);
                        if (task.IsFaulted)
                            throw task.Exception;
                    }

                    if (Parser.LoadedAll)
                        break;
                    Thread.Sleep(15000);
                    tokenSource.Cancel();
                    Logger.Warn($"Restarting parsing process at {Parser.TotalLoaded}");
                }
            }
            catch (Exception e)
            {
                Logger.Fatal($"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|;");
                Environment.Exit(1);
            }
        }
    }
}