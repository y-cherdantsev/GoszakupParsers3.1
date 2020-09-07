using System;
using System.Collections.Generic;
using System.Linq;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 04.09.2020 17:36:12
    /// <summary>
    /// Lot Parser Based On Lot
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class AnnouncementLotParser : ApiAimParser<LotDto, LotGoszakup>
    {
        public AnnouncementLotParser(Configuration.ParserSettings parserSettings, string authToken) : base(
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

        /// <inheritdoc />
        protected override IEnumerable<string> LoadAims()
        {
            var announcementContext = new AdataContext<AnnouncementGoszakup>(DatabaseConnections.ParsingAvroradata);
            var aims = announcementContext.Models
                .FromSqlRaw(
                    @"SELECT an.number_anno AS number_anno FROM avroradata.announcement_goszakup an WHERE number_anno NOT IN (
            SELECT a.number_anno
                FROM avroradata.announcement_goszakup a
            FULL OUTER JOIN (SELECT trd_buy_number_anno, sum(amount) AS sum
            FROM avroradata.lot_goszakup l
            GROUP BY trd_buy_number_anno) l ON l.trd_buy_number_anno = a.number_anno
            WHERE a.total_sum = l.sum
                )")
                .Select(x => x.NumberAnno.ToString()).ToList();
            announcementContext.Dispose();
            return aims;
        }
    }
}