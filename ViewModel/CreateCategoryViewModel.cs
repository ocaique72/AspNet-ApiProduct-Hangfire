using System.ComponentModel.DataAnnotations;

namespace apiDesafio.ViewModel
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
