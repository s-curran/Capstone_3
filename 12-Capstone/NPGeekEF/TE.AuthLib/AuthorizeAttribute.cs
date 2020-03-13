using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TE.AuthLib
{
    /// <summary>
    /// The authorization filter is used to indicate whether a controller action needs
    /// to have the user authenticated and if they need to meet certain roles.
    /// </summary>
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        public class AuthorizeAttributeOptions
        {
            public string LoginRedirectController { get; set; } = "UserAccount";
            public string LoginRedirectAction { get; set; } = "Signin";
        }
        static public AuthorizeAttributeOptions Options = new AuthorizeAttributeOptions();

        private string[] roles;

        public AuthorizeAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        /// <summary>
        /// Called after the action executes.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        /// <summary>
        /// Called before the action executes.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Get the authentication provider. Attributes don't support constructor injection
            IAuthProvider authProvider = context.HttpContext.RequestServices.GetService<IAuthProvider>();

            // If they aren't logged in, force them to login first.
            if (!authProvider.IsLoggedIn)
            {                
                context.Result = new RedirectToRouteResult(new
                {
                    controller = Options.LoginRedirectController,
                    action = Options.LoginRedirectAction,                    
                });
                return;
            }

            // If they are logged in and the user doesn't have any of the roles
            // give them a 403
            if (roles.Length > 0 && !authProvider.UserHasRole(roles))
            {
                // User shouldn't have access
                context.Result = new StatusCodeResult(403);
                return;
            }

            // If our code gets this far then the filter "lets them through".
        }
    }
}
