using Common.Application;
using MediatR;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;

namespace Shop.Presentation.Facade.Sellers
{
    internal class SellerFacade : ISellerFacade
    {
        private readonly IMediator _mediator;

        public SellerFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateSeller(CreateSellerCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditSeller(EditSellerCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams)
        {
            return await _mediator.Send(new GetSellerByFilterQuery(filterParams));
        }

        public async Task<SellerDto?> GetSellerById(long id)
        {
            return await _mediator.Send(new GetSellerByIdQuery(id));
        }
    }
}
