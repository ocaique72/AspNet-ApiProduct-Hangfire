using FluentValidation;

namespace apiDesafio.ViewModel
{
    public class UpdateCategoryViewModel
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryViewModelValidator : AbstractValidator<UpdateCategoryViewModel>
    {
        public UpdateCategoryViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .Length(2, 60).WithMessage("O nome da categoria deve ter entre 2 e 60 caracteres.");
        }
    }
}
