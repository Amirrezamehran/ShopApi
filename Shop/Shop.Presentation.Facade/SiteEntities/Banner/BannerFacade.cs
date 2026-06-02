using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Application.SiteEntities.Banners.Remove;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.Banners.GetList;
using Shop.Query.SiteEntities.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.SiteEntities.Banner
{
    internal class BannerFacade : IBannerFacade
    {
        private readonly IMediator _mediator;

        public BannerFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateBanner(CreateBannerCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditBanner(EditBannerCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveBanner(long bannerId)
        {
            return await _mediator.Send(new RemoveBannerCommand(bannerId));
        }

        public async Task<BannerDto?> GetBannerById(long id)
        {
            return await _mediator.Send(new GetBannerByIdQuery(id));
        }

        public async Task<List<BannerDto>> GetBannerList()
        {
            return await _mediator.Send(new GetBannerListQuery());
        }
    }
}
