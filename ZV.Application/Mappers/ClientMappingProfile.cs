using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Domain.Entities;

namespace ZV.Application.Mappers
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<UserInfoRequestDto, Client>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src._userId));
            CreateMap<CommerceRequestDto, Client>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src._commerce_code));
            CreateMap<Client, ClientResponseDto>()
                .ForMember(dest => dest._clientid, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest._clienttype, opt => opt.MapFrom(src => src.ClientType))
                .ForMember(dest => dest._commerce, opt => opt.MapFrom(src => src.Commerce))
                .ForMember(dest => dest._user, opt => opt.MapFrom(src => src.UserInfo));
        }

    }

}
