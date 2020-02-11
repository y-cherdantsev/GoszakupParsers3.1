// ReSharper disable once InvalidXmlDocComment
/**
@mainpage Приложение для парсинга лотов источника https://www.goszakup.gov.kz/

Состоит из следующих частей:
- @ref ContractsParse.Program Запуск парсера
- @ref ContractsParse.Database Классы для работы с базами данных
- @ref ContractsParse.Database.DbRequestsContracts Класс для работы с таблицей договоров госзакупа
- @ref ContractsParse.Parser Класс парсинга договоров
- @ref ContractsParse.Units Объекты договоров
- @ref ContractsParse.Configuration Глобальный статический класс конфигураций
- @ref ContractsParse.Units.Contract Класс для создания объектов договоров
- @ref ContractsParse.Units.ContractDb Класс для преобразования типов и выгрузки в БД
- @ref ContractsParse.Units.MainResponse Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace ContractsParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 13:38:04
@version 1.0
@brief Запуск программы
    
    */

    internal static class Program
    {
        private static Logger Logger { get; set; } /*!< Логгер текущего класса */

/*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 13:38:04
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