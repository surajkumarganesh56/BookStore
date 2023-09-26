using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Book.Store.Web.Utilities;

namespace Book.Store.Web.Filters
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly BookStoreSession _bookStoreSession;
        public CustomAuthorizeAttribute(BookStoreSession bookStoreSession)
        {
            _bookStoreSession=bookStoreSession;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            string? controllerName = context.RouteData.Values["controller"]?.ToString();
            string? actionName = context.RouteData.Values["action"]?.ToString();
            string? areaName = context.RouteData.Values["area"]?.ToString();
            if (controllerName == "Home" && actionName == "Index" && string.IsNullOrEmpty(areaName))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "User", area = "User" }));
            }
            else if (controllerName != "User" && actionName != "Index" && areaName != "User" && !string.IsNullOrEmpty(areaName) && _bookStoreSession.UserId == 0)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "User", area = "User" }));
            }
        }
    }
}
