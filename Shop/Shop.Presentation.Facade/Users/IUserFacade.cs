using Common.Application;
using Shop.Application.Users.ChargeWallet;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users
{
    public interface IUserFacade
    {
        // Commands //
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> RegisterUser(RegisterUserCommand command);
        Task<OperationResult> ChargeWallet(ChargeUserWalletCommand command);


        // Queries //
        Task<UserDto?> GetUserById(long id);
        Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
        Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    }
}
