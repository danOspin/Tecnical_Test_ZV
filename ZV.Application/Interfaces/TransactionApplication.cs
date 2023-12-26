using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Services;
using ZV.Application.Validators.UserInfo;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Application.Interfaces
{
    public class TransactionApplication : ITransactionApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserInfoValidator validationRules;


    }
}
