using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork
{
    public class AuthorizeAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            /*  This part will use if different use have permission access control from xml role permission
             * 
             */
            string controller, action;
            HttpContext context = httpContext.ApplicationInstance.Context;
            controller = context.Request.RequestContext.RouteData.Values["controller"].ToString();
            action = context.Request.RequestContext.RouteData.Values["action"].ToString();

            if (System.Web.HttpContext.Current.Session["role"] != null)
            {
                if (Convert.ToInt16(System.Web.HttpContext.Current.Session["role"]) == (int)UserType.Merchant)
                {
                    return true;
                }
                if (Convert.ToInt16(System.Web.HttpContext.Current.Session["role"]) == (int)UserType.admin)
                {
                    return true;

                }
            }
            
            
            return false;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }

            //filterContext.Result = new RedirectToRouteResult(new
            //System.Web.Routing.RouteValueDictionary(new { controller = "UserLogin", action = "Login" }));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Unauthorized/Access");
        }
    }
}