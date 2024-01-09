using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class TransactionInfoAudit
{
    public int AuditId { get; set; }

    public int? TransCode { get; set; }

    public byte? TransPaymentMethod { get; set; }

    public short? TransStatus { get; set; }

    public DateTime? ChangeDate { get; set; }

    public string? ChangedBy { get; set; }

    public string? Annotation { get; set; }

    public virtual TransactionInfo? TransCodeNavigation { get; set; }

    public virtual PaymentMethod? TransPaymentMethodNavigation { get; set; }

    public virtual TransactionStatus? TransStatusNavigation { get; set; }
}
