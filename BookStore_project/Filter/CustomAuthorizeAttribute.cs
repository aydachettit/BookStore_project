using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
    {
    }

    private class CustomAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // user is not authenticated, redirect to login page
                context.Result = new RedirectToActionResult("Login", "User", new { returnUrl = context.HttpContext.Request.Path });
            }
            else if (!context.HttpContext.User.IsInRole("Admin"))
            {
                // user is not authorized, redirect to access denied page
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}