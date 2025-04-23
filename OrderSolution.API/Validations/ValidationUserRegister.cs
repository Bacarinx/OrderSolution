using FluentValidation;
using OrderSolution.API.UseCases.User;
using OrderSolution.Comunication.Requests;
using System.Data;

namespace OrderSolution.API.Validations
{
    public class ValidationUserRegister : AbstractValidator<RequestUserRegisterJson>
    {
        public ValidationUserRegister()
        {
            RuleFor(caso => caso.Name).NotEmpty().WithMessage("O campo Nome do Usuário não pode ser vazio!");

            RuleFor(caso => caso.Email).EmailAddress().WithMessage("Preencha o campo de e-mail com um valor válido!");

            RuleFor(caso => caso.Password).NotEmpty().WithMessage("O campo Senha não pode ser vazio!");
            When(caso => !String.IsNullOrWhiteSpace(caso.Password), () =>
            {
                RuleFor(caso => caso.Password.Length).GreaterThanOrEqualTo(8).WithMessage("A senha precisa ter no mínimo 8 caracteres");
            });

            RuleFor(caso => caso.CNPJ).NotEmpty().WithMessage("O campo CNPJ não pode ser vazio!");

            RuleFor(caso => caso.Address).NotEmpty().WithMessage("O campo Endereço não pode ser vazio!");
        }
    }
}
