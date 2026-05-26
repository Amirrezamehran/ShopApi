using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetById
{
    public class GetBannerByIdQueryHandler : IQueryHandler<GetBannerByIdQuery, BannerDto?>
    {
        private readonly ShopContext _shopContext;

        public GetBannerByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<BannerDto?> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {
            var banner = await _shopContext.Banners.FirstOrDefaultAsync(b => b.Id == request.BannerId);

            if (banner == null)
                return null;

            return banner.Map();
        }
    }

}
