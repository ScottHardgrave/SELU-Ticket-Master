using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Areas.Api.Models.Venues
{
    public class VenueDto
    {
        public int Id { get; set; }

        public Address PhysicalAddress { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }
    }
}