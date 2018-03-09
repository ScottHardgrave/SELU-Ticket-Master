using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP18.PF.Core.Features.Tickets;

namespace SP18.PF.Web.Data
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(x => x.User).WithMany().IsRequired();
            builder.HasOne(x => x.Event).WithMany().IsRequired();
        }
    }
}