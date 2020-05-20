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

        protected ApiParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings)
        {
            AuthToken = authToken;
            var response = GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected abstract override Logger InitLogger();
        public abstract override Task ParseAsync();
        protected abstract TModel DtoToDb(TDto dto);

        protected string GetApiPageResponse(string url, int delay = 10000)
        {
            var i = 0;
            while (true)
            {
                var handler = new HttpClientHandler();
                handler.Proxy = new WebProxy($"185.120.78.137:65233", true)
                    {Credentials = new NetworkCredential {UserName = "21046", Password = "V1r4TjI"}};
                var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", AuthToken);
                
                string response;
                try
                {
                    response = httpClient.GetStringAsync($"https://ows.goszakup.gov.kz/{url}?limit=500").GetAwaiter()
                        .GetResult();
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
                        Logger.Error(e);
                        throw;
                    }

                    continue;
                }
                catch (TaskCanceledException e)
                {
                    Thread.Sleep(delay);
                    if (++i == 5)
                    {
                        Logger.Error(e);
                        throw;
                    }
                    
                    continue;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    throw;
                }

                return response;
            }
        }
    }
}