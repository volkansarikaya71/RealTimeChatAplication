using System.ComponentModel.DataAnnotations;

namespace deneme3.Models
{
    public class UserSignInViewModel
    {

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon Numaranız 10 Rakamlı Bir Numara Olmalıdır.")]
        public string UserPhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 en fazla 100 karakter arasında olmalıdır.")]
        public string Password { get; set; }

        public string Token { get; set; }

    }
}
