using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Application.Interfaces
{
    public interface IUserInfoApplication
    {
        Task<BaseResponse<BaseEntityResponse<UserInfoResponseDto>>> ListUsers(BaseFilterRequest filter);
        Task<BaseResponse<UserInfoResponseDto>> UserById(string id);
        Task<BaseResponse<bool>> RegisterUser(UserInfoRequestDto user);
        Task<BaseResponse<bool>> RegisterUsers(HashSet<UserInfoRequestDto> users);
    }
}
