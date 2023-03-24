using System.ComponentModel.DataAnnotations;

namespace apiDesafio.ViewModel
{
    public class CreateProductLogViewModel
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductJson { get; set; }
    }
}
