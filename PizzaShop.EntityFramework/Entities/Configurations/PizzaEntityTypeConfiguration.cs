using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class PizzaEntityTypeConfiguration : IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder.ToTable("Pizzas").HasKey(pizza => pizza.Id);
            builder.HasMany(pizza => pizza.PizzaIngredients)
                .WithOne(pizzaIngredient => pizzaIngredient.Pizza)
                .HasForeignKey(pizzaIngredient => pizzaIngredient.PizzaId)
                .IsRequired();
            builder.HasMany(pizza => pizza.Prices)
                .WithOne(price => price.Pizzas)
                .HasForeignKey(price => price.PizzaId)
                .IsRequired();
        }
    }
}
