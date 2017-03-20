using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Validation
{
    public static class Validator
    {      
        public static async Task<bool> ValidateAsync(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            var identities =  userPrincipal.Identities;
           // var identity = (userPrincipal)Thread.CurrentPrincipal;
            var claims = identities.ToList();

            return true;
            
        }

        //public static string GetUserName()
        //{
        //    return "Pankaj";
        //}

        //public static void OnAuthorization(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        //{
        //    var nop = Task.CompletedTask;
        //    var userPrincipal = context.HttpContext.User;
        //    //return false;

        //}
    }

}
