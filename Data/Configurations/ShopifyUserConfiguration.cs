using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class ShopifyUserConfiguration : IEntityTypeConfiguration<ShopifyUser>
    {

        public void Configure(EntityTypeBuilder<ShopifyUser> builder)
        {
            builder.HasMany(user => user.Orders).WithOne(order => order.User).HasForeignKey(order => order.UserId).IsRequired();
        }

    }
}