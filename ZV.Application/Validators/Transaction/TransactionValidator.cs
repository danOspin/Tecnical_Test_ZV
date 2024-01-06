using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Dtos.Request;

namespace ZV.Application.Validators.Transaction
{
    public class TransactionValidator : AbstractValidator<TransactionRequestDto>
    {
        public TransactionValidator()
        {
            RuleFor(x => x._trans_code)
                .NotNull().WithMessage("El campo trans_codigo no puede ser nulo")
                .NotEmpty().WithMessage("El campo trans_codigo no puede estar vacío");
            RuleFor(x => x._commerce_code)
                .NotNull().WithMessage("El campo comercio_codigo no puede ser nulo")
                .NotEmpty().WithMessage("El campo comercio_codigo no puede estar vacío"); 
            RuleFor(x => x._user_id)
                .NotNull().WithMessage("El campo usuario_identificacion no puede ser nulo")
                .NotEmpty().WithMessage("El campo usuario_identificacion no puede estar vacío");

        }
    }
}
