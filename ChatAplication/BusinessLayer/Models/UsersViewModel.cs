using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmLogin.Models
{
    public class UsersViewModel
    {

        public int UserId { get; set; }


        [StringLength(80, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3 ile 80 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Kullanıcı adı girmeniz Gereklidir.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$", ErrorMessage = "Sadece @gmail.com veya @hotmail.com uzantılı bir e-posta adresi kullanabilirsiniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon Numaranız 10 Rakamlı Bir Numara Olmalıdır.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Resim Eklemeniz gereklidir.")]
        [StringLength(400, ErrorMessage = "Resminiz İsmi 400 karakteri geçmemelidir.")]
        [RegularExpression(@"^(http|https):\/\/[^\s]+\.(jpg|jpeg|PNG|png|gif|bmp|webp)$", ErrorMessage = "Geçerli bir resim URL'si ekleyin (örn: .jpg, .png).")]
        public string UserImage { get; set; }

        public string Token { get; set; }

        public bool UserStatus { get; set; }

        public DateTime UserLastOnlineDate { get; set; }
    }
}
