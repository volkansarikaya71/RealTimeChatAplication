using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class UserFriendAddViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserFriendId { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        //[Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon Numaranız 10 Rakamlı Bir Numara Olmalıdır.")]
        public string FriendPhoneNumber { get; set; }

        public bool DeleteStatus { get; set; }
    }
}
