using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly ShopContext _shopContext;

        public GetUserByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _shopContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                return null;
            
            return await user.Map().SetUserRoleTitles(_shopContext);
        }
    }
}
