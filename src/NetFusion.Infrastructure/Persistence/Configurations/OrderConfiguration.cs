using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFusion.Domain.Aggregates.OrderAggregate;
using NetFusion.Domain.ValueObjects;

namespace NetFusion.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => new OrderId(value));

        // Order -> OrderItems
        builder.OwnsMany(o => o.Items, i =>
        {
            i.WithOwner().HasForeignKey("OrderId");
            i.Property<int>("Id"); // shadow PK
            i.HasKey("Id");
            i.OwnsOne(x => x.Price);
        });

        // Optional: persist IsPaid/IsShipped
        builder.Property(o => o.IsPaid);
        builder.Property(o => o.IsShipped);
    }
}
