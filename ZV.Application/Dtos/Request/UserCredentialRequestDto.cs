using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Application.Dtos.Request
{
    public class UserCredentialRequestDto
    {
        public string? Username { get; set; }

        public string? Pass { get; set; }

        public string? Userid { get; set; }
    }
}
