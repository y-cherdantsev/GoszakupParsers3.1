using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ContractsParse.Database;
using ContractsParse.Units;
using NLog;
using Npgsql;

namespace ContractsParse
{
    /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 14:49:05
@version 1.0
@brief Парсер
    
    */
    public class Parser
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger(); /*!< Логгер текущего класса */

        public static int TotalLoaded { get; set; } /*!< Всего скачано договоров с api */

        private static int Total { get; set; } /*!< Всего договоров на обработку */

        public static bool LoadedAll { get; set; } /*!< Флаг конца скачивания договоров через api  */

        private static int
            TotalProceed { get; set; } /*!< Всего обработано договоров (загружено в базу, обновлено и т.д.)*/

        private static readonly List<Contract>
            LoadedContracts = new List<Contract>(); /*!< Список договоров для обработки */


        /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 14:49:05
@version 1.0
@brief Парсер собственной персоной
@throw Exeption - непредвиденные исключения

     */
        public void Parse()
        {
            _logger.Info($"Parser has been started");

            //Определяет общее количество договоров государственных закупок на текущий момент
            Total = JsonSerializer.Deserialize<MainResponse>(GetPageResponse(Configuration.Url)).total;
            _logger.Info($"Total contracts: |{Total}|");

            //Начало скачивания всех договоров из api в отдельном потоке
            Task.Run(ParseApi);

            //Создание и запуск потоков обработки всех договоров из уже загруженных
            var tasks = new Task[Configuration.NumberOfDbConnections];

            for (var i = 0; i < tasks.Length; i++)
                tasks[i] = Task.Run(ProcessContracts);
            _logger.Info("All threads has been started");

            //Ожидание окончания работы всех потоков обработки
            Task.WaitAll(tasks);

            _logger.Info($"Parsing done");
            _logger.Info($"Total Loaded:  |{TotalLoaded}|");
            _logger.Info($"Total Proceed: |{TotalProceed}|");
        }

        /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 14:49:05
@version 1.0
@brief Скачивает всех договоров госзакупа используя api
@throw WebException - непредвиденное сетевое исключение не являющаяся потерью соединения
@throw Exception - непредвиденное исключение
     
     */
        private void ParseApi()
        {
            while (true)
            {
                //Защита от переполнения памяти
                while (TotalLoaded - TotalProceed > 5000)
                    Thread.Sleep(250);


                var response = "";
                MainResponse mainResponse;
                try
                {
                    response = GetPageResponse(Configuration.Url);
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
                    if (response != "")
                    {
                        if (response.Contains("<!DOCTYPE html>"))
                            break;
                        throw;
                    }

                    break;
                }

                Configuration.Url = mainResponse.next_page;

                LoadedContracts.AddRange(mainResponse.items);

                TotalLoaded += mainResponse.items.Count;
                _logger.Trace($"Total Loaded: {TotalLoaded}");
            }

            LoadedAll = true;
        }

        /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 14:49:05
@version 1.0
@brief Обработка договоров из скачанного списка
@throw Exception - непредвиденное исключение

     */
        private void ProcessContracts()
        {
            using (var connection = Configuration.GetNewConnection())
            {
                //Работает пока список не обнулится и загрузка из api не закончится
                while (LoadedContracts.Count != 0 || !LoadedAll)
                {
                    Contract contract = null;
                    try
                    {
                        //Лочит использование списка договоров, копирует нулевой договор забирая на обработку и удаляет его из общего списка 
                        lock (LoadedContracts)
                        {
                            if (LoadedContracts.Count == 0)
                                continue;

                            contract = LoadedContracts[0];
                            LoadedContracts.RemoveAt(0);
                        }

                        ProcessContract(contract, connection);
                    }
                    catch (NpgsqlException e)
                    {
                        LoadedContracts.Add(contract);
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
                        if (contract != null)
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
@date 08.02.2020 14:49:05
@version 1.0
@brief Обработка договора (загрузка или обновление договора в базе данных)
@param[in] - contract - договор
@throw Exception - непредвиденное исключение
     
     */
        private void ProcessContract(Contract contract, NpgsqlConnection connection)
        {
            var contractDb = new ContractDb(contract);
            DbRequestsContracts.AddContract(contractDb, connection);
            TotalProceed++;
            _logger.Trace($"Proceeding: {contractDb.description_ru}");
            _logger.Trace($"Total Proceed: {TotalProceed}");
        }


        /*!

@author Yevgeniy Cherdantsev
@date 08.02.2020 14:49:05
@version 1.0
@brief Возвращает страницу api со списком договоров
@param[in] url - сылка на следущую страницу api
@return Страница с json объектом
@throw Exception - непредвиденное исключение
     
@code

    //?limit=500 возвращаемое количество договоров после одного запроса
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