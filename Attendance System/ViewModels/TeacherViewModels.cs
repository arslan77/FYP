using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_System.ViewModels
{
    public class AttendanceForSingleStudent
    
    {
         [Display(Name = "Date Time")]
        public DateTime date { get; set; }
         [Display(Name = "Status")]
        public String status { get; set; }
    }
    public class TeacherViewModel
    {

        [Display(Name = "Teacher Name:")]
        public string teacherName { get; set; }

        [Display(Name = "Email Address:")]
        public string email { get; set; }

        [Display(Name = "Username (must be unique):")]
        public string username { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }

        [Display(Name = "Role")]
        public string role { get; set; }

    }
    public class StudentsDetailAttendance
    {
         [Display(Name = "Roll #")]
        public string rollNo { get; set; }
         [Display(Name = "Student Name")]
        public string studentName { get; set; }
         [Display(Name = "Present (%age)")]
        public decimal presentPercentage { get; set; }
         [Display(Name = "Absent (%age)")]
        public decimal abscentPercentage { get; set; }
         [Display(Name = "Total Attendance")]
        public int totalAttendace { get; set; }
    }
    public class TeacherAllocatedCourses
    {
         [Display(Name = "Class Id")]
        public int classID { get; set; }
         [Display(Name = "Course Code")]
        public string courseCode { get; set; }
         [Display(Name = "Class Name")]
        public string className { get; set; }
         [Display(Name = "Course Name")]
        public string courseName { get; set; }
         [Display(Name = "Total Students")]
        public int totalStudent { get; set; }
         [Display(Name = "is Theory?")]
        public Boolean isTheory { get; set; }
    }
}