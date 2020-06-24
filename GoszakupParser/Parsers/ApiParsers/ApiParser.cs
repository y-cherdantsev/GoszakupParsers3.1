﻿using System;
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
                (response, temp)= GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected abstract override Logger InitLogger();
        public abstract override Task ParseAsync();
        protected abstract TModel DtoToDb(TDto dto);

        protected (string, IRestResponse) GetApiPageResponse(string url, int delay = 15000)
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
                var response = "";
                IRestResponse restResponse = null;
                try
                {
                    restResponse = restClient.Execute(new RestRequest(Method.GET));
                    if (restResponse.StatusCode == HttpStatusCode.Forbidden)
                        return GetApiPageResponse(url, delay);
                    if (restResponse.StatusCode == 0 && restResponse.ContentLength == 0)
                        return GetApiPageResponse(url, delay);
                    response = restResponse.Content;
                     
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
                        restClient.Execute(new RestRequest(Method.GET));
                        throw;
                    }

                    Thread.Sleep(1000);

                    continue;
                }
                catch (WebException e)
                {
                    Logger.Warn(e, "MAFAKAKA");
                    if (e.Message.Equals("The remote server returned an error: (404) Not Found."))
                    {
                        return (null, null);
                    }

                    Thread.Sleep(delay);
                    if (++i == 5)
                    {
                        Thread.Sleep(delay);
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

                return (response, restResponse);
            }
        }
    }
}