using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            string username;
            username = (from c in principal.Claims
                        where c.Type == "Username"
                        select c.Value).FirstOrDefault();
            return username;
        }
    }
}
