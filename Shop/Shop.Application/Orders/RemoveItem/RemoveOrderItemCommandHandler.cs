using Common.Application;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.RemoveItem
{
    public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            // وجود داره با ایدی اون کاربر Order اول میایم میبینیم
            // بعد میایم اگر وجود داشت آیدی آیتمی که میخوایم حذف بشه رو بهش میدیم و حذفش میکنیم
            var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
            if (currentOrder == null)
                return OperationResult.NotFound();

            currentOrder.RemoveItem(request.ItemId);
            await _orderRepository.Save();
            return OperationResult.Success();
        }
    }
}
