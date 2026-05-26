using Shop.Domain.SiteEntities;
using Shop.Query.SiteEntities.DTOs;


namespace Shop.Query.SiteEntities.Sliders
{
    public static class SliderMapper
    {
        public static SliderDto? Map(this Slider? slider)
        {
            if (slider == null)
                return null;

            return new SliderDto()
            {
                Id = slider.Id,
                Title = slider.Title,
                Link = slider.Link,
                ImageName = slider.ImageName,
                CreationDate = slider.CreationDate
            };
        }


        public static List<SliderDto> Map(this List<Slider> slider)
        {
            List<SliderDto> model = new List<SliderDto>();

            model.ForEach(s =>
            {
                model.Add(new SliderDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Link = s.Link,
                    ImageName = s.ImageName,
                    CreationDate = s.CreationDate
                });
            });

            return model;
        }

        

    }
}
