using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Application.SiteEntities.Sliders.Remove;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Sliders.GetById;
using Shop.Query.SiteEntities.Sliders.GetList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.SiteEntities.Slider
{
    internal class SliderFacade : ISliderFacade
    {
        private readonly IMediator _mediator;

        public SliderFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateSlider(CreateSliderCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditSlider(EditSliderCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveSlider(long sliderId)
        {
            return await _mediator.Send(new RemoveSliderCommand(sliderId));
        }

        public async Task<SliderDto?> GetSliderById(long id)
        {
            return await _mediator.Send(new GetSliderByIdQuery(id));
        }

        public async Task<List<SliderDto>> GetSliderList()
        {
            return await _mediator.Send(new GetSliderListQuery());
        }
    }
}
