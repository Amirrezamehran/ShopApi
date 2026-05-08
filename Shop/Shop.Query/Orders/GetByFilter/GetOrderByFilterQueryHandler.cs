using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter
{
    public class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
    {
        private readonly ShopContext _context;

        public GetOrderByFilterQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.Orders.OrderByDescending(ord => ord.Id).AsQueryable();

            if (@params.UserId != null)
                result = result.Where(com => com.UserId == @params.UserId);

            // با کمک این دوتا دستور میتونیم بیایم مشخص کنیم کامنت های بین دو فاصله زمانی دلخواه رو نشونمون بده
            if (@params.StartDate != null)
                result = result.Where(com => com.CreationDate.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(com => com.CreationDate.Date <= @params.EndDate.Value.Date);

            if (@params.Status != null)
                result = result.Where(com => com.Status == @params.Status);

            var skip = (@params.PageId) * @params.Take;
            var model = new OrderFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(order => order.MapFilterData(_context)).ToListAsync(cancellationToken),
                FilterParams = @params

            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }

}
