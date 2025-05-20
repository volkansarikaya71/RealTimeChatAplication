using System.ComponentModel.DataAnnotations;

namespace ChatApi.EntityLayer
{
    public class Message
    {
        [Key]
        public int MessageId{ get; set; }

        public int SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public int? GroupId { get; set; }

        public string MessageType {  get; set; }

        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Messaj içeriği 1 ile 2000 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Mesaj girmeniz Gereklidir.")]
        public string MessageContext { get; set; }

        public bool SenderMessageStatus { get; set; }

        public bool ReceiverMessageStatus { get; set; }

        public bool MessageReading { get; set; }

        public DateTime MessageTime { get; set; }

        public virtual User? Receiver { get; set; }
    }
}
