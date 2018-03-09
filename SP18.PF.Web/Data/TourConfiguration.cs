using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP18.PF.Core.Features.Tours;

namespace SP18.PF.Web.Data
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(64);
        }
    }
}