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
using ZV.Application.Validators.Transaction;
using ZV.Application.Validators.UserInfo;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Application.Services
{
    public class TransactionApplication : ITransactionApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TransactionValidator _validationRules;
        public TransactionApplication(IUnitOfWork unitOfWork, IMapper mapper, TransactionValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<TransactionResponseDto>>> ListTransaction(BaseFilterRequest filter)
        {
            var response = new BaseResponse<BaseEntityResponse<TransactionResponseDto>>();
            //var categories = await _unitOfWork.
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> RegisterUser(TransactionRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TransactionResponseDto>> TransactionById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
