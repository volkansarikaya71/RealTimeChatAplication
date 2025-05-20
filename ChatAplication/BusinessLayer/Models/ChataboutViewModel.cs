using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class ChataboutViewModel
    {
        public int ChatId { get; set; }

        public string ChatName{ get; set; }

        public string ChatImage { get; set; }

        public bool ChatStatus { get; set; }

        public DateTime ChatTime { get; set; }

        public string PhoneNumber { get; set; }
    }
}
