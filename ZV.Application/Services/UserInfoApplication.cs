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
    public class UserInfoApplication : IUserInfoApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserInfoValidator _validationRules;

        public UserInfoApplication(IUnitOfWork unitOfWork, IMapper mapper, UserInfoValidator validationRules)
        {
            _validationRules = validationRules;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<BaseEntityResponse<UserInfoResponseDto>>> ListUsers(BaseFilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserInfoRequestDto userRequestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(userRequestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }
            try
            {
                var user = _mapper.Map<UserInfo>(userRequestDto);
                response.Data = await _unitOfWork.UserInfoRepository.RegisterUserInfo(user);

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
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUsers(HashSet<UserInfoRequestDto> users)
        {
            var response = new BaseResponse<bool>();
            //var validationResult = await _validationRules.ValidateAsync(userRequestDto);

            /*if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }*/
            try
            {
                HashSet<UserInfo> userInfos = new HashSet<UserInfo>();
                foreach (var userdto in users)
                {
                    userInfos.Add(_mapper.Map<UserInfo>(userdto));
                }
                //var user = _mapper.Map<UserInfo>(userRequestDto);
                response.Data = await _unitOfWork.UserInfoRepository.RegisterMultipleUserInfo(userInfos);

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


        public Task<BaseResponse<UserInfoResponseDto>> UserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
