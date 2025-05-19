using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(p => p.OrderDetails).WithOne(od => od.Product).HasForeignKey(od => od.ProductId).IsRequired();
            builder.HasMany(p => p.CartItems).WithOne(ci => ci.Product).HasForeignKey(ci => ci.ProductId).IsRequired();
        }

    }
}