using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter
{
    public class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
    {
        private readonly ShopContext _shopContext;

        public GetSellerByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _shopContext.Sellers.OrderByDescending(s => s.Id).AsQueryable();

            // یعنی حاوی این مقدار بود. که اگر نصف کد ملی هم وارد کردیم هرچی بود با اون ارقام بیاره Contains میگیم
            if (!string.IsNullOrWhiteSpace(@params.NationalCode))
                result = result.Where(s => s.NationalCode.Contains(@params.NationalCode));

            if (!string.IsNullOrWhiteSpace(@params.ShopName))
                result = result.Where(s => s.ShopName.Contains(@params.ShopName));

            var skip = (@params.PageId - 1) * @params.Take;
            var sellerResult = new SellerFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(seller => seller.MapFilterData()).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            sellerResult.GeneratePaging(result, @params.Take, @params.PageId);
            return sellerResult;

        }


    }
}
