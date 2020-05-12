using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class OrderPriceEntityTypeConfiguration : IEntityTypeConfiguration<OrderPrice>
    {
        public void Configure(EntityTypeBuilder<OrderPrice> builder)
        {
            builder.ToTable("OrderHasPrice").HasKey(orderPrice => orderPrice.Id);
            builder.HasOne(orderPrice => orderPrice.Price)
                .WithMany(price => price.OrderPrices)
                .HasForeignKey(orderPrice => orderPrice.PriceId)
                .IsRequired();
            builder.HasOne(orderPrice => orderPrice.Order)
                .WithMany(order => order.OrderPrices)
                .HasForeignKey(orderPrice => orderPrice.OrderId)
                .IsRequired();
        }
    }
}
