using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_System.ViewModels
{

    public class StudentViewModel
    {
        public StudentViewModel()
        {

        }
        public StudentViewModel(Student student, Userinfo user)
        {
            this.rollNo = student.rollNo;
            this.firstName = student.fullName;
            this.fatherName = student.fatherName;
            this.sessionID = student.sessionID;
            this.guardianID = student.guardianID;
            this.gender = student.gender;
            this.username = student.username;
            if (user != null)
            {
                this.email = user.email;
                this.imagePath = user.imagePath;


            }
        }

        [Required]
        [Display(Name = "Roll #")]
        public string rollNo { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }



        [Required]
        [Display(Name = "Session:")]
        public int sessionID { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string fatherName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Student Email:")]
        public string email  { get; set; }


        [Display(Name = "Guardian")]
        public int? guardianID { get; set; }


        [Display(Name = "Username (Must be unique.)")]
        public string username { get; set; }
        
        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        public string imagePath { get; set; }
        
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }

    }

    public class AssignedStudentData
    {
        public string rollno { get; set; }
        public string full_name { get; set; }
        public bool Assigned { get; set; }
    }
    public class TestModel
    {
        public string imagePath { get; set; }
    }
    public class StudentAttendance
    {
        public decimal presentPercentage { get; set; }
        public decimal abscentPercentage { get; set; }
        public int totalAttendace { get; set; }
    }



    public class SubjectClassDetail
    {
        [Display(Name = "Class Name")]
        public string className { get; set; }
        [Display(Name = "Subject")]
        public string courseName { get; set; }
        [Display(Name = "Teacher")]
        public string teacherName { get; set; }
        [Display(Name = "Theory")]
        public Boolean isTheory { get; set; }
        [Display(Name = "Present (%age)")]
        public decimal presentPercentage { get; set; }
        [Display(Name = "Abcent (%age)")]
        public decimal abscentPercentage { get; set; }
        [Display(Name = "Total Attendance")]
        public int totalAttendace { get; set; }
    }



    public class GuardianViewModel
    {
        public string rollNo { get; set; }
        public string name { get; set; }
        public string relationship { get; set; }
        public string mobileNumber { get; set; }
    }
}