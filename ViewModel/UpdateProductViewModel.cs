using FluentValidation;

namespace apiDesafio.ViewModel
{
    public class UpdateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductViewModelValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo Name não pode estar vazio.")
                .MaximumLength(50).WithMessage("O campo Name deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("O campo Description deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("O campo Price não pode estar vazio.")
                .GreaterThan(0).WithMessage("O campo Price deve ser maior que zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("O campo CategoryId não pode estar vazio.")
                .Must(x => int.TryParse(x.ToString(), out _)).WithMessage("O campo ProductId deve conter somente números.");
        }
    }
}
