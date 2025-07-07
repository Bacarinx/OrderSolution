using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using OrderSolution.Comunication.Requests;

namespace OrderSolution.API.Validations
{
    public class ValidationCPF : AbstractValidator<RequestNewClient>
    {
        public ValidationCPF()
        {
            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O CPF é Obrigatório.")
                .Must(IsCpfValid).WithMessage("CPF Inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .EmailAddress();
        }

        private bool IsCpfValid(string cpf)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", "");
            // CPFs inválidos conhecidos
            string[] invalidCpfs =
            {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777",
            "88888888888", "99999999999"
        };

            if (invalidCpfs.Contains(cpf)) return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * mult1[i];

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;

            tempCpf += digit1;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * mult2[i];

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;

            return cpf.EndsWith($"{digit1}{digit2}");
        }
    }
}