using Attendance_System.Attributes;
using Attendance_System.Models;
using Attendance_System.Utility;
using Attendance_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Attendance_System.Controllers
{
    [AuthorizeRequest]
    public class UserApiController : ApiController
    {



        // POST: userApi
        [AllowAnonymous]
        public IQueryable Post([FromBody]Userinfo obj)
        {
            List<UserInformation> list = new List<UserInformation>();
            list.Add(AccountManager.returnUserInformation(obj.username, obj.passwordHash));
            return list.AsQueryable();
        }
        [HttpGet]
        public IQueryable Get()
        {
            return "value".AsQueryable();
        }
    }
}