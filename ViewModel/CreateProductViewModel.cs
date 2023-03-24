using System.ComponentModel.DataAnnotations;

namespace apiDesafio.ViewModel
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
