using System;
using System.ComponentModel;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public abstract class EventBaseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Venue")]
        public int VenueId { get; set; }

        [DisplayName("Tour")]
        public int TourId { get; set; }

        [DisplayName("Event Start")]
        public DateTimeOffset? EventStart { get; set; }

        [DisplayName("Event End")]
        public DateTimeOffset? EventEnd { get; set; }

        [DisplayName("Ticket Price")]
        public decimal TicketPrice { get; set; }

        [DisplayName("Venue")]
        public string VenueName { get; set; }

        [DisplayName("Tour")]
        public string TourName { get; set; }
    }
}