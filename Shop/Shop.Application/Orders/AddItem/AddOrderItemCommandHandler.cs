using Common.Application;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Orders.AddItem
{
    public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISellerRepository _sellerRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
        {
            _orderRepository = orderRepository;
            _sellerRepository = sellerRepository;
        }

        // دقیقا چیکار میکنه request قسمت 63 دقیقه 10 به بعد توضیحش باعث میشه بفهمم این
        public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _sellerRepository.GetInventoryById(request.InventoryId);
            if (inventory == null)
            {
                return OperationResult.NotFound();
            }

            // اینجا گفتیم اگر تعداد موجودی کمتر از حد درخواستی کاربری که ریکوییست داده بود این خطا رو نشونش بده
            if (inventory.Count < request.Count)
            {
                return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");
            }

            var order = await _orderRepository.GetCurrentUserOrder(request.UserId);
            if (order == null)
            {
                order = new Order(request.UserId);
            }

            order.AddItem(new OrderItem(request.InventoryId, request.Count, inventory.Price));
            if(ItemCountBiggerThanInventoryCount(inventory, order))
            {
                return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");
            }

            await _orderRepository.Save();
            return OperationResult.Success();
        }

        private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
        {
            var orderItem = order.Items.First(i => i.InventoryId == inventory.Id);
            if (orderItem.Count > inventory.Count)
            {
                return true;
            }

            return false;
        }
        
    }
}
