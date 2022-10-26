using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scribe_Factory_Core.Common;
using Scribe_Factory_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.CustomFilters
{
    

    public class AuthorizationFilter : Attribute,IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUser = SessionHelper.GetObject<UserLoginViewModel>(context.HttpContext.Session, "CurrentUser");
            if (currentUser == null)
            {
                context.Result =  new RedirectResult("User/Login");
            }
        }
    }
}
