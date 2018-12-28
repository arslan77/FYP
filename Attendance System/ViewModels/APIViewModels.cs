using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System.ViewModels
{
    public class AttendanceReport
    {
        public string rollNo { get; set; }
        public int classID { get; set; }
        public string courseCode { get; set; }
        public decimal presentPercentage { get; set; }
        public decimal absentPercentage { get; set; }
        public int totalAttendance { get; set; }
    }

    public class GuardianDetailModel
    {
        public int guardianID { get; set; }
        public string guardianName { get; set; }
        public string relationship { get; set; }
        public string mobileNumber { get; set; }
    }
    public class AttendanceModel
    {
        public int classID { get; set; }
        public String rollNO { get; set; }
        public long date { get; set; }
        public String status { get; set; }
        public String courseCode { get; set; }

    }
    

    public class ClassCourseDetail
    {
        public String className { get; set; }
        public int classID { get; set; }
        public String courseCode { get; set; }
        public String courseName { get; set; }
        public bool isLab { get; set; }

    }
    public class UserInformation
    {
        public String token { get; set; } 
        public String name { get; set; }
        public String role { get; set; }
        public String status { get; set; }
    }
    public class StudentInformation
    {
        public String rollNo { get; set; }
        public String studentName { get; set; }
        public String gender { get; set; }
        public String fatherName { get; set; }
        public String sessionName { get; set; }
        public String guardianId { get; set; }

    }
}