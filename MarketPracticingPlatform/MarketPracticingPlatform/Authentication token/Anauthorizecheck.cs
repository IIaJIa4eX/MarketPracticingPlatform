using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Authentication_token
{
 
        public class UnAuthorizedAttribute : TypeFilterAttribute
        {
            public UnAuthorizedAttribute() : base(typeof(UnauthorizedFilter))
            {

            }
        }
        public class UnauthorizedFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                bool IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
                if (!IsAuthenticated)
                {
                    context.Result = new RedirectResult("~/Registration/Index");
                }
            }
        }

}
