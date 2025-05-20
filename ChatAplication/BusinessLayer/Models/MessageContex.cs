using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
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
