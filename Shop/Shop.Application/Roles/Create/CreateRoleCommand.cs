using Common.Application;
using Shop.Domain.RoleAggregate.Enums;


namespace Shop.Application.Roles.Create
{
    public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;
}
