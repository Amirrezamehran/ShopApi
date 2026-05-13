using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList
{
    public class GetRoleListQuery : IQuery<List<RoleDto?>>
    {
    }

    public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto?>>
    {
        private readonly ShopContext _shopContext;

        public GetRoleListQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<RoleDto?>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var role = await _shopContext.Roles.OrderByDescending(r => r.Id).ToListAsync(cancellationToken);

            if (role == null)
                return null;

            return role.Map();
        }
    }
}
