using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetById
{
    public class GetSellerByIdQueryHandler : IQueryHandler<GetSellerByIdQuery, SellerDto?>
    {
        private readonly ShopContext _shopContext;

        public GetSellerByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerDto?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var seller = await _shopContext.Sellers.FirstOrDefaultAsync(s => s.Id == request.Id);
            return seller.Map();
        }
    }

}
