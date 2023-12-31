﻿using System;
using System.Collections.Generic;

namespace ZV.Domain.Entities;

public partial class UserInfo
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool? UserStatus { get; set; }

    public virtual ICollection<TransactionInfo> TransactionInfos { get; set; } = new List<TransactionInfo>();

    public virtual Client User { get; set; } = null!;
}
