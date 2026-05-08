using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;
using Shop.Infrastructure.Persistent.Ef._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.RoleAggregate
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ShopContext context) : base(context)
        {
        }
    }
}
