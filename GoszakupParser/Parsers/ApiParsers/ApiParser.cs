using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using NLog;
using RestSharp;

namespace GoszakupParser.Parsers.ApiParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 11:53:37
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class ApiParser<TDto, TModel> : Parser
    {
        private string AuthToken { get; set; }
        protected int Total { get; set; }
        private WebProxy Proxy { get; set; }

        protected ApiParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(
            parserSettings)
        {
            AuthToken = authToken;
            Proxy = proxy;
            var response = "";
            IRestResponse temp;
            (response, temp) = GetApiPageResponse(Url).Result;
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected abstract override Logger InitLogger();
        public abstract override Task ParseAsync();
        protected abstract TModel DtoToDb(TDto dto);

        protected async Task<(string, IRestResponse)> GetApiPageResponse(string url, int delay = 15000)
        {
            // Thread.Sleep(1000);
            var i = 0;
            while (true)
            {
                var restClient = new RestClient($"https://ows.goszakup.gov.kz/{url}?limit=500");
                restClient.Proxy = Proxy;
                restClient.Timeout = 15000;
                restClient.AddDefaultHeader("Content-Type", "application/json");
                restClient.AddDefaultHeader("Authorization", AuthToken);
                string response;
                IRestResponse restResponse = null;
                try
                {
                    var cts = new CancellationTokenSource();
                    var awaitingTask = restClient.ExecuteAsync(new RestRequest(Method.GET), cts.Token);

                    var tempDelay = delay;
                    while (true)
                    {
                        Thread.Sleep(50);
                        tempDelay -= 50;
                        if (tempDelay < 0 && !awaitingTask.IsCompleted)
                        {
                            Logger.Warn("Timeout exceeded");
                            cts.Cancel();
                            return await GetApiPageResponse(url, delay);
                        }

                        if (awaitingTask.IsCompleted)
                        {
                            break;
                        }
                    }

                    if (awaitingTask.IsCompletedSuccessfully)
                    {
                        restResponse = awaitingTask.Result;
                    }
                    else
                    {
                        ++i;
                        Thread.Sleep(delay);
                        Logger.Warn($"{i} times, {restResponse.Content}");
                        continue;
                    }
                    switch (restResponse.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:case HttpStatusCode.InternalServerError:
                            ++i;
                            Thread.Sleep(delay);
                            Logger.Warn($"{i} times, {restResponse.Content}");
                            continue;
                    }

                    if (restResponse.ErrorMessage != null &&
                        restResponse.ErrorMessage.Contains(
                            "An error occurred while sending the request. The response ended prematurely."))
                    {
                        ++i;

                        Thread.Sleep(delay);
                        Logger.Warn($"{i} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    if (restResponse.ErrorMessage != null &&
                        restResponse.ErrorMessage.Contains("The operation has timed out."))
                    {
                        ++i;

                        Thread.Sleep(delay);
                        Logger.Warn($"{i} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    if (restResponse.ContentLength == 0 ||
                        (restResponse.ErrorMessage != null && restResponse.ErrorMessage.Contains(
                            "Подключение не установлено," +
                            " т.к. конечный компьютер отверг запрос на подключение. Подключение не установлено," +
                            " т.к. конечный компьютер отверг запрос на подключение.")))
                    {
                        ++i;

                        Thread.Sleep(delay);
                        Logger.Warn($"{i} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    response = restResponse.Content;
                    if (restResponse.Content.Length < 1000)
                        Logger.Trace($"{i} times, {restResponse.Content}");
                    Logger.Trace($"{url} - Left:[{Total}]");
                }
                catch (Exception e)
                {
                    Logger.Error(e, $"Link:'{url}'");
                    throw;
                }

                return (response, restResponse);
            }
        }
    }
}