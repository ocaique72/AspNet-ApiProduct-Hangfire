using apiDesafio.Models;
using FluentValidation;
using Newtonsoft.Json;

namespace apiDesafio.ViewModel
{
    public class EditorProductViewModel
    {
       
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public ICollection<int> CategoryIds { get; set; } = new List<int>();
    }

    public class EditorProductViewModelValidator : AbstractValidator<EditorProductViewModel>
    {
        public EditorProductViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo Name não pode estar vazio.")
                .MaximumLength(50).WithMessage("O campo Name deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("O campo Description deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("O campo Price não pode estar vazio.")
                .GreaterThan(0).WithMessage("O campo Price deve ser maior que zero.");

            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("O campo Categories não pode estar vazio.");
        }
    }
}
