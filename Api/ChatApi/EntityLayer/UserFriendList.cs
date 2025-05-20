namespace ChatApi.EntityLayer
{
    public class UserFriendList
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserFriendId { get; set; }

        public virtual User UserFriend { get; set; }

        public bool DeleteStatus { get; set; }
    }
}
