using Common.Application;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.Checkout
{
    public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
            if (currentOrder == null)
            {
                return OperationResult.NotFound();
            }

            var Address = new OrderAddress(request.Province, request.City, request.PostalCode, request.PostalAddress
                                    ,request.Name, request.Family, request.PhoneNumber, request.NationalCode);

            currentOrder.Checkout(Address);
            await _orderRepository.Save();
            return OperationResult.Success();
        }
    }
}
