using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class ChatMessageCountAndAbout
    {
        public int SenderId { get; set; }

        public string MessageContext { get; set; }

        public DateTime MessageTime { get; set; }

        public string SenderName { get; set; }

        public string SenderImage { get; set; }

        public string MessageType { get; set; }

        public string PhoneNumber { get; set; }

        public int MessageCount { get; set; }
    }
}
