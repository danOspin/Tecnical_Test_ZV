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
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Interfaces;
using ZV.Utilities.Static;

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

        public async Task<BaseResponse<bool>> RegisterMultipleCommerces(HashSet<CommerceRequestDto> commerces)
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
                HashSet<Commerce> commerceEntities = new HashSet<Commerce>();
                foreach (var commercedto in commerces)
                {
                    commerceEntities.Add(_mapper.Map<Commerce>(commercedto));
                }
                //var user = _mapper.Map<UserInfo>(userRequestDto);
                response.Data = await _unitOfWork.CommerceRepository.RegisterMultipleCommerces(commerceEntities);

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
    }
}
