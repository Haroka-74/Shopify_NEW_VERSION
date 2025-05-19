using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(c => c.User).WithOne(u => u.Cart).HasForeignKey<Cart>(c => c.UserId).IsRequired();
            builder.HasMany(c => c.CartItems).WithOne(u => u.Cart).HasForeignKey(ci => ci.CartId).IsRequired();
        }

    }
}