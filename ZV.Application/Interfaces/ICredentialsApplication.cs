using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;

namespace ZV.Application.Interfaces
{
    public interface ICredentialsApplication
    {
        Task<BaseResponse<bool>> RegisterCredential(UserCredentialRequestDto credentials);
        Task<BaseResponse<bool>> EditCredential(UserCredentialRequestDto credentials);
    }
}
