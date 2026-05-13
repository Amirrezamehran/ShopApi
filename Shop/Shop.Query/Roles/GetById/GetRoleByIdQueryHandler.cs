using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetById
{
    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
    {
        private readonly ShopContext _shopContext;

        public GetRoleByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _shopContext.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

            if (role == null)
                return null;

            return role.Map();
        }
    }

}
