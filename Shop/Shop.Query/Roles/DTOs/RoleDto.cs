using Common.Query;
using Shop.Domain.RoleAggregate.Enums;

namespace Shop.Query.Roles.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }

}
