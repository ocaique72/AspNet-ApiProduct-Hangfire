using System.ComponentModel.DataAnnotations;

namespace apiDesafio.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informar o invalido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informar a senha")]
        public string Password { get; set; }

    }
}
