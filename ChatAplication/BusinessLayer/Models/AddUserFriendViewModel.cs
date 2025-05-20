using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class AddUserFriendViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserFriendId { get; set; }

        public string FriendPhoneNumber { get; set; }

        public bool DeleteStatus { get; set; }

        public string Token { get; set; }
}
}
