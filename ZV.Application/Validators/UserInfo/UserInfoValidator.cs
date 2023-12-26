using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;

namespace ZV.Application.Validators.UserInfo
{
    internal class UserInfoValidator : AbstractValidator<UserInfoRequestDto>
    {
        public UserInfoValidator() 
        { 
            //RuleFor(x => x.UserId).NotNull().WithMessage("El campo ")
        }
    }
}
