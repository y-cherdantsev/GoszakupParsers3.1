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
using ParticipantsParse.Database;
using ParticipantsParse.Units;

namespace ParticipantsParse
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

        private static int TotalLoaded { get; set; } /*!< Всего скачано участников с api */

        private static int Total { get; set; } /*!< Всего участников на обработку */

        private static bool LoadedAll { get; set; } /*!< Флаг конца скачивания участников через api  */

        private static int
            TotalProceed { get; set; } /*!< Всего обработано участников (загружено в базу, обновлено и т.д.)*/

        private static readonly List<Participant>
            LoadedParticipants = new List<Participant>(); /*!< Список участников для обработки */


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

            //Определяет общее количество участников государственных закупок на текущий момент
            Total = JsonSerializer.Deserialize<MainResponse>(GetPageResponse(Configuration.Url)).total;
            _logger.Info($"Total participants: |{Total}|");

            //Начало скачивания всех участников из api в отдельном потоке
            Task.Run(ParseApi);

            //Создание и запуск потоков обработки всех участников из уже загруженных
            var tasks = new Task[Configuration.NumberOfDbConnections];

            for (var i = 0; i < tasks.Length; i++)
                tasks[i] = Task.Run(ProcessParticipants);
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
@brief Скачивает всех участников госзакупа используя api
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

                LoadedParticipants.AddRange(mainResponse.items);

                TotalLoaded += mainResponse.items.Count;
                _logger.Trace($"Total Loaded: {TotalLoaded}");
            }

            LoadedAll = true;
        }

        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:44:13
@version 1.0
@brief Обработка участников из скачанного списка
@throw Exception - непредвиденное исключение

     */
        private void ProcessParticipants()
        {
            using (var connection = Configuration.GetNewConnection())
            {
                //Работает пока список не обнулится и загрузка из api не закончится
                while (LoadedParticipants.Count != 0 || !LoadedAll)
                {
                    Participant participant = null;
                    try
                    {
                        //Лочит использование списка участников, копирует нулевого участника забирая на обработку и удаляет его из общего списка 
                        lock (LoadedParticipants)
                        {
                            if (LoadedParticipants.Count == 0)
                                continue;

                            participant = LoadedParticipants[0];
                            LoadedParticipants.RemoveAt(0);
                        }

                        ProcessParticipant(participant, connection);
                    }
                    catch (NpgsqlException e)
                    {
                        LoadedParticipants.Add(participant);
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
                    catch (NullReferenceException e)
                    {
                        if (participant != null)
                            _logger.Warn($"[StackTrace]: |{e.StackTrace}|; [Message]: |{e.Message}|;");
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
@param[in] - participant - Участник
@throw Exception - непредвиденное исключение
     
     */
        private void ProcessParticipant(Participant participant, NpgsqlConnection connection)
        {
            var participantDb = new ParticipantDb(participant);
            DbRequestsParticipants.AddParticipant(participantDb, connection);
            TotalProceed++;
            _logger.Trace($"Proceeding: {participantDb.name_ru}");
            _logger.Trace($"Total Proceed: {TotalProceed}");
        }


        /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 17:44:13
@version 1.0
@brief Возвращает страницу api со списком участников
@param[in] url - сылка на следущую страницу api
@return Страница с json объектом
@throw Exception - непредвиденное исключение
     
@code

    //?limit=500 возвращаемое количество участников после одного запроса
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