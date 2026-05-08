using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAggregate.ValueObjects;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities
{
    internal class ShippingMethodConfiguration : IEntityTypeConfiguration<OrderShippingMethod>
    {
        public void Configure(EntityTypeBuilder<OrderShippingMethod> builder)
        {
            builder.Property(b => b.ShippingType)
            .HasMaxLength(120).IsRequired();
        }
    }
}
