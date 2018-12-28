using Attendance_System.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Attendance_System.Attributes
{
    public class AuthorizeRequestAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override bool AllowMultiple
        {   
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Perform your logic here
            base.OnAuthorization(actionContext);
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            String token="";
            try { 
                    token = actionContext.Request.Headers.Authorization.Parameter;
                }
            catch(NullReferenceException)
            {
                token = "-1";

            }
            finally
            {
                if(token==""||token==null)
                {
                    token = "-1";
                }
            }
            if (AccountManager.validateToken(token))
            {
                if(Roles == AccountManager.getRoleFromToken(token) || Roles == "")
                  return true;
            }
            return false;
        }
       
    }
}