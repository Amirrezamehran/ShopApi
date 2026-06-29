using Common.Application;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.SetActiveAddress;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses
{
    public interface IUserAddressFacade
    {
        // Commands //
        Task<OperationResult> AddUserAddress(AddUserAddressCommand command);
        Task<OperationResult> EditUserAddress(EditUserAddressCommand command);
        Task<OperationResult> DeleteUserAddress(DeleteUserAddressCommand command);
        Task<OperationResult> SetActiveAddress(SetActiveUserAddressCommand command);


        // Queries //
        Task<AddressDto?> GetUserAddressById(long userAddressId);
        Task<List<AddressDto>> GetUserAddressList(long userId);

    }
}
