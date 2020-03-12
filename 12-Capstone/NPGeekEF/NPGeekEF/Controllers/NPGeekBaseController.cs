using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TE.AuthLib;

namespace NPGeekEF.Controllers
{
    public abstract class NPGeekBaseController : Controller
    {
        protected IAuthProvider authProvider;

        public NPGeekBaseController(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        protected bool IsLoggedIn
        {
            get
            {
                return authProvider.IsLoggedIn;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (IsLoggedIn)
            {
                ViewData["CurrentUser"] = authProvider.GetCurrentUser();
            }
        }
    }
}
