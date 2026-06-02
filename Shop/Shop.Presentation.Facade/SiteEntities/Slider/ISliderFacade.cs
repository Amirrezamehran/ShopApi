using Common.Application;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Application.SiteEntities.Sliders.Remove;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Slider
{
    public interface ISliderFacade
    {
        // Commands //
        Task<OperationResult> CreateSlider(CreateSliderCommand command);
        Task<OperationResult> EditSlider(EditSliderCommand command);
        Task<OperationResult> RemoveSlider(long sliderId);


        // Queries //
        Task<SliderDto?> GetSliderById(long id);
        Task<List<SliderDto>> GetSliderList();
    }
}
