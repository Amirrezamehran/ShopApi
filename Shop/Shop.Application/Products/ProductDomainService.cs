using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _repository;

        public ProductDomainService(IProductRepository repository)
        {
            _repository = repository;
        }

        public bool SlugIsExists(string slug)
        {
            return _repository.Exists(s => s.Slug == slug);
        }
    }
}
