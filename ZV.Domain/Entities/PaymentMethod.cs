using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class PaymentMethod
{
    public byte PaymentMethodId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TransactionInfoAudit> TransactionInfoAudits { get; set; } = new List<TransactionInfoAudit>();

    public virtual ICollection<TransactionInfo> TransactionInfos { get; set; } = new List<TransactionInfo>();
}
