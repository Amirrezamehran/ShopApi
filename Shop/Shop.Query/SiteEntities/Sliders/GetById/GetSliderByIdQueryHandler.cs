using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById
{
    public class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDto?>
    {
        private readonly ShopContext _shopContext;

        public GetSliderByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SliderDto?> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
        {
            var slider = await _shopContext.Sliders.FirstOrDefaultAsync(s => s.Id == request.SliderId);
            if (slider == null)
                return null;
            return slider.Map();
        }
    }
}
