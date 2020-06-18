using System;
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

        protected ApiParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings)
        {
            AuthToken = authToken;
            Proxy = proxy;
            var response = GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected abstract override Logger InitLogger();
        public abstract override Task ParseAsync();
        protected abstract TModel DtoToDb(TDto dto);

        protected string GetApiPageResponse(string url, int delay = 10000)
        {
            // Thread.Sleep(1000);
            var i = 0;
            while (true)
            {
                var handler = new HttpClientHandler();

                handler.Proxy = Proxy;
                var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", AuthToken);

                string response = "";
                try
                {
                    response = httpClient.GetStringAsync($"https://ows.goszakup.gov.kz/{url}?limit=500").GetAwaiter()
                        .GetResult();
                    Logger.Trace($"{url} - Left:[{Total}]");
                }
                catch (HttpRequestException e)
                {
                    // if (response.Contains("\"status\": 403,"))
                    // {
                        // Console.WriteLine(response);
                    // }
                    if (++i == 25)
                    {
                        Logger.Error(e, $"Link:'{url}'");
                        httpClient.GetStringAsync($"https://ows.goszakup.gov.kz/{url}?limit=500")
                            .GetAwaiter()
                            .GetResult();
                        throw;
                    }
                    Thread.Sleep(1000);

                    continue;
                }
                catch (WebException e)
                {
                    if (e.Message.Equals("The remote server returned an error: (404) Not Found."))
                    {
                        return null;
                    }

                    Thread.Sleep(delay);
                    if (++i == 5)
                    {
                        Logger.Error(e, $"Link:'{url}'");
                        throw;
                    }

                    continue;
                }
                catch (TaskCanceledException e)
                {
                    Thread.Sleep(delay);
                    if (++i == 5)
                    {
                        Logger.Error(e, $"Link:'{url}'");
                        throw;
                    }

                    continue;
                }
                catch (Exception e)
                {
                    Logger.Error(e, $"Link:'{url}'");
                    throw;
                }

                return response;
            }
        }
    }
}