﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Contexts.ParsingContexts;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.GraphQlParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.09.2020 11:32:17
    /// <summary>
    /// Tender Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class TenderParser : GraphQlSequentialParser<TrdBuyDto>
    {
        /// <summary>
        /// QueryTemplate for requests
        /// </summary>
        private const string QueryTemplate =
            "{\"query\":\"query{TrdBuy(limit:200,_AFTER){numberAnno,nameRu,orgBin,totalSum,countLots,startDate,endDate,publishDate,RefTypeTrade{nameRu},RefTradeMethods{nameRu},RefSubjectType{nameRu},RefBuyStatus{nameRu},Files{filePath,originalName,nameRu}Lots{lotNumber,count,RefLotsStatus{nameRu}customerBin,descriptionRu,amount,nameRu,Files{filePath,originalName,nameRu},Plans{refEnstruCode,supplyDateRu,RefUnits{nameRu}PlansKato{fullDeliveryPlaceNameRu}}}}}\"}";

        /// <inheritdoc />
        public TenderParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        /// <inheritdoc />
        protected override string GetQuery(int after)
        {
            var query = QueryTemplate.Replace("_AFTER", after == 0 ? string.Empty : $"after:{after}");

            return query;
        }

        /// <inheritdoc />
        // ReSharper disable once CognitiveComplexity
        protected override async Task ProcessObject(TrdBuyDto dto)
        {
            await using var ctx = new ParsingTenderContext();
            ctx.ChangeTracker.AutoDetectChangesEnabled = false;

            long.TryParse(dto.orgBin, out var orgBin);
            DateTime.TryParse(dto.startDate, out var startDate);
            DateTime.TryParse(dto.endDate, out var endDate);
            DateTime.TryParse(dto.publishDate, out var publishDate);

            var announcement = new AnnouncementGoszakup
            {
                BuyStatus = dto.RefBuyStatus?.nameRu,
                OrganizatorBiin = orgBin,
                CountLots = dto.countLots,
                StartDate = startDate.Year == 1 ? (DateTime?) null : startDate,
                EndDate = endDate.Year == 1 ? (DateTime?) null : endDate,
                NumberAnno = dto.numberAnno,
                NameRu = dto.nameRu,
                PublishDate = publishDate.Year == 1 ? (DateTime?) null : publishDate,
                SubjectType = dto.RefSubjectType?.nameRu,
                TotalSum = dto.totalSum,
                TradeMethod = dto.RefTradeMethods?.nameRu,
                TypeTrade = dto.RefTypeTrade?.nameRu
            };

            ctx.AnnouncementsGoszakup.Add(announcement);
            await ctx.SaveChangesAsync();

            if (dto.Files != null)
                foreach (var dtoFile in dto.Files)
                {
                    var file = new AnnouncementFileGoszakup
                    {
                        AnnoId = announcement.Id,
                        Link = dtoFile.filePath,
                        NameRu = dtoFile.nameRu,
                        OriginalName = dtoFile.originalName
                    };
                    ctx.AnnouncementFilesGoszakups.Add(file);
                    await ctx.SaveChangesAsync();
                }

            if (dto.Lots != null)
                foreach (var trdBuyLot in dto.Lots)
                {
                    long.TryParse(trdBuyLot.customerBin, out var bin);

                    var lot = new LotGoszakup
                    {
                        Amount = trdBuyLot.amount,
                        Count = trdBuyLot.count,
                        AnnoId = announcement.Id,
                        CustomerBin = bin,
                        DescriptionRu = trdBuyLot.descriptionRu,
                        LotNumber = trdBuyLot.lotNumber,
                        LotStatus = trdBuyLot.RefLotsStatus.nameRu,
                        NameRu = trdBuyLot.nameRu
                    };
                    if (trdBuyLot.Plans != null && trdBuyLot.Plans.Length > 0)
                    {
                        lot.TruCode = trdBuyLot.Plans[0]?.refEnstruCode;
                        lot.Units = trdBuyLot.Plans[0]?.RefUnits.nameRu;
                        lot.SupplyDateRu = trdBuyLot.Plans[0]?.supplyDateRu;
                        if (trdBuyLot.Plans[0].PlansKato != null && trdBuyLot.Plans[0].PlansKato.Length > 0)
                            lot.DeliveryPlace = trdBuyLot.Plans[0]?.PlansKato[0].fullDeliveryPlaceNameRu;
                    }

                    ctx.LotsGoszakup.Add(lot);
                    await ctx.SaveChangesAsync();

                    // ReSharper disable once InvertIf
                    if (trdBuyLot.Files != null)
                        foreach (var dtoLotFile in trdBuyLot.Files)
                        {
                            var file = new LotFileGoszakup
                            {
                                LotId = lot.Id,
                                Link = dtoLotFile.filePath,
                                NameRu = dtoLotFile.nameRu,
                                OriginalName = dtoLotFile.originalName
                            };
                            ctx.LotFilesGoszakup.Add(file);
                            await ctx.SaveChangesAsync();
                        }
                }
        }

        /// <summary>
        /// Returns true if any of announcement elements are older than 4 days
        /// </summary>
        protected override bool StopCondition(object checkElement)
        {
            var elements = (List<TrdBuyDto>) checkElement;
            return elements.All(x =>
            {
                DateTime.TryParse(x.publishDate, out var startDate);
                return startDate < DateTime.Now.Subtract(TimeSpan.FromDays(7));
            });
        }

        /// <inheritdoc />
        protected override string LogMessage(object obj = null) =>
            $"Left: {Total}; Parsing {(DateTime.Now - DateTime.Parse(((List<TrdBuyDto>) obj)!.OrderBy(x => x.publishDate).Last().publishDate)).Days} day";

        /// <inheritdoc />
        /// <inheritdoc />
        public override async Task TruncateParsingTables()
        {
            await using var ctx = new ParsingTenderContext();

            // Truncating ContractGoszakup Model Table
            var entityType = ctx.Model.FindEntityType(typeof(AnnouncementGoszakup));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            await ctx.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {schema}.{tableName} RESTART IDENTITY CASCADE");
        }
    }
}