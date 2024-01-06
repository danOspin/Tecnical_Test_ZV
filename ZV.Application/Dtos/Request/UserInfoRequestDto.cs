using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Api.Controllers.Helper;

namespace ZV.Application.Dtos.Request
{
    public class UserInfoRequestDto
    {
        public string _userId { get; set; } = null!;
        public string? _userName { get; set; }
        public string? _email { get; set; }

        /// <summary>
        /// Constructor que recibe objeto raw transaction proveniente del json que se responde al 
        /// hacer petición al servidor.
        /// </summary>
        /// <param name="transaction"></param>
        public UserInfoRequestDto (RawTransaction transaction)
        {
            _userId = transaction.usuario_identificacion;
            _userName = transaction.usuario_nombre;
            _email = transaction.usuario_email;
        }
        public UserInfoRequestDto(string UserId, string username, string email)
        {
            _userId = UserId;
            _userName = username;
            _email = email;
        }
    }
}
