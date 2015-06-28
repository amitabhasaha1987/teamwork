using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited=true,AllowMultiple=true)]
    public class AuthorizeAccess : AuthorizeAttribute
    {
        public AuthorizeAccess(params UserType[] roles)
        {

            this.Roles = roles;

        }
        public UserType[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            /*  This part will use if different use have permission access control from xml role permission
             * 
             * string controller, action;
             * HttpContext context = httpContext.ApplicationInstance.Context;
             * controller = context.Request.RequestContext.RouteData.Values["controller"].ToString();
             * action = context.Request.RequestContext.RouteData.Values["action"].ToString();
             * 
             */


            if (System.Web.HttpContext.Current.Session["role"] != null)
            {
                UserType userrole = (UserType)(Convert.ToByte(System.Web.HttpContext.Current.Session["role"]));
                if (Array.Exists(Roles, e => e == userrole))
                {
                    return true;
                }
                else
                {
                    return false;
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
            if (System.Web.HttpContext.Current.Session["role"] != null)
            {
                filterContext.Result = new RedirectResult("/Unauthorized/Access");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
            
        }
    }
}