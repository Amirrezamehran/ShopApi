using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly ShopContext _shopContext;
        private readonly DapperContext _dapperContext;
        public GetOrderByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
        {
            _shopContext = shopContext;
            _dapperContext = dapperContext;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _shopContext.Orders.FirstOrDefaultAsync(or => or.Id == request.OrderId, cancellationToken);
            if (order == null)
                return null;

            var orderDto = order.Map();
            orderDto.UserFullname = await _shopContext.Users.Where(us => us.Id == orderDto.UserId)
                        .Select(us => $"{us.Name} {us.Family}").FirstAsync(cancellationToken);

            orderDto.Items = await orderDto.GetOrderItems(_dapperContext);

            return orderDto;
        }

    }

}
