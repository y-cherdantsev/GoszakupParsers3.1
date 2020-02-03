﻿﻿using System;
  using System.Text;
  using Microsoft.Extensions.Configuration;
  using NLog;
  using NLog.Config;
  using Npgsql;

  namespace ParticipantsParse
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
        internal static string DbHost { get; set; }
        internal static string DbPort { get; set; }
        internal static string DbUserName { get; set; }
        internal static string DbPassword { get; set; }
        internal static string DbName { get; set; }
        internal static string DbScheme { get; set; }
        internal static string DbParticipants { get; set; }
        internal static int NumberOfDbConnections { get; set; }
        internal static string Url { get; set; }
        internal static string AuthToken { get; set; }
        
        public static void LoadConfiguration()
        {
            
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Goszakup Participants Parse";
            
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            
            var getConfiguration = new ConfigurationBuilder().AddJsonFile("Configuration.json").Build();
            
            DbHost = getConfiguration["Configuration:DbHost"];
            DbPort = getConfiguration["Configuration:DbPort"];
            DbUserName = getConfiguration["Configuration:DbUserName"];
            DbPassword = getConfiguration["Configuration:DbPassword"];
            DbName = getConfiguration["Configuration:DbName"];
            DbScheme = getConfiguration["Configuration:DbScheme"];
            DbParticipants = getConfiguration["Configuration:DbParticipants"];
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