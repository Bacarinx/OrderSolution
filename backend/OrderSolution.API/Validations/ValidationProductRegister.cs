using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OrderSolution.Comunication.Requests;

namespace OrderSolution.API.Validations
{
    public class ValidationProductRegister : AbstractValidator<RequestNewProduct>
    {
        public ValidationProductRegister()
        {
            RuleFor(product => product.CategoryId).Must(categoryId => categoryId != 0).WithMessage("Produto precisa contem uma categoria correlacionada!");
            RuleFor(product => product.Name).NotNull().WithMessage("Produto precisa de um nome!");
        }
    }
}