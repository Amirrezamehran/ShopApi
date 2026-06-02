
using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.SiteEntities.Sliders.Remove
{
    public record RemoveSliderCommand(long SliderId) : IBaseCommand;
}
