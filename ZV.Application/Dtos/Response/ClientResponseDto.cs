using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Application.Dtos.Response
{
    public class ClientResponseDto
    {
        public string _clientid { get; set; }
        public string _clienttype { get; set; }
        public UserInfoResponseDto _user { get; set; }
        public CommerceResponseDto _commerce { get; set; }
    }
}
