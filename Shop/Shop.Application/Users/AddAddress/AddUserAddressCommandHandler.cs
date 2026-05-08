using Common.Application;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.AddAddress
{
    public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTracking(request.UserId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }

            var address = new UserAddress(request.Province, request.City, request.PostalCode, request.PostalAddress
                                ,request.Name, request.Family, request.PhoneNumber, request.NationalCode);

            user.AddAddress(address);
            await _userRepository.Save();
            return OperationResult.Success();
        }
    }
}
