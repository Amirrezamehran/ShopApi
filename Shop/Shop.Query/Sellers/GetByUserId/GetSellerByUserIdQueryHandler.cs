using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByUserId
{
    public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto?>
    {
        private readonly ShopContext _shopContext;

        public GetSellerByUserIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerDto?> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _shopContext.Sellers.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            return user.Map();
        }
    }



}
