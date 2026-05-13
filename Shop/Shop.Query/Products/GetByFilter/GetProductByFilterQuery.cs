using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByFilter
{
    public class GetProductByFilterQuery : QueryFilter<ProductFilterResult, ProductFilterParams>
    {
        public GetProductByFilterQuery(ProductFilterParams filterParams) : base(filterParams)
        {
        }
    }


    public class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
    {
        private readonly ShopContext _shopContext;

        public GetProductByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            // که گویا بهینه تره ولی باید راجبش سرچ کنم AsSplitQuery اگر داده هامون زیاد بود بهتر بود میزدیم
            var result = _shopContext.Products.OrderByDescending(p => p.Id).AsQueryable();

            if (@params.Id != null)
                result = result.Where(p => p.Id == @params.Id);

            if (!string.IsNullOrWhiteSpace(@params.Slug))
                result = result.Where(p => p.Slug == @params.Slug);

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(p => p.Title.Contains(@params.Title));

            // این خطو داخل توضیحات پروژه گفتم چیکار میکنه
            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ProductFilterResult()
            {
                Data = result.Skip(skip).Take(@params.Take).Select(p => p.MapListData()).ToList(),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }

    }
}
