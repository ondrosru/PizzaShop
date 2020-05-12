using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class PizzaIngredientsEntityTypeConfiguration : IEntityTypeConfiguration<PizzaIngredient>
    {
        public void Configure(EntityTypeBuilder<PizzaIngredient> builder)
        {
            builder.ToTable("PizzaHasIngredient").HasKey(pizzaIngredient => pizzaIngredient.Id);
            builder.HasOne(pizzaIngredient => pizzaIngredient.Ingredient)
                .WithMany(ingredient => ingredient.PizzaIngredients)
                .HasForeignKey(pizzaIngredient => pizzaIngredient.IngredientId)
                .IsRequired();
            builder.HasOne(pizzaIngredient => pizzaIngredient.Pizza)
                .WithMany(pizza => pizza.PizzaIngredients)
                .HasForeignKey(pizzaIngredient => pizzaIngredient.PizzaId)
                .IsRequired();
        }
    }
}
