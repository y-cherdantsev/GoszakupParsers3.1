﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GoszakupParser.Parsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 14:51:37
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class ApiSequentialParser<TDto, TModel> : Parser<TDto, TModel>
        where TModel : DbLoggerCategory.Model, new()
    {
        protected ApiSequentialParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings)
        {
            AuthToken = authToken;
            var response = GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        private int Total { get; set; }
        private string AuthToken { get; set; }

        public override async Task ParseAsync()
        {
            var tasks = new List<Task>();
            Logger.Info("Starting parsing");
            while (true)
            {
                var response = GetApiPageResponse(Url);
                if (response == null)
                    break;
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                Url = apiResponse?.next_page;

                await Task.WhenAll(tasks);
                tasks.Clear();

                for (var i = 0; i < NumOfDbConnections; i++)
                    tasks.Add(ProcessObjects(DivideList(apiResponse?.items, i)));

                if (Url != "") continue;
                await Task.WhenAll(tasks);
                tasks.Clear();
                break;
            }

            Logger.Info("Parsing done");
            if (Total > 0)
                Logger.Info($"{Total} elements hasn't been parsed");
        }

        protected override async Task ProcessObjects(TDto[] entities)
        {
            using (var context = new ParserContext<TModel>())
            {
                foreach (var dto in entities)
                {
                    var model = DtoToDb(dto);
                    context.Models.Add(model);
                    await context.SaveChangesAsync();
                    lock (Lock)
                        Logger.Trace($"Left:{--Total}");
                }
            }
        }
        
        private string GetApiPageResponse(string url)
        {
            var request = WebRequest.Create($"https://ows.goszakup.gov.kz/{url}?limit=500");
            request.Method = WebRequestMethods.Http.Get;
            request.Headers["Content-Type"] = "application/json";
            request.Headers["Authorization"] = AuthToken;
            request.AuthenticationLevel = AuthenticationLevel.None;
            var response = request.GetResponse();
            if (response.GetResponseStream() == null)
                return null;
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
        protected TDto[] DivideList(List<TDto> list, int i)
        {
            return list.Where(x => list.IndexOf(x) % NumOfDbConnections == i).ToArray();
        }
    }
}