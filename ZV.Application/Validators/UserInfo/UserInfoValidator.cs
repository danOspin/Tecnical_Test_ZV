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
            RuleFor(x => x._userId)
                .NotNull().WithMessage("El campo usuario_identificacion no puede ser nulo")
                .NotEmpty().WithMessage("El campo usuario_identificacion no puede estar vacío");
            RuleFor(x => x._userId)
                .NotNull().WithMessage("El campo usuario_Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo usuario_Nombre no puede estar vacío");

        }
    }
}
