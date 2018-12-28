using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance_System.Models;

namespace Attendance_System.ViewModels
{

    public class AddStudents
    {
        public int classID { get; set; }
        public int sessionID { get; set; }
        public List<String> AssignedStudent { get; set; }
    }


    public class NewClass
    {


        public int classID { get; set; }
        [Display(Name = "Class Name")]
        public string name { get; set; }
        [Display(Name = "Session")]
        public int sessionID { get; set; }
        [Display(Name = "Section")]
        public int sectionID { get; set; }
        [Display(Name = "Morning")]
        public bool isMorning { get; set; }
        [Display(Name = "Theory")]
        public bool isTheory { get; set; }
        [Display(Name = "Supply Class")]
        public bool isSupply { get; set; }

        public virtual ICollection<AttendanceModel> Attendances { get; set; }
        public virtual Session Session1 { get; set; }
        public virtual ICollection<Teacher_Course_Allocation> Teacher_Course_Allocation { get; set; }
        public virtual ICollection<AssignedStudentData> Students { get; set; }
    }
}