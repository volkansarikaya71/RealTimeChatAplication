using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class LogOutViewModel
    {

        public string Token { get; set; }

        public bool UserStatus { get; set; }

        public DateTime? UserLastOnlineDate { get; set; }
    }
}
