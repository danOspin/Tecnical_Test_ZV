using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class Commerce
{
    public string CommerceId { get; set; } = null!;

    public string? CommerceName { get; set; }

    public string? CommerceAddress { get; set; }

    public string? CommerceNit { get; set; }

    public bool? CommerceStatus { get; set; }

    public virtual Client CommerceNavigation { get; set; } = null!;

    public virtual ICollection<TransactionInfo> TransactionInfos { get; set; } = new List<TransactionInfo>();
}
