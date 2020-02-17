// ReSharper disable once InvalidXmlDocComment
/**
@mainpage Приложение для парсинга участников источника https://www.goszakup.gov.kz/

Состоит из следующих частей:
- @ref ParticipantsParse.Program Запуск парсера
- @ref ParticipantsParse.Database Классы для работы с базами данных
- @ref ParticipantsParse.Database.DbRequestsParticipants Класс для работы с таблицей участников госзакупа
- @ref ParticipantsParse.Parser Класс парсинга участников
- @ref ParticipantsParse.Units Объекты участников
- @ref ParticipantsParse.Configuration Глобальный статический класс конфигураций
- @ref ParticipantsParse.Units.Participant Класс для создания объектов участников
- @ref ParticipantsParse.Units.ParticipantDb Класс для преобразования типов и выгрузки в БД
- @ref ParticipantsParse.Units.MainResponse Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace ParticipantsParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:30:44
@version 1.0
@brief Запуск программы
    
    */

    internal static class Program
    {
        private static Logger Logger { get; set; } /*!< Логгер текущего класса */

/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16.32.22
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