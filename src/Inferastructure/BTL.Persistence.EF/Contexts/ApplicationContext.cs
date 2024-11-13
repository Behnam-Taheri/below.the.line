using BTL.Domain.Discounts;
using BTL.Persistence.EF.Discounts.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BTL.Persistence.EF.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Discount> Vendors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountEntityTypeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
