using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Sliders.Create
{
    public class CreateSliderCommand : IBaseCommand
    {
        // مقداردهی کنیم Constructor کنیم و داخل Private Set اینجا اگر اینارو
        // هست خطا میده و دلیلشم داخل فایل توضیحات هر دو پروژه نوشتم Razor Page به مشکل میخوریم و برنامه سمت ویو که همون
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
    }
}
