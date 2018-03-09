using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP18.PF.Core.Features.Venues;
using SP18.PF.Web.Extensions;

namespace SP18.PF.Web.Data
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.OwnsAddress(x => x.PhysicalAddress);
            builder.Property(x => x.Name).HasMaxLength(64);
        }
    }
}