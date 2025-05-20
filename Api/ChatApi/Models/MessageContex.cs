using ChatApi.EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    public class MessageContex
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public string MessageContext { get; set; }

        public bool MessageReading { get; set; }

        public DateTime MessageTime { get; set; }

        public string MessageType { get; set; }
        

    }
}
