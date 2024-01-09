using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public bool? ClientStatus { get; set; }

    public string? ClientType { get; set; }

    public virtual Commerce? Commerce { get; set; }

    public virtual TCredential? TCredential { get; set; }

    public virtual UserInfo? UserInfo { get; set; }
}
