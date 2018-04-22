using SP18.PF.Core.Features.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP18.PF.Mobile.Models
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
