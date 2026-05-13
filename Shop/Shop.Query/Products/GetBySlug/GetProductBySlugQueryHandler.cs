using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetBySlug
{
    public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto?>
    {
        private readonly ShopContext _shopContext;

        public GetProductBySlugQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var product = await _shopContext.Products.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);
            var model = product.Map();

            if (model == null)
                return null;

            await model.SetCategories(_shopContext);
            return model;
        }
    }
}
