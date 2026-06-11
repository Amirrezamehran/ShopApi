using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Domain.RoleAggregate.Enums;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        [HttpGet("{orderId}")]
        public async Task<ApiResult<OrderDto?>> GetOrderById(long orderId)
        {
            var order = await _orderFacade.GetOrderById(orderId);
            return QueryResult(order);
        }

        [PermissionChecker(Permission.Order_Management)]
        [HttpGet]
        public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery]OrderFilterParams filterParams)
        {
            var order = await _orderFacade.GetOrderByFilter(filterParams);
            return QueryResult(order);
        }

        [HttpPost]
        public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
        {
            var order = await _orderFacade.AddItemOrder(command);
            return CommandResult(order);
        }

        [HttpPost("Checkout")]
        public async Task<ApiResult> CheckoutOrder(CheckoutOrderCommand command)
        {
            var order = await _orderFacade.CheckoutOrder(command);
            return CommandResult(order);
        }

        [HttpPut("orderItem/IncreaseCount")]
        public async Task<ApiResult> IncreaseOrder(IncreaseOrderItemCountCommand command)
        {
            var order = await _orderFacade.IncreaseOrder(command);
            return CommandResult(order);
        }

        [HttpPut("orderItem/DecreaseCount")]
        public async Task<ApiResult> DecreaseOrder(DecreaseOrderItemCountCommand command)
        {
            var order = await _orderFacade.DecreaseOrder(command);
            return CommandResult(order);
        }

        [HttpDelete("orderItem")]
        public async Task<ApiResult> RemoveOrder(RemoveOrderItemCommand command)
        {
            var order = await _orderFacade.RemoveOrder(command);
            return CommandResult(order);
        }

    }
}
