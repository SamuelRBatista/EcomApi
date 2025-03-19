using Domain.Entities;
using FluentValidation;

namespace Application.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
        .NotEmpty().WithMessage("O nome do produto é obrigatório.")
        .MaximumLength(100).WithMessage("O nome do produto não pode ter mais que 100 caracteres. ");

        RuleFor(p =>p.Price)
        .GreaterThan(0).WithMessage("O preço deve ser maior que zero. ");

        RuleFor(p => p.BarCode)
        .NotEmpty().WithMessage("O código de barras é obrigatório.")
        .Length(13).WithMessage("O código de barras deve ter exatamente 13 caracteres. ");
    }

}

