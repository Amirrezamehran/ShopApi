using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;

namespace Shop.Presentation.Facade.Orders
{
    public interface IOrderFacade
    {
        // Commands //
        Task<OperationResult> AddItemOrder(AddOrderItemCommand command);
        Task<OperationResult> CheckoutOrder(CheckoutOrderCommand command);
        Task<OperationResult> DecreaseOrder(DecreaseOrderItemCountCommand command);
        Task<OperationResult> IncreaseOrder(IncreaseOrderItemCountCommand command);
        Task<OperationResult> RemoveOrder(RemoveOrderItemCommand command);


        // Queries //
        Task<OrderDto?> GetOrderById(long id);
        Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams);
    }
}
