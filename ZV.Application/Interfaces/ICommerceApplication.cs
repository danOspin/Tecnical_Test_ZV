using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Application.Interfaces
{
    public interface ICommerceApplication
    {
        Task<BaseResponse<BaseEntityResponse<CommerceResponseDto>>> ListCommerce(BaseFilterRequest filter);
        Task<BaseResponse<CommerceResponseDto>> CommerceById(string id);
        Task<BaseResponse<bool>> RegisterCommerce(CommerceRequestDto commerce);
        Task<BaseResponse<bool>> RegisterMultipleCommerces(HashSet<CommerceRequestDto> commerces);
    }
}
