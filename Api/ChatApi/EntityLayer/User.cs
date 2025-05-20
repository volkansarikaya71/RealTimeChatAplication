using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.EntityLayer
{
    public class User
    {

        public int UserId { get; set; }


        [StringLength(80, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3 ile 80 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Kullanıcı adı girmeniz Gereklidir.")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 en fazla 100 karakter arasında olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(255, MinimumLength = 12, ErrorMessage = "e-posta Adresiniz en az 12 en fazla 255 karakter arasında olmalıdır.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$", ErrorMessage = "Sadece @gmail.com veya @hotmail.com uzantılı bir e-posta adresi kullanabilirsiniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        //[Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon Numaranız 10 Rakamlı Bir Numara Olmalıdır.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Resim Eklemeniz gereklidir.")]
        [StringLength(400, ErrorMessage = "Resminiz İsmi 400 karakteri geçmemelidir.")]
        [RegularExpression(@"^(http|https):\/\/[^\s]+\.(jpg|jpeg|PNG|png|gif|bmp|webp)$|^(ftp):\/\/[^\s]+\.(jpg|jpeg|PNG|png|gif|bmp|webp)$|^[A-Za-z]:\\[^\s]+\.(jpg|jpeg|PNG|png|gif|bmp|webp)$", ErrorMessage = "Geçerli bir resim URL'si ekleyin (örn: .jpg, .png) veya geçerli bir dosya yolu (örn: C:\\Users\\Volkan\\Desktop\\hatya.png).")]
        public string UserImage { get; set; }

        public string? Token { get; set; }

        public bool UserStatus { get; set; }

        public DateTime UserLastOnlineDate { get; set; }

        public virtual ICollection<UserFriendList>? UserFriend { get; set; }

        public virtual ICollection<Message>? Receiver { get; set; }

    }
}
