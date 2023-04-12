using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace apiDesafio.ViewModel
{
    public class EditorCategoryViewModel
    {     
        public string Name { get; set; }
    }

    public class CreateCategoryViewModelValidator : AbstractValidator<EditorCategoryViewModel>
    {
        public CreateCategoryViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .Length(2, 60).WithMessage("O nome da categoria deve ter entre 2 e 60 caracteres.");
        }
    }
}
