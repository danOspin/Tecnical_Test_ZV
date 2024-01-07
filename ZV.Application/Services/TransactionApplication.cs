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
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Interfaces;
using ZV.Utilities.Static;

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

        public async Task<BaseResponse<bool>> RegisterMultipleTransactions(HashSet<TransactionRequestDto> transactions)
        {
            var response = new BaseResponse<bool>();

            /* Remover de la lista aquellos que fueron invalidos. Retornar aquellos que presentan problemas. Pero esto es secundario.
             * foreach (var user in users)
            {
                var validationResult = await _validationRules.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }
            }*/

            try
            {
                HashSet<TransactionInfo> transactionEntities = new HashSet<TransactionInfo>();
                foreach (var transactiondto in transactions)
                {
                    transactionEntities.Add(_mapper.Map<TransactionInfo>(transactiondto));
                }
                //var user = _mapper.Map<UserInfo>(userRequestDto);
                response.Data = await _unitOfWork.TransactionRepository.RegisterMultipleTransactions(transactionEntities);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public Task<BaseResponse<bool>> RegisterTransaction(TransactionRequestDto transaction)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TransactionResponseDto>> TransactionById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
