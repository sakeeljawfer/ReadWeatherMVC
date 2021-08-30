using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Filters
{
    //Impleminting Filter Pipline
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = context.HttpContext.Request.Headers.ContainsKey("Authorization");

            var token = string.Empty;
            if (result) token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;

            if (!result)
            {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized.");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
