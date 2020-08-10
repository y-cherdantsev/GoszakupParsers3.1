using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.ParsingModels
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:43:17
    /// <summary>
    /// Participant Parsing DB table object
    /// </summary>
    [Table("participant_goszakup")]
    public class ParticipantGoszakup : BaseModel
    {
        [Key] [Column("pid")] public int? Pid { get; set; }
        [Column("bin")] public long? Bin { get; set; }
        [Column("iin")] public long? Iin { get; set; }
        [Column("inn")] public string Inn { get; set; }
        [Column("unp")] public string Unp { get; set; }
        [Column("regdate")] public DateTime? Regdate { get; set; }
        [Column("crdate")] public DateTime? Crdate { get; set; }
        [Column("index_date")] public DateTime? IndexDate { get; set; }
        [Column("number_reg")] public string NumberReg { get; set; }
        [Column("series")] public string Series { get; set; }
        [Column("name_ru")] public string NameRu { get; set; }
        [Column("name_kz")] public string NameKz { get; set; }
        [Column("full_name_ru")] public string FullNameRu { get; set; }
        [Column("full_name_kz")] public string FullNameKz { get; set; }
        [Column("country_code")] public int? CountryCode { get; set; }
        [Column("customer")] public bool? Customer { get; set; }
        [Column("organizer")] public bool? Organizer { get; set; }
        [Column("mark_national_company")] public bool? MarkNationalCompany { get; set; }
        [Column("ref_kopf_code")] public string RefKopfCode { get; set; }
        [Column("mark_assoc_with_disab")] public bool? MarkAssocWithDisab { get; set; }
        [Column("system_id")] public int? SystemId { get; set; }
        [Column("supplier")] public bool? Supplier { get; set; }
        [Column("krp_code")] public int? KrpCode { get; set; }
        [Column("oked_list")] public int? OkedList { get; set; }
        [Column("kse_code")] public int? KseCode { get; set; }
        [Column("mark_world_company")] public bool? MarkWorldCompany { get; set; }
        [Column("mark_state_monopoly")] public bool? MarkStateMonopoly { get; set; }
        [Column("mark_natural_monopoly")] public bool? MarkNaturalMonopoly { get; set; }
        [Column("mark_patronymic_producer")] public bool? MarkPatronymicProducer { get; set; }
        [Column("mark_patronymic_supplier")] public bool? MarkPatronymicSupplier { get; set; }
        [Column("mark_small_employer")] public bool? MarkSmallEmployer { get; set; }
        [Column("is_single_org")] public bool? IsSingleOrg { get; set; }
        [Column("email")] public string Email { get; set; }
        [Column("phone")] public string Phone { get; set; }
        [Column("website")] public string Website { get; set; }
        [Column("last_update_date")] public DateTime? LastUpdateDate { get; set; }
        [Column("qvazi")] public bool? Qvazi { get; set; }
        [Column("year")] public DateTime? Year { get; set; }
        [Column("mark_resident")] public bool? MarkResident { get; set; }
        [Column("type_supplier")] public int? TypeSupplier { get; set; }
    }
}