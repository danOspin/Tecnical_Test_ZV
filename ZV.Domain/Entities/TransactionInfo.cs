using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class TransactionInfo
{
    public int TransCode { get; set; }

    public string? UserId { get; set; }

    public string? CommerceId { get; set; }

    public byte? TransPaymentMethod { get; set; }

    public short? TransStatus { get; set; }

    public string? TransConcept { get; set; }

    public DateTime? TransDate { get; set; }

    public decimal? TransTotal { get; set; }

    public DateTime? AuditChangeDate { get; set; }

    public virtual Commerce? Commerce { get; set; }

    public virtual PaymentMethod? TransPaymentMethodNavigation { get; set; }

    public virtual TransactionStatus? TransStatusNavigation { get; set; }

    public virtual ICollection<TransactionInfoAudit> TransactionInfoAudits { get; set; } = new List<TransactionInfoAudit>();

    public virtual UserInfo? User { get; set; }
}
