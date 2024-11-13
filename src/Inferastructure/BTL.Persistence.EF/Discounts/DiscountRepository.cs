using BTL.Domain.Discounts;
using BTL.Domain.Discounts.Contracts;
using BTL.Persistence.EF.Contexts;
using Framework.Persistence;
using System.Linq.Expressions;

namespace BTL.Persistence.EF.Discounts
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(ApplicationContext context) : base(context)
        {
        }

        public Discount? GetById(long id) => Get(x => x.Id == id);

        public async Task<Discount?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
          return await GetAsync(x => x.Id == id, cancellationToken);
        }

        protected override IEnumerable<Expression<Func<Discount, object>>>? GetIncludeExpressions()
        {
            yield return null;
        }
    }
}
