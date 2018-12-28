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
    [AuthorizeRequest]
    public class AttendanceApiController : ApiController
    {
        ProjectEntities db = new ProjectEntities();
        // GET: api/AttendanceApi
        public IQueryable Get()
        {
            DataManager dm = new DataManager();
            List<AttendanceReport> list = new List<AttendanceReport>();
            String token = Request.Headers.Authorization.Parameter;
            int teacherId = dm.getTeacherID(AccountManager.getUsernameFromToken(token));
            List<ClassCourseDetail> detail = dm.getClassCourseDetail(teacherId);
            foreach (ClassCourseDetail sdetail in detail)
            {
                List<Student> students = db.Students.Where(o => o.Classes.Any(c => c.classID == sdetail.classID)).ToList<Student>();
                foreach (Student std in students)
                {
                    AttendanceReport obj = new AttendanceReport();
                    obj.classID = sdetail.classID;
                    obj.courseCode = sdetail.courseCode;
                    obj.rollNo = std.rollNo;
                    StudentsDetailAttendance stdetail = dm.getAttendanceDetail(std.rollNo, sdetail.classID, sdetail.courseCode, teacherId);
                    obj.presentPercentage = stdetail.presentPercentage;
                    obj.totalAttendance = stdetail.totalAttendace;
                    obj.absentPercentage = stdetail.abscentPercentage;
                    list.Add(obj);
                }
            }
            return list.AsQueryable();
           
        }

        // GET: api/AttendanceApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AttendanceApi
        public IQueryable Post(List<AttendanceModel> data)
        {
            DataManager dm = new DataManager();
            String token = Request.Headers.Authorization.Parameter;
            int teacherId = dm.getTeacherID(AccountManager.getUsernameFromToken(token));
            foreach (AttendanceModel obj in data)
            {
                Attendance x = new Attendance();
                x.classID = obj.classID;
                DateTime dateTime = new DateTime(1970,1,1);
                TimeSpan time = TimeSpan.FromMilliseconds(obj.date);
                dateTime = dateTime.Add(time);
                dateTime = dateTime.AddHours(5);
                x.date = dateTime;
                x.teacherID = teacherId;
                x.rollNo = obj.rollNO;
                x.courseCode = obj.courseCode;
                x.status = obj.status;
                db.Attendances.Add(x);
                db.SaveChanges();

            }
            return new String []{"OK", "WE" }.AsQueryable();
        }

        // PUT: api/AttendanceApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AttendanceApi/5
        public void Delete(int id)
        {
        }
    }
}
