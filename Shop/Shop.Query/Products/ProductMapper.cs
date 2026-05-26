using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAggregate;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products
{
    public static class ProductMapper
    {
        public static ProductDto? Map(this Product? product)
        {
            if (product == null)
                return null;

            return new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                ImageName = product.ImageName,
                Description = product.Description,
                Slug = product.Slug,
                SeoData = product.SeoData,
                Images = product.Images.Select(p => new ProductImageDto()
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ImageName = p.ImageName,
                    Sequence = p.Sequence,
                    CreationDate = p.CreationDate

                }).ToList(),
                Specifications = product.Specifications.Select(p => new ProductSpecificationDto()
                {
                    Key = p.Key,
                    Value = p.Value

                }).ToList(),

                // اینجا آیدی دسته بندیارو دادیم که پایینتر بتونیم کوئری بزنیم و دریافت کنیم مقادیری که میخوایم
                // دلیلش اینه که ما فقط آیدی کتگوری رو داخل محصولات داریم باقی
                // مقادیرشو نداریم و برای بدست آوردنش باید کوئری بزنیم
                Category = new ProductCategoryDto() { Id = product.CategoryId },
                SubCategory = new ProductCategoryDto() { Id = product.SubCategoryId },
                // این زیر یک شرط گذاشتیم که اگر مخالف نال بود بیا آیدیو بده اگر نبود بیا نال رو بده بهش
                SecondarySubCategory = product.SecondarySubCategoryId != null ?
                                    new ProductCategoryDto() 
                                    {
                                        Id = (long)product.SecondarySubCategoryId
                                    } : null
            };
        }


        public static ProductFilterData MapListData(this Product product)
        {
            return new ProductFilterData()
            {
                Id = product.Id,
                Title = product.Title,
                ImageName = product.ImageName,
                Slug = product.Slug,
                CreationDate = product.CreationDate
            };
        }


        // این بخش برای اینه که مثلا میخوایم ببینیم یک محصول کتگوریش یا همون دسته بندیش چیه به این صورت پیش میریم
        public static async Task SetCategories(this ProductDto productDto, ShopContext Context)
        {
            // اومدیم آیدی های کتگوری و زیرمجموعه هاشو دادیم و اینجاهم براساس اون آیدی ها عمل میکنن Map انتهای تابع
            var categories = await Context.Categories.Where(c => c.Id == productDto.Category.Id || c.Id == productDto.SubCategory.Id)
                    .Select(p => new ProductCategoryDto()
                    {
                        Id = p.Id,
                        ParentId = p.ParentId,
                        Title = p.Title,
                        Slug= p.Slug,
                        SeoData = p.SeoData

                    }).ToListAsync();

            
            // اینو با بالایی یکی کردیم اینجوری خیلی سرعت میره بالاتر و کد کمتریم مینویسیم
            //var subCategory = await Context.Categories.Where(c => c.Id == productDto.SubCategory.Id)
            //        .Select(p => new ProductCategoryDto()
            //        {
            //            Id = p.Id,
            //            ParentId = p.ParentId,
            //            Title = p.Title,
            //            Slug= p.Slug,
            //            SeoData = p.SeoData

            //        }).FirstOrDefaultAsync();


            if (productDto.SecondarySubCategory != null)
            {
                var secondarySubCategory = await Context.Categories.Where(c => c.Id == productDto.SecondarySubCategory.Id)
                    .Select(p => new ProductCategoryDto()
                    {
                        Id = p.Id,
                        ParentId = p.ParentId,
                        Title = p.Title,
                        Slug = p.Slug,
                        SeoData = p.SeoData

                    }).FirstOrDefaultAsync();

                if (secondarySubCategory != null)
                    productDto.SecondarySubCategory = secondarySubCategory;
            }


            if (categories != null)
                productDto.Category = categories.First(c => c.Id == productDto.Category.Id);

            if (categories != null)
                productDto.SubCategory = categories.First(c => c.Id == productDto.SubCategory.Id);

        }


    }

}
