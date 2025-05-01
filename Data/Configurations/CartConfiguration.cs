using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(cart => cart.User).WithMany(user => user.Carts).HasForeignKey(cart => cart.UserId).IsRequired();
            builder.HasOne(cart => cart.Product).WithMany(p => p.Carts).HasForeignKey(cart => cart.ProductId).IsRequired();
        }

    }
}