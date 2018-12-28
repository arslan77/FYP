using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance_System.Utility;
using System.Web.Mvc;
using System.Web.Routing;

namespace Attendance_System.Attributes
{
    //Class for checking user is signin or not? or user is in Role that access this page ??
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        //Role parameter
        public string Role { get; set; }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            bool a;
            if (SessionManager.CheckSession("login"))
                a = bool.Parse(SessionManager.ReturnSessionObject("login").ToString());
            else
                a = false;
            //User isn't logged in
            if (!a)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "login" })
                );
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "NotAuthorized" })
                );
            }
        }
        //end


        //Core authentication, called before each action
        //called when we call authentication
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool a;
            if (SessionManager.CheckSession("login"))
                a = bool.Parse(SessionManager.ReturnSessionObject("login").ToString());
            else
                a = false;
            var b = SessionManager.ReturnSessionObject("role");
            //Is user logged in?
            if (a)
                //Is user is in proper role ? Role = Null will b true when user not 
                //not assigned to any role
                if(Role != null)
                {
                    Role = Role.Replace(" ","");
                    string[] var = Role.Split(',');
                    foreach (string item in var)
                    {
                        if(item.Equals(b.ToString()))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            //Returns true or false, meaning allow or deny. False will call HandleUnauthorizedRequest above
            return a;
        }
        //end
    }
}