using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class TCredential
{

    public string CredentialId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public string? Salt { get; set; }

    public virtual Client Credential { get; set; } = null!;
}
