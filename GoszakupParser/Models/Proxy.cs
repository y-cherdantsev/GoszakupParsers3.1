using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using NpgsqlTypes;

namespace GoszakupParser.Models
{
    [Table("proxies")]
    public class Proxy
    {
        [Column("address")] public IPAddress Address { get; set; }
        [Column("port")] public int Port { get; set; }
        [Column("username")] public string Username { get; set; }
        [Column("password")] public string Password { get; set; }
        [Column("status")] public bool? Status { get; set; }
        [Column("relevant_from")] public DateTime? RelevantFrom { get; set; }
        [Column("relevant_until")] public DateTime? RelevantUntil { get; set; }
    }
}