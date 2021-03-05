using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 09:33:10
    /// <summary>
    /// proxies Parsing DB table field
    /// </summary>
    [Table("proxies", Schema = "monitoring")]
    public sealed class Proxy : BaseModel
    {
        public override void BuildModel(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Proxy>().HasKey(proxy => new {proxy.Address, proxy.Port});

        [Column("address")] public IPAddress Address { get; set; }
        [Column("port")] public int Port { get; set; }
        [Column("username")] public string Username { get; set; }
        [Column("password")] public string Password { get; set; }
        [Column("status")] public bool? Status { get; set; }
        [Column("relevant_from")] public DateTime? RelevantFrom { get; set; }
        [Column("relevant_until")] public DateTime? RelevantUntil { get; set; }
    }
}