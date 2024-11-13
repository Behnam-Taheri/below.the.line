using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Query.DataModel.Models;
using Query.Retrieval.Discounts.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Query.Retrieval.Discounts
{
    public interface IDiscountReadRepository
    {
        Task<List<DiscountListQueryResponse>> GetAsync(int skip, int take, CancellationToken cancellationToken);

    }

    public class DiscountReadRepository : IDiscountReadRepository
    {
        private readonly BTLDBScaffoldContext _context;

        public DiscountReadRepository(IConfiguration configuration)
        {
            _context = new BTLDBScaffoldContext(configuration.GetConnectionString("ReadConnectionString"));
        }

        public async Task<List<DiscountListQueryResponse>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _context.Discounts.Skip(skip).Take(take).Select(x => new DiscountListQueryResponse
            {
                Brand = x.Brand,
                ProductBarcode = x.Barcode,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ImagePath = x.ImagePath,
                OriginalPrice = x.OriginalPrice,
                DiscountedPrice = x.DiscountedPrice,
                ProductName = x.ProductName,
            }).ToListAsync();
        }
    }
}
