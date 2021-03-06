﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class PriceEntityTypeConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Prices").HasKey(price => price.Id);
            builder.HasOne(price => price.Pizza)
                .WithMany(pizza => pizza.Prices)
                .HasForeignKey(price => price.PizzaId)
                .IsRequired();
            builder.HasMany(price => price.OrderPrices)
                .WithOne(orderPrice => orderPrice.Price)
                .HasForeignKey(orderPrice => orderPrice.PriceId)
                .IsRequired();
        }
    }
}
