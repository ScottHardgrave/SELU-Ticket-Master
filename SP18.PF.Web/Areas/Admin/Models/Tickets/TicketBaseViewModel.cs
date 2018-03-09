using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SP18.PF.Web.Areas.Admin.Models.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public SelectList EventSelectList { get; set; }

        public SelectList UserSelectList { get; set; }

        [DisplayName("Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [DisplayName("Venue")]
        public string VenueName { get; set; }

        [DisplayName("Tour")]
        public string TourName { get; set; }

        [DisplayName("User")]
        public string UserName { get; set; }

        [DisplayName("User")]
        public int UserId { get; set; }

        [DisplayName("Event")]
        public int EventId { get; set; }
    }
}