using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoszakupParser.Models.ParsingModels
{
    
    [Table("plan_goszakup")]
    public class PlanGoszakup : DbLoggerCategory.Model
    {
        [Key] [Column("id")] public long Id{get; set;}
    }
}