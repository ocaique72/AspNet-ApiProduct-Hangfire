using FluentValidation;

namespace apiDesafio.ViewModel
{
    public class UpdateProductLogViewModel
    {
        public int ProductId { get; set; }
        public string ProductJson { get; set; }
    }

    public class UpdateProductLogViewModelValidator : AbstractValidator<UpdateProductLogViewModel>
    {
        public UpdateProductLogViewModelValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("O campo ProductId é obrigatório.")
                .Must(x => int.TryParse(x.ToString(), out _)).WithMessage("O campo ProductId deve conter somente números.");

            RuleFor(x => x.ProductJson)
                .MaximumLength(50).WithMessage("O campo ProductJson deve ter no máximo 50 caracteres.");
        }
    }
}
