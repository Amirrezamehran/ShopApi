using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;

namespace Shop.Presentation.Facade.Orders
{
    internal class OrderFacade : IOrderFacade
    {
        private readonly IMediator _mediator;

        public OrderFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddItemOrder(AddOrderItemCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CheckoutOrder(CheckoutOrderCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DecreaseOrder(DecreaseOrderItemCountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> IncreaseOrder(IncreaseOrderItemCountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveOrder(RemoveOrderItemCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams)
        {
            return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
        }

        public async Task<OrderDto?> GetOrderById(long id)
        {
            return await _mediator.Send(new GetOrderByIdQuery(id));
        }

        public async Task<OrderDto?> GetCurrentOrder(long userId)
        {
            return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
        }
    }
}
