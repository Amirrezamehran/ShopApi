using Common.Application;
using Shop.Domain.RoleAggregate.Enums;


namespace Shop.Application.Roles.Edit
{
    public record EditRoleCommand(long Id, string Title, List<Permission> Permissions) : IBaseCommand;
}
