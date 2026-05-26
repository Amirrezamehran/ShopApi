using Shop.Domain.SiteEntities;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners
{
    public static class BannerMapper
    {
        public static BannerDto? Map(this Banner? banner)
        {
            if (banner == null)
                return null;

            return new BannerDto()
            {
                Id = banner.Id,
                Link = banner.Link,
                ImageName = banner.ImageName,
                Position = banner.Position,
                CreationDate = banner.CreationDate
            };
        }

        public static List<BannerDto> Map(this List<Banner> banner)
        {
            List<BannerDto> model = new List<BannerDto>();

            banner.ForEach(b =>
            {
                model.Add(new BannerDto
                {
                    Id = b.Id,
                    Link = b.Link,
                    ImageName = b.ImageName,
                    Position = b.Position,
                    CreationDate = b.CreationDate
                });
            });

            return model;
        }

    }
}
