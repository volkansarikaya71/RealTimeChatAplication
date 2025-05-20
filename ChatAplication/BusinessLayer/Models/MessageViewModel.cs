using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public int? GroupId { get; set; }

        public string MessageType { get; set; }

        public string MessageContext { get; set; }

        public bool SenderMessageStatus { get; set; }

        public bool ReceiverMessageStatus { get; set; }

        public bool MessageReading { get; set; }

        public DateTime MessageTime { get; set; }

        //



    }
}
