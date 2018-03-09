using System;

namespace SP18.PF.Web.Areas.Api.Models.Events
{
    public class EventSearchDto
    {
        public string SearchTerm { get; set; }
        public int? VenueId { get; set; }
        public int? TourId { get; set; }
        public DateTimeOffset? DateTime { get; set; }
    }
}