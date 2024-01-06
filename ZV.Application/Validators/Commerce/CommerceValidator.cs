using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;

namespace ZV.Application.Validators.Commerce
{
    public class CommerceValidator : AbstractValidator<CommerceRequestDto>
    {
        public CommerceValidator()
        {
            RuleFor(x => x._commerce_code)
                .NotNull().WithMessage("El campo comercio_codigo no puede ser nulo")
                .NotEmpty().WithMessage("El campo comercio_codigo no puede estar vacío");
           
        }
    }
}
