using Common.Application.Validation;
using Common.Domain.Repository;
using MediatR;
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
using Shop.Infrastructure.Persistent.Ef._Utilities;
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
    public class InfrastructureBootstrapper
    {
        public static void Init(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<IBannerRepository, BannerRepository>();
            services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient(_ => new DapperContext(connectionString));
            services.AddDbContext<ShopContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });


        }
    }

} // End Class
