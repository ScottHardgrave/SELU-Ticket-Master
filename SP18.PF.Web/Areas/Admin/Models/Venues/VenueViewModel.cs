using Microsoft.AspNetCore.Mvc.Rendering;
using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Venues
{
    public class VenueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

        public Address PhysicalAddress { get; set; }

        public SelectList StateList => new SelectList(States.List, nameof(State.Abbr), nameof(State.Name));
    }
}
