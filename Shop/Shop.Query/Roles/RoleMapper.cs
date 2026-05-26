using Shop.Domain.RoleAggregate;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles
{
    public static class RoleMapper
    {
        public static RoleDto Map(this Role role)
        {
            return new RoleDto()
            {
                Id = role.Id,
                Title = role.Title,
                // بخوایم مپ کنیم اینجوری باید بنویسیم Enum وقتی یک لیست از
                Permissions = role.Permissions.Select(r => r.Permissions).ToList(),
                CreationDate = role.CreationDate
            };
        }

        public static List<RoleDto> Map(this List<Role> role)
        {
            List<RoleDto> model = new List<RoleDto>();

            role.ForEach(r =>
            {
                model.Add(new RoleDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Permissions = r.Permissions.Select(r => r.Permissions).ToList(),
                    CreationDate = r.CreationDate
                });
            });

            return model;
        }
    }
}
