using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class TransactionInfo
{
    public int TransCode { get; set; }

    public string? UserId { get; set; }

    public int? CommerceId { get; set; }

    public byte? TransPaymentMethod { get; set; }

    public short? TransStatus { get; set; }

    public string? TransConcept { get; set; }

    public DateOnly? TransDate { get; set; }

    public decimal? TransTotal { get; set; }

    public virtual Commerce? Commerce { get; set; }

    public virtual UserInfo? User { get; set; }
}
