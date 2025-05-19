using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Models;

namespace Shopify.Data.Configurations
{
    public class ShopifyUserConfiguration : IEntityTypeConfiguration<ShopifyUser>
    {

        public void Configure(EntityTypeBuilder<ShopifyUser> builder)
        {
            builder.HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId).IsRequired();
        }

    }
}