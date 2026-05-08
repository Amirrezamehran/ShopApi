


using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Edit
{
    public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDomainService _userDomainService;
        private readonly IFileService _fileService;

        public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService, IFileService fileService)
        {
            _userRepository = userRepository;
            _userDomainService = userDomainService;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTracking(request.UserId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }

            var oldAvatar = user.AvatarName;
            user.EditUser(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _userDomainService);

            if (request.Avatar != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
                user.SetAvatar(imageName);
            }

            await _userRepository.Save();
            DeleteOldAvatar(request.Avatar, oldAvatar);
            return OperationResult.Success();
        }

        private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
        {
            if (avatarFile == null || oldImage == "avatar.png")
            {
                return;
            }

            _fileService.DeleteFile(Directories.UserAvatars, oldImage);
        }
    }
}
