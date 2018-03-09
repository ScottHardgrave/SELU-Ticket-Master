using System.ComponentModel;

namespace SP18.PF.Core.Features.Shared
{
    public class Address
    {
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
    }
}