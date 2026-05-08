using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CommentAggregate.Repository;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.RoleAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.UserAggregate.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Infrastructure.Persistent.Ef.CategoryAggregate;
using Shop.Infrastructure.Persistent.Ef.CommentAggregate;
using Shop.Infrastructure.Persistent.Ef.OrderAggregate;
using Shop.Infrastructure.Persistent.Ef.ProductAggregate;
using Shop.Infrastructure.Persistent.Ef.RoleAggregate;
using Shop.Infrastructure.Persistent.Ef.SellerAggregate;
using Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;
using Shop.Infrastructure.Persistent.Ef.UserAggregate;


namespace Shop.Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static void Init(this IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IRoleRepository, RoleRepository>();
            service.AddTransient<ISellerRepository, SellerRepository>();
            service.AddTransient<IBannerRepository, BannerRepository>();
            service.AddTransient<ISliderRepository, SliderRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();

            service.AddTransient(_ => new DapperContext(connectionString));
            service.AddDbContext<ShopContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            
        }
    }

} // End Class
