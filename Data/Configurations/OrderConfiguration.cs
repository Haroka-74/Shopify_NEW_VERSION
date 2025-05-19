using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order).HasForeignKey(od => od.OrderId).IsRequired();
        }

    }
}