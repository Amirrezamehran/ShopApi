using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Domain.RoleAggregate.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Users;

namespace Shop.Api.Infrastructure.Security;


// این کلاس برای اینه که بفهمیم هرکاربر نقشش چیه و به چه چیزهایی دسترسی داره
// مشخص میکنه مثلا نقشش مدیره Role نقش رو
// مشخص میکنه مثلا به همه چیز دسترسی داره Permission و سطح دسترسی رو
// حتی ممکنه یک کاربر چند نقش داشته باشد
public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IUserFacade _userFacade;
    private IRoleFacade _roleFacade;
    private readonly Permission _permission;

    public PermissionChecker(Permission permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
            return;

        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorize");
        }
    }


    // باشند AllowAnonymous این متد باعث میشه بتونیم مشخص کنیم که کدام متد ها میتوانند
    // و بدون لاگین کردن یا بدون داشتن سطح دسترسی هم به آنها دسترسی داشته باشیم و فراخوانی بشوند
    // این تابع در قسمت 96 و 10 دقیقه پایانی توضیح داده شده
    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        // این جزئیات اون اکشنی که بهش در لحظه ریکوییست زدیم رو میده ActionDescriptor

        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }

        return hasAllowAnonymous;
    }

    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.User.GetUserId();
        var user = await _userFacade.GetUserById(userId);

        if (user == null)
            return false;

        var roleIds = user.Roles.Select(s => s.RoleId).ToList();
        var roles = await _roleFacade.GetRoleList();

        var userRoles = roles.Where(r => roleIds.Contains(r.Id));

        return userRoles.Any(r => r.Permissions.Contains(_permission));
    }
}
