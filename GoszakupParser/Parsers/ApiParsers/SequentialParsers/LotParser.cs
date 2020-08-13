using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Parsers.ApiParsers.AimParsers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:55:36
    /// <summary>
    /// Lot Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class LotParser : ApiSequentialParser<LotDto, LotGoszakup>
    {
        /// <inheritdoc />
        public LotParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <inheritdoc />
        protected override LotGoszakup DtoToModel(LotDto dto)
        {
            long.TryParse(dto.customer_bin, out var customerBin);
            DateTime.TryParse(dto.index_date, out var indexDate);
            DateTime.TryParse(dto.last_update_date, out var lastUpdateDate);

            var lot = new LotGoszakup
            {
                Id = dto.id,
                LotNumber = dto.lot_number,
                RefLotStatusId = dto.ref_lot_status_id,
                UnionLots = dto.union_lots == 1,
                Count = dto.count,
                Amount = dto.amount,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                DescriptionRu = dto.description_ru,
                DescriptionKz = dto.description_kz,
                CustomerId = dto.customer_id,
                CustomerBin = customerBin,
                TrdBuyNumberAnno = dto.trd_buy_number_anno,
                TrdBuyId = dto.trd_buy_id,
                Dumping = dto.dumping == 1,
                DumpingLotPrice = dto.dumping_lot_price != 0 ? dto.dumping_lot_price : (int?) null,
                PsdSign = dto.psd_sign != 0 ? dto.psd_sign : (int?) null,
                ConsultingServices = dto.consulting_services == 1,
                SingleOrgSign = dto.singl_org_sign,
                IsLightIndustry = dto.is_light_industry == 1,
                IsConstructionWork = dto.is_construction_work == 1,
                DisablePersonId = dto.disable_person_id == 1,
                CustomerNameKz = dto.customer_name_kz,
                CustomerNameRu = dto.customer_name_ru,
                RefTradeMethodsId = dto.ref_trade_methods_id,
                SystemId = dto.system_id,
                IndexDate = indexDate,
                LastUpdateDate = lastUpdateDate,
                PlanPoint = dto.point_list.Length > 0 ? dto.point_list[0] : (int?) null
            };
            return lot;
        }


        /// <summary>
        /// Returns true if all lot elements are older than 60 days
        /// </summary>
        protected override bool StopCondition(object checkElement)
        {
            var elements = (ApiResponse<LotDto>) checkElement;
            return elements.items.All(x =>
            {
                DateTime.TryParse(x.index_date, out var indexDate);
                return indexDate < DateTime.Now.Subtract(TimeSpan.FromDays(32));
            });
        }

        /// <summary>
        /// Converts dto into model and inserts it into DB
        /// </summary>
        /// <param name="dto">Dto from Api</param>
        /// <param name="context">Parsing DB context</param>
        // protected override async Task ProcessObject(LotDto dto, AdataContext<LotGoszakup> context)
        // {
        //     var model = DtoToModel(dto);
        //     context.Models.Add(model);
        //     
        //     var flag = false;
        //     InsertDataOperation:
        //     try
        //     {
        //         await context.SaveChangesAsync();
        //     }
        //     // Appears while network card error occurs
        //     catch (InvalidOperationException e)
        //     {
        //         Logger.Warn(e.Message);
        //         Thread.Sleep(15000);
        //         goto InsertDataOperation;
        //     }
        //     catch (DbUpdateException e)
        //     {
        //         if (e.InnerException != null && e.InnerException.Message.Contains("goszakup_lot_plan_fk") && flag==false)
        //         {
        //             flag = true;
        //             var newParserSetting = Configuration.ParsersStatic.FirstOrDefault(x => x.Name == "LotPlan");
        //             // ReSharper disable once PossibleNullReferenceException
        //             newParserSetting.Threads = 1;
        //             lock (Url)
        //             {
        //                 new ParsePlanForce(newParserSetting, Configuration.AuthTokenStatic, dto.point_list[0].ToString()).ParseAsync().GetAwaiter().GetResult();
        //             }
        //             goto InsertDataOperation;
        //         }
        //
        //         if (e.InnerException is NpgsqlException)
        //             Logger.Trace($"Message: {e.InnerException?.Data["MessageText"]}; " +
        //                          $"{e.InnerException?.Data["Detail"]} " +
        //                          $"{e.InnerException?.Data["SchemaName"]}.{e.InnerException?.Data["TableName"]}");
        //         else
        //             throw;
        //     }
        // }
        //
        //
        // private class ParsePlanForce : LotPlanParser
        // {
        //     private readonly string _aim;
        //     public ParsePlanForce(Configuration.ParserSettings parserSettings, string authToken, string aim) : base(parserSettings, authToken)
        //     {
        //         _aim = aim;
        //     }
        //
        //     // ReSharper disable once OptionalParameterHierarchyMismatch
        //     protected override IEnumerable<string> LoadAims()
        //     {
        //         return new List<string> {_aim};
        //     }
        // }
    }
}