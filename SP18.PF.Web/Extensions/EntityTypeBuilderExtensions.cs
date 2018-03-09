using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static ReferenceOwnershipBuilder<T, Address> OwnsAddress<T>(this EntityTypeBuilder<T> entityTypeBuilder, Expression<Func<T,Address>> addressExpression) where T : class
        {
            var owned = entityTypeBuilder.OwnsOne(addressExpression);
            owned.Property(x => x.AddressLine1).HasMaxLength(64);
            owned.Property(x => x.AddressLine2).HasMaxLength(64);
            owned.Property(x => x.City).HasMaxLength(64);
            owned.Property(x => x.ZipCode).HasMaxLength(5);
            owned.Property(x => x.State).HasMaxLength(2);
            return owned;
        }
    }
}