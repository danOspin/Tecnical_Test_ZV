using AutoMapper;
using Chronic.Core.Tags.Repeaters;
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
        public async Task<BaseResponse<BaseEntityResponse<TransactionResponseDto>>> ListTransaction(TransactionsPerClientRequestDto filter)
        {
            var response = new BaseResponse<BaseEntityResponse<TransactionResponseDto>>();
            //TODO: Validación de campos antes de ingresar a repository.
            var transactionResponse = await _unitOfWork.TransactionRepository.ListTransaction(filter);
            try
            {
                if (transactionResponse.CountRegister > 0)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<TransactionResponseDto>>(transactionResponse);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message += e.ToString();
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterMultipleTransactions(HashSet<TransactionRequestDto> transactions)
        {
            var response = new BaseResponse<bool>();

            foreach (var transaction in transactions)
            {
                var validationResult = await _validationRules.ValidateAsync(transaction);
                if (!validationResult.IsValid)
                {
                    transactions.Remove(transaction);
                }
            }
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
