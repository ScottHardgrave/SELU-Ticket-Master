using SP18.PF.Core.Features.Events;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Core.Features.Tickets
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal PurchasePrice { get; set; }

        public virtual User User { get; set; }

        public virtual Event Event { get; set; }

        public int EventId{ get; set; }
        public int UserId { get; set; }
    }
}