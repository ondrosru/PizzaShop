using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(order => order.Id);
            builder.HasMany(order => order.OrderPrices)
                .WithOne(orderPrice => orderPrice.Order)
                .HasForeignKey(orderPrice => orderPrice.OrderId)
                .IsRequired();
            builder.HasOne(order => order.Account)
                .WithMany(account => account.Orders)
                .HasForeignKey(order => order.AccountId);
        }
    }
}
