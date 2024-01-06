using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Application.Interfaces;
using ZV.Application.Validators.Commerce;
using ZV.Application.Validators.Transaction;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Application.Services
{
    public class CommerceApplication : ICommerceApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CommerceValidator _validationRules;

        public CommerceApplication(IUnitOfWork unitOfWork, IMapper mapper, CommerceValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public Task<BaseResponse<CommerceResponseDto>> CommerceById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<BaseEntityResponse<CommerceResponseDto>>> ListCommerce(BaseFilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> RegisterCommerce(CommerceRequestDto commerce)
        {
            throw new NotImplementedException();
        }
    }
}
