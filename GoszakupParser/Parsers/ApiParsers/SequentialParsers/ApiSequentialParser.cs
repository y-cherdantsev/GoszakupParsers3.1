﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 14:51:37
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class ApiSequentialParser<TDto, TModel> : ApiParser<TDto, TModel>
        where TModel : DbLoggerCategory.Model, new()
    {
        protected ApiSequentialParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(
            parserSettings, authToken, proxy)
        {
        }

        public override async Task ParseAsync()
        {
            var tasks = new List<Task>();
            Logger.Info("Starting parsing");
            while (true)
            {var response = "";
                    IRestResponse restResp;
                        (response, restResp)= GetApiPageResponse(Url).Result;
                try
                {
                    
                    if (response == null)
                        break;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                    Url = apiResponse?.next_page;
                    if (Url == null)
                        Console.WriteLine("Url is null");

                    await Task.WhenAll(tasks);
                    tasks.Clear();

                    for (var i = 0; i < Threads; i++)
                        tasks.Add(ProcessObjects(DivideList(apiResponse?.items, i)));

                    if (Url != "") continue;
                    await Task.WhenAll(tasks);
                    foreach (var task in tasks.Where(task => task.IsFaulted))
                    {
                        Logger.Error(task.Exception);
                    }

                    if (tasks.Any(x => x.IsFaulted))
                        throw new Exception("Parsing hasn't been done");
                    tasks.Clear();
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error(response);
                }
            }

            Logger.Info("Parsing done");
            // if (Total > 0)
                // Logger.Info($"{Total} elements hasn't been parsed");
        }

        private async Task ProcessObjects(TDto[] entities)
        {
            //TODO(Fix error Подключение не установлено, т.к. конечный компьютер отверг запрос на подключение. 192.168.2.25:5432)
            try
            {
                await using var context = new ParserContext<TModel>();
                foreach (var dto in entities)
                {
                    var model = DtoToDb(dto);
                        context.Models.Add(model);
                        await context.SaveChangesAsync();

                    --Total;
                    // lock (Lock)
                    // Logger.Trace($"Left:{--Total}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private TDto[] DivideList(List<TDto> list, int i)
        {
            return list.Where(x => list.IndexOf(x) % Threads == i).ToArray();
        }
    }
}