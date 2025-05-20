using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class FriendDeleteViewModel
    {

        public int UserId { get; set; }

        public int UserFriendId { get; set; }

    }
}
