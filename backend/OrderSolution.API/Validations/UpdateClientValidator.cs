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

            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}