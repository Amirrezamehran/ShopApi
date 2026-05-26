using Common.Application;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles.DTOs;

namespace Shop.Presentation.Facade.Roles
{
    public interface IRoleFacade
    {
        // Commands //
        Task<OperationResult> CreateRole(CreateRoleCommand command);
        Task<OperationResult> EditRole(EditRoleCommand command);


        // Queries //
        Task<RoleDto?> GetRoleById(long id);
        Task<List<RoleDto>> GetRoleList();
    }
}
