using AutoMapper;
using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Application.Interfaces;
using ZV.Application.Validators.UserInfo;
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Interfaces;
using ZV.Utilities.Static;

namespace ZV.Application.Services
{
    public class ClientApplication : IClientApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly CommerceValidator _validationRules;
        private readonly CredentialValidator _validationRules;

        public ClientApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_validationRules = validationRules;
        }

        public async Task<BaseResponse<ClientResponseDto>> ClientById(string clientid)
        {
            var response = new BaseResponse<ClientResponseDto>();
            /*var validationResult = await _validationRules.ValidateAsync(id);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }*/
            try
            {
                Client existingClient = await _unitOfWork.ClientRepository.GetClientComplete(clientid);

                if (existingClient != null)
                {
                    var clientDto = _mapper.Map<ClientResponseDto>(existingClient);
                    response.Data = clientDto;
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY;

                    //if () TODO: Validar que usuario exista en T_Credentials
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Errors = (IEnumerable<FluentValidation.Results.ValidationFailure>?)e.Data;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> CreateCredentials(ClientFilterRequest client)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var existingCreds = await _unitOfWork.ClientRepository.CheckCredsRegistry(client);
                if (existingCreds is not null && !string.IsNullOrEmpty(existingCreds.Pass))
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;

                    return response;
                }
                Client existingClient = await _unitOfWork.ClientRepository.GetClientBasicInfo(client.clientid);
               
                if (existingClient != null)
                {
                    response.Data = await _unitOfWork.ClientRepository.SetCredentials(client);
                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = ReplyMessage.MESSAGE_QUERY;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_FAILED;
                    }
                }

                return response;
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<BaseResponse<bool>> CheckCredentials(ClientFilterRequest client)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                Client existingClient = await _unitOfWork.ClientRepository.GetClientBasicInfo(client.clientid);
                if (existingClient != null)
                {
                    var dataResponse = await _unitOfWork.ClientRepository.CheckCredentials(client);
                    if (dataResponse.Equals(ReplyMessage.MESSAGE_QUERY))
                    {
                        response.Data = true;
                        response.IsSuccess = true;
                        response.Message = ReplyMessage.MESSAGE_QUERY;
                    }
                    else 
                    {
                        response.Data = false;
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_FAILED;
                    }
                }
            }
            catch (Exception e)
            {

                response.Data = false;
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public Task<BaseResponse<BaseEntityResponse<UserInfoRequestDto>>> ListUsers(BaseFilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> RegisterClient(UserInfoRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> RegisterClients(HashSet<UserInfoRequestDto> users)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<BaseEntityResponse<UserInfoResponseDto>>> IClientApplication.ListUsers(BaseFilterRequest filter)
        {
            throw new NotImplementedException();
        }
    }
}
