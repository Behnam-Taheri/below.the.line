using BTL.Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTL.Persistence.EF.Discounts.EntityTypeConfigurations
{
    public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable(nameof(Discount));
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
        }
    }
}
