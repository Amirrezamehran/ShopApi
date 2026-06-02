using Common.Application;

namespace Shop.Application.SiteEntities.Banners.Remove
{
    public record RemoveBannerCommand(long BannerId) : IBaseCommand;
}
