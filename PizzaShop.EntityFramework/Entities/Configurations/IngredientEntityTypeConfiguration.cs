using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredient").HasKey(ingredient => ingredient.Id);
            builder.HasMany(ingredient => ingredient.PizzaIngredients)
                .WithOne(pizzaIngredient => pizzaIngredient.Ingredient)
                .HasForeignKey(pizzaIngredient => pizzaIngredient.IngredientId)
                .IsRequired();
        }
    }
}
