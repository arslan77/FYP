using Attendance_System.Attributes;
using Attendance_System.Models;
using Attendance_System.Utility;
using Attendance_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Attendance_System.Controllers
{
    public class SubjectCourseDetailController : ApiController
    {
        // GET: api/SubjectCourseDetail
        [AuthorizeRequest]
        public IQueryable Get()
        {
            
            String token = Request.Headers.Authorization.Parameter;
            
            List<ClassCourseDetail> details = new List<ClassCourseDetail>();
            if (AccountManager.getRoleFromToken(token) == "teacher" || AccountManager.getRoleFromToken(token) == "admin")
            {
                String username = AccountManager.getUsernameFromToken(token);
                DataManager dm = new DataManager();
                int teacherID = dm.getTeacherID(username);
                details = dm.getClassCourseDetail(teacherID);

            }
            else
            {
             
            }
            return details.AsQueryable();
            
        }
        ProjectEntities db = new ProjectEntities();
        
        // POST: api/SubjectCourseDetail
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SubjectCourseDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SubjectCourseDetail/5
        public void Delete(int id)
        {
        }
    }
}
