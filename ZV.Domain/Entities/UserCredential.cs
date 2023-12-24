using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class UserCredential
{
    public string CredentialId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public virtual UserInfo Credential { get; set; } = null!;
}
