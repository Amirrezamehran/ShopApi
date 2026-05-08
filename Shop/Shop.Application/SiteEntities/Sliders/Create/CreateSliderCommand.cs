using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Sliders.Create
{
    public class CreateSliderCommand : IBaseCommand
    {
        public string Link { get; private set; }
        public IFormFile ImageFile { get; private set; }
        public string Title { get; private set; }

        public CreateSliderCommand(string link, IFormFile imageFile, string title)
        {
            Link = link;
            ImageFile = imageFile;
            Title = title;
        }
    }
}
