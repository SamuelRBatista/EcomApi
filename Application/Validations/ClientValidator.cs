using System.Text.RegularExpressions;
using Domain.Entities;
using FluentValidation;

namespace Application.Validations;


public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(c => c.Name)
        .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
        .MaximumLength(100).WithMessage("O nome do produto não pode ter mais que 100 caracteres. ");

        RuleFor(c => c.Cpf)
        .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
        .Must(BeAValidCpf).WithMessage("O CPF informado não é válido.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O e-mail não pode ser vazio.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");
        
        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("O número de telefone não pode ser vazio.")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("O número de telefone informado não é válido.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("A rua não pode ser vazia.")
            .Length(5, 100).WithMessage("A rua deve ter entre 5 e 100 caracteres.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("O bairro não pode ser vazio.")
            .Length(3, 100).WithMessage("O bairro deve ter entre 3 e 100 caracteres.");

        RuleFor(c => c.ZipCode)
            .NotEmpty().WithMessage("O CEP não pode ser vazio.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("O CEP deve estar no formato XXXXX-XXX.");
    }

      private bool BeAValidCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return false;

        cpf = cpf.Trim().Replace(".", "").Replace("-", ""); // Remover pontuação

        if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
            return false;

        // Cálculo do CPF (dois dígitos verificadores)
        int[] v1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] v2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var cpfArray = cpf.ToCharArray();

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (cpfArray[i] - '0') * v1[i];
        int mod = sum % 11;
        int firstDigit = mod < 2 ? 0 : 11 - mod;

        if (firstDigit != (cpfArray[9] - '0'))
            return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (cpfArray[i] - '0') * v2[i];
        mod = sum % 11;
        int secondDigit = mod < 2 ? 0 : 11 - mod;

        return secondDigit == (cpfArray[10] - '0');
    }


}