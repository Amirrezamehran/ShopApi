using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetList
{
    public class GetSliderListQueryHandler : IQueryHandler<GetSliderListQuery, List<SliderDto>>
    {
        private readonly ShopContext _shopContext;

        public GetSliderListQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<SliderDto>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
        {
            var slider = await _shopContext.Sliders.OrderByDescending(s => s.Id).ToListAsync(cancellationToken);
            return slider.Map();
        }
    }
}
