using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users
{
    public class UserDomainService : IUserDomainService
    {
        public bool IsEmailExist(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsPhoneNumberExist(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
