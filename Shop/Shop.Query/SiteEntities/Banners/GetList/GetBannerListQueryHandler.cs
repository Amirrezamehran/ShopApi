using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList
{
    public class GetBannerListQueryHandler : IQueryHandler<GetBannerListQuery, List<BannerDto>>
    {
        private readonly ShopContext _shopContext;

        public GetBannerListQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<BannerDto>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
        {
            var banner = await _shopContext.Banners.OrderByDescending(b => b.Id).ToListAsync(cancellationToken);
            return banner.Map();
        }
    }
}
