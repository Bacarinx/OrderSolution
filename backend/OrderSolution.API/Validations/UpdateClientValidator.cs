using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OrderSolution.Comunication.Requests;

namespace OrderSolution.API.Validations
{
    public class UpdateClientValidator : AbstractValidator<RequestUpdateClient>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.Gender)
                .Must(g => g == "M" || g == "F")
                .WithMessage("O Gênero deve ser 'M' ou 'F'.");

            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}