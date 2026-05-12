using Dapper;
using Shop.Domain.OrderAggregate;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders
{
    public static class OrderMapper
    {
        public static OrderDto Map(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullname = "",
                Status = order.Status,
                Items = new(),
                Discount = order.Discount,
                Address = order.Address,
                ShippingMethod = order.ShippingMethod,
                LastUpdate = order.LastUpdate,
                CreationDate = order.CreationDate,
            };
        }

        public static async Task<List<OrderItemDto>> GetOrderItems(this OrderDto orderDto, DapperContext dapperContext)
        {
            using var connection = dapperContext.CreateConnection();

            // داشته باشیم OrderItemDto چیزایی که میگیم سلکت کن بهمون برگردون باید دقیقا پراپرتی هاشو توی
            // بشه OrderItemDto که بتونه بریزه داخلش و تبدیل به
            var sql = @$"SELECT s.ShopName, o.OrderId, o.InventoryId, o.Count, o.Price,
                        p.Title as ProductTitle , p.Slug as ProductSlug ,
                          p.ImageName as ProductImageName
                        FROM {dapperContext.OrderItems} o
                        Inner Join {dapperContext.Inventories} i on o.InventoryId=i.Id
                        Inner Join {dapperContext.Products} p on i.ProductId=p.Id
                        Inner Join {dapperContext.Sellers} s on i.SellerId=s.Id
                        where o.OrderId=@orderId";

            
            var result = await connection.QueryAsync<OrderItemDto>(sql, new { orderId = orderDto.Id });
                        
            return result.ToList();
        }

        public static OrderFilterData MapFilterData(this Order order, ShopContext context)
        {
            string userFullname = context.Users.Where(us => us.Id == order.UserId)
                    .Select(us => $"{us.Name} {us.Family}").First();

            return new OrderFilterData()
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullname = userFullname,
                Status = order.Status,
                Province = order.Address?.Province,
                City = order.Address?.City,
                ShippingType = order.ShippingMethod?.ShippingType,
                TotalItemCount = order.ItemCount,
                TotalPrice = order.TotalPrice,
                CreationDate = order.CreationDate
            };
        }


    }
}
