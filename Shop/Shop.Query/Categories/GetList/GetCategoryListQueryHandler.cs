using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetList
{
    public class GetCategoryListQueryHandler : IQueryHandler<GetCategoryListQuery, List<CategoryDto>>
    {
        private readonly ShopContext _context;

        public GetCategoryListQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            // هاشم بهمون بده Childs اینجا میگیم وقتی میخوای بدی بهمون بیا دو سطر فرزندانشو یا همون
            var result = await _context.Categories
                .Where(c => c.ParentId == null)
                .Include(c => c.Childs)
                .ThenInclude(c => c.Childs).OrderByDescending(cat => cat.Id).ToListAsync(cancellationToken);
            return result.Map();
        }
    }
}
