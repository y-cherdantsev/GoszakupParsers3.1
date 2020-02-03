using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Npgsql;
using UnscrupulousesParse.Database;
using UnscrupulousesParse.Units;

namespace UnscrupulousesParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:36:34
@version 1.0
@brief Парсер
    
    */
    public class Parser
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */

        private static int TotalLoaded { get; set; } /*!< Всего скачано недобросовестных участников с api */

        private static int Total { get; set; } /*!< Всего недобросовестных участников на обработку */

        private static bool LoadedAll { get; set; } /*!< Флаг конца скачивания недобросовестных участников через api  */

        private static int
            TotalProceed { get; set; } /*!< Всего обработано недобросовестных участников (загружено в базу, обновлено и т.д.)*/

        private static readonly List<Unscrupulous>
            LoadedUnscrupulouses = new List<Unscrupulous>(); /*!< Список недобросовестных участников для обработки */


        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:42:27
@version 1.0
@brief Парсер собственной персоной
@throw Exeption - непредвиденные исключения

     */
        public void Parse()
        {
            _logger.Info($"Parser has been started");

            //Определяет общее количетво недобросовестных участников государственных закупок на текущий момент
            Total = JsonSerializer.Deserialize<MainResponse>(GetPageResponse(Configuration.Url)).total;
            _logger.Info($"Total unscrupulouses: |{Total}|");

            //Начало скачивания всех недобросовестных участников из api в отдельном потоке
            Task.Run(ParseApi);

            //Создание и запуск потоков обработки всех недобросовестных участников из уже загруженных
            var tasks = new Task[Configuration.NumberOfDbConnections];

            for (var i = 0; i < tasks.Length; i++)
                tasks[i] = Task.Run(ProcessUnscrupulous);
            _logger.Info("All threads has been started");

            //Ожидание окончания работы всех потоков обработки
            Task.WaitAll(tasks);

            _logger.Info($"Parsing done");
            _logger.Info($"Total Loaded:  |{TotalLoaded}|");
            _logger.Info($"Total Proceed: |{TotalProceed}|");
        }

        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:42:27
@version 1.0
@brief Скачивает всех недобросовестных участников госзакупа используя api
@throw WebException - непредвиденное сетевое исключение не являющаяся потерью соединения
@throw Exception - непредвиденное исключение
     
     */
        private void ParseApi()
        {
            var nextUrl = Configuration.Url;
            while (true)
            {
                //Защита от переполнения памяти
                while (TotalLoaded - TotalProceed > 5000)
                    Thread.Sleep(250);


                MainResponse mainResponse;
                try
                {
                    var response = GetPageResponse(nextUrl);
                    mainResponse = JsonSerializer.Deserialize<MainResponse>(response);
                }
                catch (WebException e)
                {
                    if (e.Status != WebExceptionStatus.NameResolutionFailure) throw;
                    _logger.Debug("Lost connection, waiting for reconnecting");
                    Thread.Sleep(5000);
                    continue;
                }
                //Если не может распарсить JSON, значит что это была последняя страница
                catch (JsonException)
                {
                    break;
                }

                nextUrl = mainResponse.next_page;

                LoadedUnscrupulouses.AddRange(mainResponse.items);

                TotalLoaded += mainResponse.items.Count;
                _logger.Trace($"Total Loaded: {TotalLoaded}");
            }

            LoadedAll = true;
        }

        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:44:13
@version 1.0
@brief Обработка недобросовестных участников из скачанного списка
@throw Exception - непредвиденное исключение

     */
        private void ProcessUnscrupulous()
        {
            using (var connection = Configuration.GetNewConnection())
            {
                //Работает пока список не обнулится и загрузка из api не закончится
                while (LoadedUnscrupulouses.Count != 0 || !LoadedAll)
                {
                    Unscrupulous unscrupulous = null;
                    try
                    {
                        //Лочит использование списка недобросовестных участников, копирует нулевого участника забирая на обработку и удаляет его из общего списка 
                        lock (LoadedUnscrupulouses)
                        {
                            if (LoadedUnscrupulouses.Count == 0)
                                continue;

                            unscrupulous = LoadedUnscrupulouses[0];
                            LoadedUnscrupulouses.RemoveAt(0);
                        }

                        ProcessParticipant(unscrupulous, connection);
                    }
                    catch (NpgsqlException e)
                    {
                        LoadedUnscrupulouses.Add(unscrupulous);
                        _logger.Debug("Lost connection to the database");
                        Thread.Sleep(5000);
                        try
                        {
                            connection.Open();
                            continue;
                        }
                        catch (Exception)
                        {
                            _logger.Warn($"{e.StackTrace} | {e.Message}");
                        }

                        _logger.Error(
                            $"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|; [Connection State]: |{connection.State}|;");
                    }
                    catch (Exception e)
                    {
                        _logger.Fatal(
                            $"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|;");
                        throw;
                    }
                }
            }
        }


        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:44:13
@version 1.0
@brief Обработка участника (загрузка или обновление участника в базе данных)
@param[in] - unscrupulous - Участник
@throw Exception - непредвиденное исключение
     
     */
        private void ProcessParticipant(Unscrupulous unscrupulous, NpgsqlConnection connection)
        {
            var unscrupulousDb = new UnscrupulousDb(unscrupulous);
            DbRequestsUnscrupulouses.AddUnscrupulous(unscrupulousDb, connection);
            TotalProceed++;
            _logger.Trace($"Proceeding: {unscrupulousDb.supplier_name_ru}");
            _logger.Trace($"Total Proceed: {TotalProceed}");
        }


        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:44:13
@version 1.0
@brief Возвращает страницу api со списком недобросовестных участников
@param[in] url - сылка на следущую страницу api
@return Страница с json объектом
@throw Exception - непредвиденное исключение
     
@code

    //?limit=500 возвращаемое количество недобросовестных участников после одного запроса
     var request = WebRequest.Create($"https://ows.goszakup.gov.kz/{url}?limit=500");
     
@endcode
     
     */
        private static string GetPageResponse(string url)
        {
            var request = WebRequest.Create($"https://ows.goszakup.gov.kz/{url}?limit=500");
            request.Method = WebRequestMethods.Http.Get;
            request.Headers["Content-Type"] = "application/json";
            request.Headers["Authorization"] = Configuration.AuthToken;
            request.AuthenticationLevel = AuthenticationLevel.None;
            var response = request.GetResponse();
            var objReader =
                new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());

            var sLine = "";
            var pageResponse = "";
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    pageResponse += sLine;
            }

            return pageResponse;
        }
    }
}