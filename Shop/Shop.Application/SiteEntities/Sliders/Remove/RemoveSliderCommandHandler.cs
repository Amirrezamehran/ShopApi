
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Remove
{
    public class RemoveSliderCommandHandler : IBaseCommandHandler<RemoveSliderCommand>
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IFileService _fileService;

        public RemoveSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(RemoveSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepository.GetTracking(request.SliderId);
            if (slider == null)
                return OperationResult.NotFound();

            _sliderRepository.Remove(slider);
            await _sliderRepository.Save();
            _fileService.DeleteFile(Directories.SliderImages, slider.ImageName);
            return OperationResult.Success();
        }
    }
}
