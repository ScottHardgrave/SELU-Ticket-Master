using Microsoft.AspNetCore.Mvc.Rendering;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public class EventCreateViewModel : EventBaseViewModel
    {
        public SelectList VenueSelectList { get; set; }
        public SelectList TourSelectList { get; set; }
    }
}