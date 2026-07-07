using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure.Persistent.Ef._Utilities;


namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories
{
    internal class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {
        public SliderRepository(ShopContext context) : base(context)
        {
        }

        public void Remove(Slider slider)
        {
            Context.Sliders.Remove(slider);
        }
    }
}
