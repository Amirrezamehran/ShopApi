using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Products.Create;
using Shop.Application.Roles.Create;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.SellerAggregate.Services;
using Shop.Domain.UserAggregate.Services;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query.Categories.GetById;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
            // اینجا کلا کاری با کلاسی که دادیم نداره فقط کافیه کلاس تو اون اسمبلی مورد نظرمون باشه
            // میاد خودش میگرده و چیزایی که بهش مربوط میشه رو خودش پیدا میکنه و کاراشو میکنه
            // است IRequestHandler و IRequest چیزایی که بهش مربوطه شامل
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Directories).Assembly);
            });

            // باید اینجوری اینجکت کنیم سرویس هاشو MediatR14 تو ورژن
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetCategoryByIdQuery).Assembly);
            });
            //services.AddMediatR(typeof(Directories).Assembly);
            //services.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);

            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<ISellerDomainService, SellerDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();

            // اینجاهم مثل توضیحات بالاس
            services.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);

            // این خط حتما باید آخر نوشته بشه چون اول باید تمام مقادیر این صفحه رو اینجکت کنه
            // و سرویس رو بده تا مقادیر داخل InfrastructureBootstrapper.Init بعد بره داخل
            // کنه Migration اونجاهم اینجکت بشه و بعد بتونه دیتابیس رو
            InfrastructureBootstrapper.Init(services, connectionString);
            services.InitFacadeDependency();

        }

    } // End Class

}





