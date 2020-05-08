using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaShop.EntityFramework.Entities.Configurations
{
    class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account").HasKey(account => account.Id);
            builder.HasMany(account => account.Orders)
                .WithOne(order => order.Account)
                .HasForeignKey(order => order.AccountId);
        }
    }
}
