using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class TokenControlViewModel
    {
        public string Token { get; set; }

        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 en fazla 100 karakter arasında olmalıdır.")]
        public string Password { get; set; }

        
    }
}
