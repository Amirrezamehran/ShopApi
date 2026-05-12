using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories
{
    public static class CategoryMapper
    {
        public static CategoryDto Map(this Category? category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Title = category.Title,
                Slug = category.Slug,
                SeoData = category.SeoData,
                CreationDate = category.CreationDate,
                // تبدیل کنه رو ChildCategoryDto اینجور که فهمیدم میاد فرزندانی که میتونه به
                // Childs میگیره تبدیل میکنه میریزه داخل این
                Childs = category.Childs.MapChildren()
            };

        }

        public static List<CategoryDto> Map(this List<Category> categories)
        {
            List<CategoryDto> model = new List<CategoryDto>();

            categories.ForEach(cat =>
            {
                model.Add(new CategoryDto()
                {
                    Id = cat.Id,
                    Title = cat.Title,
                    Slug = cat.Slug,
                    SeoData = cat.SeoData,
                    CreationDate = cat.CreationDate,
                    Childs = cat.Childs.MapChildren()
                });
            });

            return model;
        }

        public static List<ChildCategoryDto> MapChildren(this List<Category> children)
        {
            List<ChildCategoryDto> model = new List<ChildCategoryDto>();

            //foreach (var child in Children)
            //{
            //    model.Add(new ChildCategoryDto()
            //    {
            // اینم کار همین تابع پایینی رو میکنه
            //    });
            //}

            children.ForEach(cat =>
            {
                model.Add(new ChildCategoryDto()
                {
                    Id = cat.Id,
                    Title = cat.Title,
                    Slug = cat.Slug,
                    SeoData = cat.SeoData,
                    ParentId = (long)cat.ParentId,
                    CreationDate = cat.CreationDate,
                    Childs = cat.Childs.MapSecondaryChild()
                });
            });

            return model;
        }

        public static List<SecondaryChildCategoryDto> MapSecondaryChild(this List<Category> secondaryChildren)
        {
            List<SecondaryChildCategoryDto> model = new List<SecondaryChildCategoryDto>();

            secondaryChildren.ForEach(cat =>
            {
                model.Add(new SecondaryChildCategoryDto()
                {
                    Id = cat.Id,
                    Title = cat.Title,
                    Slug = cat.Slug,
                    SeoData = cat.SeoData,
                    ParentId = (long)cat.ParentId,
                    CreationDate = cat.CreationDate,
                });
            });

            return model;
        }




    }

} // End Class
