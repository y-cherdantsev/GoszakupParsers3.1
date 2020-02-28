/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 13:56:44
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class ParticipantParser : Parser
    {
        protected override List<ParserContext> InitParserContexts()
        {
            var parserContexts = new List<ParserContext>();
            for (var i = 0; i < NumOfDbConnections; i++)
                parserContexts.Add(new UnscrupulousGoszakupContext());
            return parserContexts;
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override ApiResponse JsonDeserializedObject(string response)
        {
            throw new NotImplementedException();
        }

        protected override List<object> Divide(ApiResponse apiResponse, int i)
        {
            throw new NotImplementedException();
        }

        protected override async Task ProcessObjects(object[] entities)
        {
            var participants = (ParticipantDto[]) entities;
            var participantGoszakupContext =
                (ParticipantGoszakupContext) Contexts[participants[0].pid % NumOfDbConnections];
            foreach (var entity in participants)
            {
                var participant = (ParticipantGoszakup) DtoToDb(entity);
                participantGoszakupContext.ParticipantsGoszakup.Add(participant);
                await participantGoszakupContext.SaveChangesAsync();
                lock (Lock)
                    Logger.Trace($"Left:{--Total}");
            }
        }

        protected override object DtoToDb(object dto)
        {
            var participantDto = (ParticipantDto) dto;
            var participantGoszakup = new ParticipantGoszakup();
            participantGoszakup.Pid = participantDto.pid;
            long.TryParse(participantDto.bin, out var bin);
            participantGoszakup.Bin = bin;
            long.TryParse(participantDto.iin, out var iin);
            participantGoszakup.Iin = iin;
            participantGoszakup.Inn = participantDto.inn;
            participantGoszakup.Unp = participantDto.unp;
            try { participantGoszakup.Regdate = DateTime.Parse(participantDto.regdate); }catch (Exception) { }
            try { participantGoszakup.IndexDate = DateTime.Parse(participantDto.index_date); }catch (Exception) { }
            try { participantGoszakup.LastUpdateDate = DateTime.Parse(participantDto.last_update_date); }catch (Exception) { }
            try { participantGoszakup.Crdate = DateTime.Parse($"{participantDto.crdate.ToString()}-01-01 00:00:00"); }catch (Exception) { }
            try { participantGoszakup.Year = DateTime.Parse($"{participantDto.year.ToString()}-01-01 00:00:00"); }catch (Exception) { }
            participantGoszakup.NumberReg = participantDto.number_reg;
            participantGoszakup.Series = participantDto.series;
            participantGoszakup.NameRu = participantDto.name_ru;
            participantGoszakup.NameKz = participantDto.name_kz;
            participantGoszakup.FullNameRu = participantDto.full_name_ru;
            participantGoszakup.FullNameKz = participantDto.full_name_kz;
            participantGoszakup.CountryCode = Convert.ToInt32(participantDto.country_code);
            participantGoszakup.Customer = participantDto.customer == 1;
            participantGoszakup.Organizer = participantDto.organizer == 1;
            participantGoszakup.MarkNationalCompany = participantDto.mark_national_company == 1;
            participantGoszakup.RefKopfCode = participantDto.ref_kopf_code;
            participantGoszakup.MarkAssocWithDisab = participantDto.mark_assoc_with_disab == 1;
            participantGoszakup.SystemId = participantDto.system_id;
            participantGoszakup.Supplier = participantDto.supplier == 1;
            participantGoszakup.TypeSupplier = participantDto.type_supplier;
            participantGoszakup.KrpCode = participantDto.krp_code;
            participantGoszakup.OkedList = participantDto.oked_list;
            participantGoszakup.KseCode = participantDto.kse_code;
            participantGoszakup.MarkWorldCompany = participantDto.mark_world_company == 1;
            participantGoszakup.MarkStateMonopoly = participantDto.mark_state_monopoly == 1;
            participantGoszakup.MarkNaturalMonopoly = participantDto.mark_natural_monopoly == 1;
            participantGoszakup.MarkPatronymicProducer = participantDto.mark_patronymic_producer == 1;
            participantGoszakup.MarkPatronymicSupplier = participantDto.mark_patronymic_supplyer == 1;
            participantGoszakup.MarkSmallEmployer = participantDto.mark_small_employer == 1;
            participantGoszakup.IsSingleOrg = participantDto.is_single_org == 1;
            participantGoszakup.Email = participantDto.email;
            participantGoszakup.Phone = participantDto.phone;
            participantGoszakup.Website = participantDto.website;
            participantGoszakup.Qvazi = participantDto.qvazi == 1;
            participantGoszakup.MarkResident = participantDto.mark_resident == 1;
            return participantGoszakup;
        }

        public ParticipantParser(Configuration.ParserSettings parserSettings, string authToken = null) : base(parserSettings, authToken)
        {
        }
    }
}*/