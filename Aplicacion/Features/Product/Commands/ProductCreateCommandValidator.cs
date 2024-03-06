using FluentValidation;

namespace Tekton.Application.Features.Product.Commands
{
    public class ProductCreateCommandValidator : AbstractValidator<ProductCreateCommand>
    {

        public ProductCreateCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Nombre no puede estar vacio")
               .NotNull()
               .MaximumLength(50).WithMessage("Nombre no puede exceder los 50 caracteres");

            RuleFor(p => p.Description)
               .NotEmpty().WithMessage("Descripción no puede estar vacio")
               .NotNull()
               .MaximumLength(100).WithMessage("Descripción no puede exceder los 100 caracteres");

            RuleFor(p => p.SKU)
               .NotEmpty().WithMessage("SKU no puede estar vacio")
               .NotNull()
               .MaximumLength(20).WithMessage("SKU no puede exceder los 20 caracteres");

            RuleFor(p => p.Price)
                 .NotEmpty().WithMessage("Precio debe ser mayor a 0")
                 .GreaterThan(0);

            RuleFor(x => x.StatusId)
            .InclusiveBetween(0, 1)
            .WithMessage("Status debe ser 0 o 1.");

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("La {Stock} debe ser mayor a 0")
                .GreaterThanOrEqualTo(0);
        }

    }
}

