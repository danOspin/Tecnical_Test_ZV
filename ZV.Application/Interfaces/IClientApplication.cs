using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<BaseResponse<BaseEntityResponse<UserInfoResponseDto>>> ListUsers(BaseFilterRequest filter);
        Task<BaseResponse<ClientResponseDto>> ClientById(string id);
        Task<BaseResponse<bool>> RegisterClient(UserInfoRequestDto clients);
        Task<BaseResponse<bool>> RegisterClients(HashSet<UserInfoRequestDto> clients);
        Task<BaseResponse<bool>> CreateCredentials(ClientFilterRequest client);
        Task<BaseResponse<bool>> CheckCredentials(ClientFilterRequest client);
    }
}
