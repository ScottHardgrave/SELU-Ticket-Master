using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Extensions;

namespace SP18.PF.Web.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(64);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.OwnsAddress(x => x.BillingAddress);
        }
    }
}