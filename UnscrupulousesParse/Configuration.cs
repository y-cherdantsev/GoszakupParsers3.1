using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Config;
using Npgsql;

namespace UnscrupulousesParse
{
/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:28:01
@version 1.0
@brief Класс глобальных конфигураций и констант
     
@code
     
@endcode
    
    */
    
    public static class Configuration
    {
        
        private static Logger Logger { get; set; } /*!< Логгер текущего класса */
        internal static string DbHost { get; set; } /*!< Хост БД */
        internal static string DbPort { get; set; } /*!< Порт БД */
        internal static string DbUserName { get; set; } /*!< Имя пользователя БД */
        internal static string DbPassword { get; set; } /*!< Пароль пользователя БД */
        internal static string DbName { get; set; } /*!< Имя БД */
        internal static string DbScheme { get; set; } /*!< Имя схемы БД */
        internal static string DbUnscrupulouses { get; set; } /*!< Имя таблицы недобросовестных участников гос. закупа */
        internal static int NumberOfDbConnections { get; set; } /*!< Количество соединений с БД */
        internal static string Url { get; set; } /*!< URL API */
        internal static string AuthToken { get; set; } /*!< окен авторизации API */


        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:56:29
@version 1.0
@brief Загрузка всех конфигураций
@throw Exception - непредвиденные исключение
     
     */
        public static void LoadConfiguration()
        {
            
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Goszakup Unscrupulous Parse";
            
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            
            var getConfiguration = new ConfigurationBuilder().AddJsonFile("Configuration.json").Build();
            
            DbHost = getConfiguration["Configuration:DbHost"];
            DbPort = getConfiguration["Configuration:DbPort"];
            DbUserName = getConfiguration["Configuration:DbUserName"];
            DbPassword = getConfiguration["Configuration:DbPassword"];
            DbName = getConfiguration["Configuration:DbName"];
            DbScheme = getConfiguration["Configuration:DbScheme"];
            DbUnscrupulouses = getConfiguration["Configuration:DbUnscrupulouses"];
            NumberOfDbConnections = Convert.ToInt32(getConfiguration["Configuration:NumberOfDbConnections"]);
            
            if (NumberOfDbConnections > 5)
                NumberOfDbConnections = 5;
            else if (NumberOfDbConnections < 1)
                NumberOfDbConnections = 1;
            
            Url = getConfiguration["Configuration:Url"];
            AuthToken = getConfiguration["Configuration:AuthToken"];
            
            Logger = LogManager.GetCurrentClassLogger();
            
            Logger.Info("Configuration has been loaded");
            Logger.Info("Constants has been loaded");
            
            
        }

        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:57:03
@version 1.0
@brief Создает и возвращает соединение с БД
@return NpgsqlConnection - соединение с БД
     
     */
        
        public static NpgsqlConnection GetNewConnection()
        {
            var connection = new NpgsqlConnection(
                $"Server={DbHost};User Id={DbUserName};Password={DbPassword};Database={DbName};");
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Logger.Error($"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|;");
                throw;
            }
            Logger.Info("Connection to the database has been established");
            return connection;
        }
    }
}