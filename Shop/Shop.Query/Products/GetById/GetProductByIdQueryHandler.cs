using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetById
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly ShopContext _shopContext;

        public GetProductByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _shopContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            var model = product.Map();

            if (model == null)
                return null;

            await model.SetCategories(_shopContext);
            return model;
        }
    }
}
