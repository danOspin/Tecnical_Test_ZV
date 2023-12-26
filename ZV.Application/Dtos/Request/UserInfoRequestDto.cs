using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Application.Dtos.Request
{
    public class UserInfoRequestDto
    {
        public string _userId { get; set; } = null!;
        public string? _userName { get; set; }
        public string? _email { get; set; }
        public bool? _userStatus { get; set; }


        public UserInfoRequestDto(string UserId, string username, string email)
        {
            _userId = UserId;
            _userName = username;
            _email = email;
            _userStatus = true;
        }
    }
}
