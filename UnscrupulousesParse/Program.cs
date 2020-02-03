// ReSharper disable once InvalidXmlDocComment
/**
@mainpage Приложение для парсинга недобросовестных участников источника https://www.goszakup.gov.kz/

Состоит из следующих частей:
- @ref UnscrupulousesParse.Program Запуск парсера
- @ref UnscrupulousesParse.Database Классы для работы с базами данных
- @ref UnscrupulousesParse.Database.DbRequestsUnscrupulouses Класс для работы с таблицей недобросовестных участников госзакупа
- @ref UnscrupulousesParse.Parser Класс парсинга недобросовестных участников
- @ref UnscrupulousesParse.Units Объекты недобросовестных участников
- @ref UnscrupulousesParse.Configuration Глобальный статический класс конфигураций
- @ref UnscrupulousesParse.Units.Unscrupulouse Класс для создания объектов недобросовестных участников
- @ref UnscrupulousesParse.Units.UnscrupulouseDb Класс для преобразования типов и выгрузки в БД
- @ref UnscrupulousesParse.Units.MainResponse Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
using NLog;

namespace UnscrupulousesParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 18:18:49
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
                parser.Parse();
            }
            catch (Exception e)
            {
                Logger.Fatal($"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|;");
                Environment.Exit(1);
            }
        }
    }
}