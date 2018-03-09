using System;

namespace SP18.PF.Web.Areas.Api.Models.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public int TourId { get; set; }

        public decimal TicketPrice { get; set; }

        public DateTimeOffset EventStart { get; set; }
        public DateTimeOffset EventEnd { get; set; }

        public string VenueName { get; set; }
        public string TourName { get; set; }
    }
}