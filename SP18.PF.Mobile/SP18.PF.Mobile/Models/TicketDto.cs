using System;
using System.Collections.Generic;
using System.Text;

namespace SP18.PF.Mobile.Models
{

    public class TicketDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public string purchasePrice { get; set; }

        public string description { get; set; }

        public UserLoginDto user { get; set; }

        public EventDto @event { get; set; }
    }

}
