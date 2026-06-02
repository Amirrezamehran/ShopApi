using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Remove
{
    public class RemoveBannerCommandHandler : IBaseCommandHandler<RemoveBannerCommand>
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IFileService _fileService;

        public RemoveBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
        {
            _bannerRepository = bannerRepository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _bannerRepository.GetTracking(request.BannerId);
            if (banner == null)
                return OperationResult.NotFound();

            _bannerRepository.Remove(banner);
            await _bannerRepository.Save();
            _fileService.DeleteFile(Directories.BannerImages, banner.ImageName);
            return OperationResult.Success();
        }
    }
}
