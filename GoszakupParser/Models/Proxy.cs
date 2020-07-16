using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 09:33:10
    /// <summary>
    /// proxies Parsing DB table field
    /// </summary>
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