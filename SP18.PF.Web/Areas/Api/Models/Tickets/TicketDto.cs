using SP18.PF.Web.Areas.Api.Models.Events;
using SP18.PF.Web.Areas.Api.Models.Users;

namespace SP18.PF.Web.Areas.Api.Models.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }

        public decimal PurchasePrice { get; set; }

        public virtual UserDto User { get; set; }

        public virtual EventDto Event { get; set; }
    }
}