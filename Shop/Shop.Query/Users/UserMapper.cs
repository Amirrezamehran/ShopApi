using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAggregate;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users
{
    public static class UserMapper
    {
        public static UserDto Map(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                AvatarName = user.AvatarName,
                Gender = user.Gender,
                CreationDate = user.CreationDate,
                IsActive = user.IsActive,
                Roles = user.Roles.Select(r => new UserRoleDto()
                {
                    RoleId = r.RoleId,
                    RoleTitle = ""
                }).ToList()
            };
        }

        public static async Task<UserDto> SetUserRoleTitles(this UserDto userDto, ShopContext shopContext)
        {
            var objRoleId = userDto.Roles.Select(r => r.RoleId);
            var result = await shopContext.Roles.Where(r => objRoleId.Contains(r.Id)).ToListAsync();
            List<UserRoleDto> model = new List<UserRoleDto>();
            foreach (var role in result)
            {
                model.Add(new UserRoleDto()
                {
                    RoleId = role.Id,
                    RoleTitle = role.Title,
                });
            }

            userDto.Roles = model;
            return userDto;
        }

        public static UserFilterData MapFilterData(this User user)
        {
            return new UserFilterData()
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                AvatarName = user.AvatarName,
                Gender = user.Gender,
                CreationDate = user.CreationDate,
            };
        }

        

    }
}
