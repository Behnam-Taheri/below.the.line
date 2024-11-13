namespace BTL.Domain.Discounts.Contracts
{
    public interface IDiscountRepository
    {
        Task CreateAsync(Discount discount, CancellationToken cancellationToken);
        Discount? GetById(long id);
        Task<Discount?> GetByIdAsync(long id, CancellationToken cancellationToken);
    }
}
