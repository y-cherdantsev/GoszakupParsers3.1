/**
@mainpage Приложение для парсинга владельцев неблагонадежных компаний

Состоит из следующих частей:
- @ref GoszakupParticipantsParse.Program Запуск парсера
- @ref GoszakupParticipantsParse.Database Классы для работы с базами данных
- @ref GoszakupParticipantsParse.Database.DbRequestsParticipants Класс для работы с таблицами участников госзакупа
- @ref GoszakupParticipantsParse.Parse.ParticipantsParser Класс парсинга участников
- @ref GoszakupParticipantsParse.Units.Settings Статические переменные и настройки парсера
- @ref GoszakupParticipantsParse.Units.Settings.Constants Глобальный статический класс
- @ref GoszakupParticipantsParse.Units.Settings.Configuration Класс конфигураций
- @ref GoszakupParticipantsParse.Units.Participant Классы отображающие свойства участников
- @ref GoszakupParticipantsParse.Units.Participant.Participant Класс для создания объектов участников
- @ref GoszakupParticipantsParse.Units.Participant.MainResponseParticipants Класс для создания объектов возвращаемых api после запроса
- @ref NLog.config Настройки логгера
- @ref Configuration.json Файл с настройками парсера
*/

using System;
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