// ReSharper disable once InvalidXmlDocComment
/**
@mainpage Приложение для парсинга лотов источника https://www.goszakup.gov.kz/

Состоит из следующих частей:
- @ref LotsParse.Program Запуск парсера
- @ref LotsParse.Database Классы для работы с базами данных
- @ref LotsParse.Database.DbRequestsLots Класс для работы с таблицей лотов госзакупа
- @ref LotsParse.Parser Класс парсинга лотов
- @ref LotsParse.Units Объекты лотов
- @ref LotsParse.Configuration Глобальный статический класс конфигураций
- @ref LotsParse.Units.Lot Класс для создания объектов лотов
- @ref LotsParse.Units.LotDb Класс для преобразования типов и выгрузки в БД
- @ref LotsParse.Units.MainResponse Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace LotsParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 05.02.2020 18.01.20
@version 1.0
@brief Запуск программы
    
    */

    internal static class Program
    {
        private static Logger Logger { get; set; } /*!< Логгер текущего класса */

/*!

@author Yevgeniy Cherdantsev
@date 05.02.2020 18.01.20
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