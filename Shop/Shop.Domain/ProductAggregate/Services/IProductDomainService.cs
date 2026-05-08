
namespace Shop.Domain.ProductAggregate.Services
{
    public interface IProductDomainService
    {
        bool SlugIsExists(string slug);
    }


}
