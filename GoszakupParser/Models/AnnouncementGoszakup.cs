using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models
{


	/// @author Yevgeniy Cherdantsev
	/// @date 26.02.2020 18:42:59
	/// @version 1.0
	/// <summary>
	/// INPUT
	/// </summary>
	[Table("announcement_goszakup")]
	public class AnnouncementGoszakup: DbLoggerCategory.Model
	{
		[Key] [Column("id")] public int? Id{get; set;}
		[Column("number_anno")] public string NumberAnno{get; set;}
		[Column("name_ru")] public string NameRu{get; set;}
		[Column("name_kz")] public string NameKz{get; set;}
		[Column("total_sum")] public double? TotalSum{get; set;}
		[Column("ref_trade_methods_id")] public int? RefTradeMethodsId{get; set;}
		[Column("ref_subject_type_id")] public int? RefSubjectTypeId{get; set;}
		[Column("customer_bin")] public long? CustomerBin{get; set;}
		[Column("customer_pid")] public int? CustomerPid{get; set;}
		[Column("org_bin")] public long? OrgBin{get; set;}
		[Column("org_pid")] public int? OrgPid{get; set;}
		[Column("ref_buy_status_id")] public int? RefBuyStatusId{get; set;}
		[Column("start_date")] public DateTime? StartDate{get; set;}
		[Column("repeat_start_date")] public DateTime? RepeatStartDate{get; set;}
		[Column("repeat_end_date")] public DateTime? RepeatEndDate{get; set;}
		[Column("end_date")] public DateTime? EndDate{get; set;}
		[Column("publish_date")] public DateTime? PublishDate{get; set;}
		[Column("itogi_date_public")] public DateTime? ItogiDatePublic{get; set;}
		[Column("ref_type_trade_id")] public int? RefTypeTradeId{get; set;}
		[Column("disable_person_id")] public bool? DisablePersonId{get; set;}
		[Column("discus_start_date")] public DateTime? DiscusStartDate{get; set;}
		[Column("discus_end_date")] public DateTime? DiscusEndDate{get; set;}
		[Column("id_supplier")] public int? IdSupplier{get; set;}
		[Column("biin_supplier")] public long? BiinSupplier{get; set;}
		[Column("parent_id")] public int? ParentId{get; set;}
		[Column("single_org_sign")] public bool? SingleOrgSign{get; set;}
		[Column("is_light_industry")] public bool? IsLightIndustry{get; set;}
		[Column("is_construction_work")] public bool? IsConstructionWork{get; set;}
		[Column("customer_name_kz")] public string CustomerNameKz{get; set;}
		[Column("customer_name_ru")] public string CustomerNameRu{get; set;}
		[Column("org_name_kz")] public string OrgNameKz{get; set;}
		[Column("org_name_ru")] public string OrgNameRu{get; set;}
		[Column("system_id")] public int? SystemId{get; set;}
		[Column("index_date")] public DateTime? IndexDate{get; set;}
		// [Column("relevance")] public DateTime? Relevance{get; set;}
		
	}
}
