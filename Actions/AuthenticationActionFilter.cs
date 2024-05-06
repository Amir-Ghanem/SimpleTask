using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SimpleTask.Models.ViewModels;

namespace SimpleTask.Actions
{
    public class AuthenticationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAuthenticated = CurrentUser.IsAuthenticated;

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
