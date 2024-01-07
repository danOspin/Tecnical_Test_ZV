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
    public class CommerceMappingProfile : Profile
    {
        public CommerceMappingProfile() 
        {
            CreateMap<Commerce, CommerceRequestDto>().ReverseMap();
            CreateMap<BaseEntityResponse<Commerce>, BaseEntityResponse<CommerceResponseDto>>().ReverseMap();
            CreateMap<CommerceRequestDto, Commerce>()
                .ForMember(dest => dest.CommerceId, opt => opt.MapFrom(src => src._commerce_code))
                .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src._commerce_name))
                .ForMember(dest => dest.CommerceAddress, opt => opt.MapFrom(src => src._commerce_address))
                .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src._commerce_nit))
                .ForMember(dest => dest.CommerceStatus, opt => opt.MapFrom(src => src._commerce_status));
            CreateMap<Commerce, CommerceResponseDto>()
                .ForMember(dest => dest._commerce_code, opt => opt.MapFrom(src => src.CommerceId))
                .ForMember(dest => dest._commerce_name, opt => opt.MapFrom(src => src.CommerceName))
                .ForMember(dest => dest._commerce_address, opt => opt.MapFrom(src => src.CommerceAddress))
                .ForMember(dest => dest._commerce_nit, opt => opt.MapFrom(src => src.Nit))
                .ForMember(dest => dest._commerce_status, opt => opt.MapFrom(src => src.CommerceStatus));
        }
    }
}
