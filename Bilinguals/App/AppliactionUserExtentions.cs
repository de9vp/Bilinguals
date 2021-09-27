using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Bilinguals.App
{
    public static class AppliactionUserExtentions
    {
        //https://stackoverflow.com/questions/35982571/asp-net-identity-claim-fullname-in-view-page

        ///// <summary>
        /////     Return the user name using the UserNameClaimType
        ///// </summary>
        ///// <param name="identity"></param>
        ///// <returns></returns>
        //public static string GetUserName(this IIdentity identity)
        //{
        //    if (identity == null)
        //    {
        //        throw new ArgumentNullException("identity");
        //    }
        //    var ci = identity as ClaimsIdentity;
        //    if (ci != null)
        //    {
        //        return ci.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
        //    }
        //    return null;
        //}

        public static string GetFullName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;

            if (ci != null)
            {
                return ci.FindFirstValue(ClaimTypes.GivenName);
            }
            return null;
        }
    }
}