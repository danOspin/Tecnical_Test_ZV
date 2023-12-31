﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

            //Remover de la lista aquellos que fueron invalidos. Retornar aquellos que presentan problemas. Pero esto es secundario.
            foreach (var user in users)
            {
                var validationResult = await _validationRules.ValidateAsync(user);
                if (!validationResult.IsValid)
                {

                    Console.WriteLine("Usuario removido por id nulo");
                    users.Remove(user);
                }
            }

            try
            {
                HashSet<UserInfo> userInfos = new HashSet<UserInfo>();
                HashSet<Client> clientInfos = new HashSet<Client>();
                foreach (var userdto in users)
                {
                    clientInfos.Add(_mapper.Map <Client>(userdto));
                    userInfos.Add(_mapper.Map<UserInfo>(userdto));
                }
                response.Data = await _unitOfWork.ClientRepository.RegisterMultipleUserInfo(clientInfos, "Pagador");
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
        public async Task<BaseResponse<UserInfoResponseDto>> UserById(string userid)
        {
            var response = new BaseResponse<UserInfoResponseDto>();
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
                UserInfo test_user = await _unitOfWork.UserInfoRepository.GetUserInfo(userid);

                if (test_user != null)
                {

                    var user = _mapper.Map<UserInfoResponseDto>(test_user);
                    response.Data = user;
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY;
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

        public async Task<BaseResponse<UserInfoResponseDto>> UserCredentialStatus(string userid)
        {
            var response = new BaseResponse<UserInfoResponseDto>();
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
                UserInfo test_user = await _unitOfWork.UserInfoRepository.GetUserInfo(userid);
                Client test = await _unitOfWork.ClientRepository.GetClientBasicInfo(userid);
                //test.
                if (test_user != null)
                {
                    var user = _mapper.Map<UserInfoResponseDto>(test_user);
                    response.Data = user;
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY;

                    /*if (test_user. is null)
                    {
                        response.Data = user;
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_NO_PASS;
                    }*/
                    
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
    }
}
