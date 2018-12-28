using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance_System.ViewModels;
using Attendance_System.Models;

namespace Attendance_System.Utility
{
    public class DataManager
    {
        private ProjectEntities db = new ProjectEntities();
        public StudentAttendance getAttendance(int classID, string courseCode, int teacherID, string rollNo)
        {
            List<Attendance> obj = db.Attendances.Where(o => o.classID == classID && o.courseCode == courseCode && o.teacherID == teacherID && o.rollNo == rollNo).ToList<Attendance>();
            StudentAttendance studentAttendance = new StudentAttendance();
            studentAttendance.totalAttendace = obj.Count;
            
            if(obj.Count==0)
            {
                studentAttendance.presentPercentage = 0;
                studentAttendance.abscentPercentage = 0;
            }
            else
            {
            Double presentVal = obj.Where(o => o.status == "Present").ToArray().Length;
            Decimal present = Decimal.Parse(((presentVal)/Double.Parse(obj.Count.ToString())* 100.00).ToString());
            studentAttendance.presentPercentage = present;
            Double abcentVal = obj.Where(o => o.status == "Absent").ToArray().Length;
            Decimal abcent = Decimal.Parse(((abcentVal) / Double.Parse(obj.Count.ToString()) * 100.00).ToString());
            studentAttendance.abscentPercentage = abcent;
            }
            return studentAttendance;
        }



        public List<StudentInformation> getStudentsFromSession(int? sessionID)
        {
            List<StudentInformation> list = new List<StudentInformation>();
            List<Student> students = db.Students.Where(o => o.sessionID==sessionID).ToList<Student>();
            foreach (var item in students)
            {
                StudentInformation obj = new StudentInformation();
                obj.rollNo = item.rollNo;
                obj.studentName = item.fullName;
                obj.fatherName = item.fatherName;
                obj.sessionName = db.Sessions.Find(item.sessionID).name;
                list.Add(obj);
            }
            return list;
        }

        public List<StudentInformation> getStudentsFromClass(int classID)
        {
            List<StudentInformation> list = new List<StudentInformation>();
            List<Student> students = db.Students.Where(o =>o.Classes.Any(c=> c.classID==classID)).ToList<Student>();
            foreach (var item in students)
            {
                StudentInformation obj = new StudentInformation();
                obj.rollNo = item.rollNo;
                obj.studentName = item.fullName;
                obj.fatherName = item.fatherName;
                obj.gender = item.gender;
                if (item.guardianID.HasValue)
                {
                    obj.guardianId = item.guardianID + "";
                }
                else
                {
                    obj.guardianId = "-1";
                }
                obj.sessionName = db.Sessions.Find(item.sessionID).name;
                list.Add(obj);
            }
            return list;
        }
       public int getTeacherID(String username)
        {

            Teacher obj = db.Teachers.Where(o => o.username == username).First();
            return obj.teacherID;
        }
       public String getRollNumber(String username)
       {
           return db.Students.Where(o => o.username == username).First().rollNo;
       }

       public List<AttendanceForSingleStudent> getAttendanceForSingleStudent(int teacherID, int classID, String courseCode, String rollNo)
       {
           List<AttendanceForSingleStudent> list = new List<AttendanceForSingleStudent>();
           List<Attendance> attendance = db.Attendances.Where(o => o.classID == classID && o.teacherID == teacherID && o.courseCode == courseCode && o.rollNo == rollNo).ToList();
           foreach (Attendance obj in attendance)
           {
               AttendanceForSingleStudent item = new AttendanceForSingleStudent();
               item.date = obj.date;
               item.status = obj.status;
               list.Add(item);
           }
           return list;
       }

        public List<ClassCourseDetail> getClassCourseDetail(int teacherID)
       {
           try { 
           var query = from obj in db.Teacher_Course_Allocation
                       where obj.teacherID == teacherID
                       select obj;
           List<ClassCourseDetail> list = new List<ClassCourseDetail>();
           foreach (var item in query)
           {
               if (item.isCurrent) { 
               ClassCourseDetail detail = new ClassCourseDetail();
               Class classObj = db.Classes.Find(item.classID);
               detail.classID = classObj.classID;
               detail.className = classObj.name;
               detail.isLab = !(classObj.isTheory);
               Subject subjectObj = db.Subjects.Find(item.courseCode);
               detail.courseCode = subjectObj.courseCode;
               detail.courseName = subjectObj.name;
               list.Add(detail);
               }
               else
               {

               }
           }
           return list;
           }
           catch (Exception ex)
           {
               Console.Write(ex.Message);
               return null;

           }

       }
        
        public StudentsDetailAttendance getAttendanceDetail(string rollNo, int classID, string courseCode, int teacherID){
            StudentsDetailAttendance detail = new StudentsDetailAttendance();
            detail.rollNo = rollNo;
            detail.studentName = db.Students.Find(rollNo).fullName;
            List<Attendance> obj = db.Attendances.Where(o => o.classID == classID && o.courseCode == courseCode && o.teacherID == teacherID && o.rollNo == rollNo).ToList<Attendance>();

            detail.totalAttendace = obj.Count;   
            if(obj.Count==0)
            {
                detail.presentPercentage = 0;
                detail.abscentPercentage = 0;
            }
            else
            {
            Double presentVal = obj.Where(o => o.status == "Present").ToArray().Length;
            Decimal present = Decimal.Parse(((presentVal)/Double.Parse(obj.Count.ToString())* 100.00).ToString());
            detail.presentPercentage = present;
            Double abcentVal = obj.Where(o => o.status == "Absent").ToArray().Length;
            Decimal abcent = Decimal.Parse(((abcentVal) / Double.Parse(obj.Count.ToString()) * 100.00).ToString());
            detail.abscentPercentage = abcent;
            
            }
            return detail;

    }

        public ICollection<AssignedStudentData> Populatestudentdata()
        {


            var query = from student in db.Students
                        select student;

            var x = new List<AssignedStudentData>();
                foreach (var item in query)
                {
                    x.Add(new AssignedStudentData
                    {
                        full_name = item.fullName,
                        rollno = item.rollNo,
                        Assigned = false
                    });
                }
           
            return x;
        }
    }
}