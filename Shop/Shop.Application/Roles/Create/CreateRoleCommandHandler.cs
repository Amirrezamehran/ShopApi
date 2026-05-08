using Common.Application;
using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;

namespace Shop.Application.Roles.Create
{
    public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var permissions = new List<RolePermission>();

            request.Permissions.ForEach(p =>
            {
                permissions.Add(new RolePermission(p));
            });

            var role = new Role(request.Title, permissions);
            await _roleRepository.AddAsync(role);
            await _roleRepository.Save();
            return OperationResult.Success();

        }
    }
}
