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
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<TransactionInfo, TransactionRequestDto>().ReverseMap();
            CreateMap<BaseEntityResponse<TransactionInfo>, BaseEntityResponse<TransactionResponseDto>>().ReverseMap();
            CreateMap<TransactionRequestDto, TransactionInfo>()
                    .ForMember(dest => dest.TransCode, opt => opt.MapFrom(src => src._trans_code))
                    .ForMember(dest => dest.TransPaymentMethod, opt => opt.MapFrom(src => src._trans_payment_method))
                    .ForMember(dest => dest.TransStatus, opt => opt.MapFrom(src => src._trans_status))
                    .ForMember(dest => dest.TransDate, opt => opt.MapFrom(src => src._trans_date))
                    .ForMember(dest => dest.TransConcept, opt => opt.MapFrom(src => src._trans_concept))
                    .ForMember(dest => dest.TransTotal, opt => opt.MapFrom(src => src._trans_total))
                    .ForMember(dest => dest.CommerceId, opt => opt.MapFrom(src => src._commerce_code))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src._user_id));

            CreateMap<TransactionInfo,TransactionResponseDto>()
                    .ForMember(dest => dest._trans_code, opt => opt.MapFrom(src => src.TransCode))
                    .ForMember(dest => dest._trans_payment_method, opt => opt.MapFrom(src => src.TransPaymentMethod))
                    .ForMember(dest => dest._trans_status, opt => opt.MapFrom(src => src.TransStatus))
                    .ForMember(dest => dest._trans_date, opt => opt.MapFrom(src => src.TransDate))
                    .ForMember(dest => dest._trans_concept, opt => opt.MapFrom(src => src.TransConcept))
                    .ForMember(dest => dest._trans_total, opt => opt.MapFrom(src => src.TransTotal))
                    .ForMember(dest => dest._commerce_code, opt => opt.MapFrom(src => src.CommerceId))
                    .ForMember(dest => dest._user_id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}/* 
        public string _trans_code { get; set; }
        public byte _trans_payment_method { get; set; }
        public byte _trans_status {  get; set; }
        public decimal _trans_total {  get; set; } 
        public DateTime _trans_date {  get; set; }
        public string _trans_concept { get; set; }
        public string _commerce_code { get; set; }
        public string _user_id {  get; set; }
*/
