using Attendance_System.Attributes;
using Attendance_System.Models;
using Attendance_System.Utility;
using Attendance_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System.Controllers
{
    [AuthorizeUser]
    public class SubjectClassDetailController : Controller
    {
        // GET: SubjectClassDetail
        public ActionResult Index()
        {
            if (AccountManager.IsUserInRole("student"))
            {
                return RedirectToAction("Student", "SubjectClassDetail");
            }
            else if (AccountManager.IsUserInRole("teacher"))
            {
                return RedirectToAction("Teacher", "SubjectClassDetail");
            }

            return View();
        }
        ProjectEntities db = new ProjectEntities();
        public ActionResult Student()
        {
            String rollNo = AccountManager.getId();
            List<Teacher_Course_Allocation> detailList;
            DataManager dm = new DataManager();
            List<SubjectClassDetail> list = new List<SubjectClassDetail>();
            List<Class> classes = db.Classes.Where(o=> o.Students.Any(s=>s.rollNo==rollNo)).ToList<Class>();
            foreach (Class obj in classes)
            {
                String className = obj.name;
                detailList = db.Teacher_Course_Allocation.Where(o => o.classID == obj.classID).ToList<Teacher_Course_Allocation>();
                foreach (Teacher_Course_Allocation detail in detailList)
                {
                    SubjectClassDetail data = new SubjectClassDetail();
                    data.className = className;
                    data.teacherName = detail.Teacher.name;
                    data.courseName = detail.Subject.name;
                    data.isTheory = detail.Class.isTheory;
                    StudentAttendance stdAttendance = dm.getAttendance(detail.classID, detail.courseCode, detail.teacherID, rollNo);
                    data.presentPercentage = stdAttendance.presentPercentage;
                    data.abscentPercentage = stdAttendance.abscentPercentage;
                    data.totalAttendace = stdAttendance.totalAttendace;
                    list.Add(data);

                }
            }

            return View(list);
        }
        public ActionResult Teacher()
        {
            
            int teacherID = int.Parse( AccountManager.getId());
            List<TeacherAllocatedCourses> list = new List<TeacherAllocatedCourses>();
            List<Teacher_Course_Allocation> data = db.Teacher_Course_Allocation.Where(o => o.teacherID == teacherID).ToList<Teacher_Course_Allocation>();
            foreach (Teacher_Course_Allocation obj in data)
            {
                TeacherAllocatedCourses teacherAllocatedCourses = new TeacherAllocatedCourses();
                teacherAllocatedCourses.classID = obj.classID;
                teacherAllocatedCourses.courseCode = obj.Subject.courseCode;
                teacherAllocatedCourses.className = obj.Class.name;
                teacherAllocatedCourses.courseName = obj.Subject.name;
                teacherAllocatedCourses.isTheory = obj.Class.isTheory;
                teacherAllocatedCourses.totalStudent = db.Students.Where(o => o.Classes.Any(c => c.classID == obj.classID)).ToArray().Length;
                list.Add(teacherAllocatedCourses);
            }
            return View(list);
        }

        public ActionResult Details(int classID, string courseCode)
        {
            int teacherID = int.Parse(AccountManager.getId());
            DataManager dm = new DataManager(); 
            List<Student> students = db.Students.Where(o => o.Classes.Any(c => c.classID == classID)).ToList<Student>();
            List<StudentsDetailAttendance> list = new List<StudentsDetailAttendance>();
            foreach (Student obj in students)
            {
                list.Add(dm.getAttendanceDetail(obj.rollNo, classID, courseCode, teacherID));
            }
            ViewBag.classID = classID;
            ViewBag.courseCode = courseCode;

            return View(list);
        }

        public ActionResult ShowAttendanceDetails(int classID, String courseCode, String rollNo)
        {
            DataManager dm = new DataManager();
            int teacherID = int.Parse(AccountManager.getId());
            List<AttendanceForSingleStudent> list = dm.getAttendanceForSingleStudent(teacherID, classID, courseCode, rollNo);

            return View(list);
        }

    }
}
