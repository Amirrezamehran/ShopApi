using Common.Query;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetBySlug
{
    public class GetProductBySlugQuery : IQuery<ProductDto?>
    {
        public string Slug { get; private set; }

        public GetProductBySlugQuery(string slug)
        {
            Slug = slug;
        }

    }
}
