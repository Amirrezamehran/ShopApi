using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber
{
    public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
    {
        private readonly ShopContext _shopContext;

        public GetUserByPhoneNumberQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var user = await _shopContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (user == null)
                return null;

            return await user.Map().SetUserRoleTitles(_shopContext);
        }
    }
}
