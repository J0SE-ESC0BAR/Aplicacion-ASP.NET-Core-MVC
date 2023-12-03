using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdventureWorksPhotos.Controllers
{
    public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);

        HttpCookie authCookie = filterContext.HttpContext.Request.Cookies["UserSession"];
        if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
        {
            // Redireccionar al usuario al login si no está autenticado
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
        }
    }
}
}