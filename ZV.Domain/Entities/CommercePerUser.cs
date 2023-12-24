using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class CommercePerUser
{
    public int Id { get; set; }

    public int? CommerceId { get; set; }

    public string? UserId { get; set; }

    public virtual Commerce? Commerce { get; set; }

    public virtual UserInfo? User { get; set; }
}
