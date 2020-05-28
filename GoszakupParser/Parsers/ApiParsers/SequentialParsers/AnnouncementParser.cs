using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:59:50
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class AnnouncementParser : ApiSequentialParser<AnnouncementDto, AnnouncementGoszakup>
    {
        public AnnouncementParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
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
            announcement.NameRu = !string.IsNullOrEmpty(dto.name_ru) ? dto.name_ru : null;
            announcement.NameKz = !string.IsNullOrEmpty(dto.name_kz) ? dto.name_kz : null;
            announcement.TotalSum = dto.total_sum;
            announcement.RefTradeMethodsId = dto.ref_trade_methods_id;
            announcement.RefSubjectTypeId = dto.ref_subject_type_id;
            long.TryParse(dto.customer_bin, out var customerBin);
            announcement.CustomerBin = customerBin != 0 ? customerBin : (long?) null;
            announcement.CustomerPid = dto.customer_pid != 0 ? dto.customer_pid : (int?) null;
            long.TryParse(dto.org_bin, out var orgBin);
            announcement.OrgBin = orgBin;
            announcement.OrgPid = dto.org_pid;
            announcement.RefBuyStatusId = dto.ref_buy_status_id;
            announcement.RefTypeTradeId = dto.ref_type_trade_id;
            announcement.DisablePersonId = dto.disable_person_id == 1;
            announcement.IdSupplier = dto.id_supplier != 0 ? dto.id_supplier : (int?) null;
            long.TryParse(dto.biin_supplier, out var biinSupplier);
            announcement.BiinSupplier = biinSupplier != 0 ? biinSupplier : (long?) null;
            announcement.ParentId = dto.parent_id != 0 ? dto.parent_id : (int?) null;
            announcement.SingleOrgSign = dto.singl_org_sign == 1;
            announcement.IsLightIndustry = dto.is_light_industry == 1;
            announcement.IsConstructionWork = dto.is_construction_work == 1;
            announcement.CustomerNameKz = !string.IsNullOrEmpty(dto.customer_name_kz) ? dto.customer_name_kz : null;
            announcement.CustomerNameRu = !string.IsNullOrEmpty(dto.customer_name_ru) ? dto.customer_name_ru : null;
            announcement.OrgNameKz = !string.IsNullOrEmpty(dto.org_name_kz) ? dto.org_name_kz : null;
            announcement.OrgNameRu = !string.IsNullOrEmpty(dto.org_name_ru) ? dto.org_name_ru : null;
            announcement.SystemId = dto.system_id;
            try
            {
                announcement.StartDate = DateTime.Parse(dto.start_date);
            }
            catch (Exception)
            {
                announcement.StartDate = null;
            }

            try
            {
                announcement.RepeatStartDate = DateTime.Parse(dto.repeat_start_date);
            }
            catch (Exception)
            {
                announcement.RepeatStartDate = null;
            }

            try
            {
                announcement.RepeatEndDate = DateTime.Parse(dto.repeat_end_date);
            }
            catch (Exception)
            {
                announcement.RepeatEndDate = null;
            }

            try
            {
                announcement.EndDate = DateTime.Parse(dto.end_date);
            }
            catch (Exception)
            {
                announcement.EndDate = null;
            }

            try
            {
                announcement.PublishDate = DateTime.Parse(dto.publish_date);
            }
            catch (Exception)
            {
                announcement.PublishDate = null;
            }

            try
            {
                announcement.ItogiDatePublic = DateTime.Parse(dto.itogi_date_public);
            }
            catch (Exception)
            {
                announcement.ItogiDatePublic = null;
            }

            try
            {
                announcement.DiscusStartDate = DateTime.Parse(dto.discus_start_date);
            }
            catch (Exception)
            {
                announcement.DiscusStartDate = null;
            }

            try
            {
                announcement.DiscusEndDate = DateTime.Parse(dto.discus_end_date);
            }
            catch (Exception)
            {
                announcement.DiscusEndDate = null;
            }

            try
            {
                announcement.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
                announcement.IndexDate = null;
            }

            return announcement;
        }
    }
}