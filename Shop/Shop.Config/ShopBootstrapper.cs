using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Roles.Create;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.SellerAggregate.Services;
using Shop.Domain.UserAggregate.Services;
using Shop.Infrastructure;
using Shop.Query.Categories.GetById;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            // اینجا کلا کاری با کلاسی که دادیم نداره فقط کافیه کلاس تو اون اسمبلی مورد نظرمون باشه
            // میاد خودش میگرده و چیزایی که بهش مربوط میشه رو خودش پیدا میکنه و کاراشو میکنه
            // است IRequestHandler و IRequest چیزایی که بهش مربوطه شامل
            services.AddMediatR(typeof(Directories).Assembly);
            services.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);

            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<ISellerDomainService, SellerDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();

            // اینجاهم مثل توضیحات بالاس
            services.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);
        }

    } // End Class

} 
