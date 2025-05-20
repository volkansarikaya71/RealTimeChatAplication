using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class ListUserFriendViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public bool UserStatus { get; set; }

        public DateTime UserLastOnlineDate { get; set; }

        public string Token { get; set; }

        public string UserPhoneNumber {  get; set; }

    }
}
