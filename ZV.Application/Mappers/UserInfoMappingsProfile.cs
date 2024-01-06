using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Application.Mappers
{
    public class UserInfoMappingsProfile : Profile
    {
        public UserInfoMappingsProfile() 
        {
            CreateMap<UserInfo, UserInfoResponseDto>().ReverseMap();
            CreateMap<BaseEntityResponse<UserInfo>, BaseEntityResponse<UserInfoResponseDto>>().ReverseMap();
            CreateMap<UserInfoRequestDto, UserInfo>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src._userId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src._userName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src._email));
        }

    }
}
