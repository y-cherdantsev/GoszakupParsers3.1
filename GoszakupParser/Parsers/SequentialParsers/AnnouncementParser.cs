using System;
using GoszakupParser.Models;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;
using NpgsqlTypes;

namespace GoszakupParser.Parsers.SequentialParsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:59:50
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class AnnouncementParser : ApiSequentialParser<AnnouncementDto, AnnouncementGoszakup>
    {
        public AnnouncementParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings, authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override AnnouncementGoszakup DtoToDb(AnnouncementDto dto)
        {
            var announcement = new AnnouncementGoszakup();

            announcement.Id = dto.id;
            announcement.NumberAnno = dto.number_anno;
            announcement.NameRu = dto.name_ru;
            announcement.NameKz = dto.name_kz;
            announcement.TotalSum = dto.total_sum;
            announcement.RefTradeMethodsId = dto.ref_trade_methods_id;
            announcement.RefSubjectTypeId = dto.ref_subject_type_id;
            long.TryParse(dto.customer_bin, out var customerBin);
            announcement.CustomerBin = customerBin;
            announcement.CustomerPid = dto.customer_pid;
            long.TryParse(dto.org_bin, out var orgBin);
            announcement.OrgBin = orgBin;
            announcement.OrgPid = dto.org_pid;
            announcement.RefBuyStatusId = dto.ref_buy_status_id;
            announcement.RefTypeTradeId = dto.ref_type_trade_id;
            announcement.DisablePersonId = dto.disable_person_id == 1;
            announcement.IdSupplier = dto.id_supplier;
            long.TryParse(dto.biin_supplier, out var biinSupplier);
            announcement.BiinSupplier = biinSupplier;
            announcement.ParentId = dto.parent_id;
            announcement.SingleOrgSign = dto.singl_org_sign == 1;
            announcement.IsLightIndustry = dto.is_light_industry == 1;
            announcement.IsConstructionWork = dto.is_construction_work == 1;
            announcement.CustomerNameKz = dto.customer_name_kz;
            announcement.CustomerNameRu = dto.customer_name_ru;
            announcement.OrgNameKz = dto.org_name_kz;
            announcement.OrgNameRu = dto.org_name_ru;
            announcement.SystemId = dto.system_id;
            try { announcement.StartDate = DateTime.Parse(dto.start_date); }catch (Exception) { }
            try { announcement.RepeatStartDate = DateTime.Parse(dto.repeat_start_date); }catch (Exception) { }
            try { announcement.RepeatStartDate = DateTime.Parse(dto.repeat_end_date); }catch (Exception) { }
            try { announcement.EndDate = DateTime.Parse(dto.end_date); }catch (Exception) { }
            try { announcement.PublishDate = DateTime.Parse(dto.publish_date); }catch (Exception) { }
            try { announcement.ItogiDatePublic = DateTime.Parse(dto.itogi_date_public); }catch (Exception) { }
            try { announcement.DiscusStartDate = DateTime.Parse(dto.discus_start_date); }catch (Exception) { }
            try { announcement.DiscusEndDate = DateTime.Parse(dto.discus_end_date); }catch (Exception) { }
            try { announcement.IndexDate = DateTime.Parse(dto.index_date); }catch (Exception) { }

            return announcement;
        }
    }
}