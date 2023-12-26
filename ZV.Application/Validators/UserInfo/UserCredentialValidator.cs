using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;

namespace ZV.Application.Validators.UserInfo
{
    internal class UserCredentialValidator : AbstractValidator<UserCredentialRequestDto>
    {
        public UserCredentialValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("El campo Nombre de usuario no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre de usuario no puede estar vacío");
            RuleFor(x => x.Pass)
                .NotNull().WithMessage("El campo Contraseña no puede ser nulo")
                .NotEmpty().WithMessage("El campo Contraseña no puede estar vacío");
        }
    }
}
