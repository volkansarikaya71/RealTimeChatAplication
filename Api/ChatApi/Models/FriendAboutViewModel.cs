using ChatApi.EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class FriendAboutViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public bool UserStatus { get; set; }

        public DateTime? UserLastOnlineDate { get; set; }
        
        public string UserPhoneNumber { get; set; }


    }
}
