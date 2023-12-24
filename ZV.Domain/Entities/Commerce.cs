using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class Commerce
{
    public int CommerceId { get; set; }

    public string? CommerceName { get; set; }

    public string? CommerceAddress { get; set; }

    public string? Nit { get; set; }

    public bool? CommerceStatus { get; set; }

    public virtual ICollection<CommercePerUser> CommercePerUsers { get; set; } = new List<CommercePerUser>();

    public virtual ICollection<TransactionInfo> TransactionInfos { get; set; } = new List<TransactionInfo>();
}
