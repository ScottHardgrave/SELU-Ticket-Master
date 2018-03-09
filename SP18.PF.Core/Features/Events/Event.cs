using System;
using SP18.PF.Core.Features.Tours;
using SP18.PF.Core.Features.Venues;

namespace SP18.PF.Core.Features.Events
{
    public class Event
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public int TourId { get; set; }

        public decimal TicketPrice { get; set; }

        public DateTimeOffset EventStart { get; set; }
        public DateTimeOffset EventEnd { get; set; }

        public virtual Venue Venue { get; set; }
        public virtual Tour Tour { get; set; }
    }
}