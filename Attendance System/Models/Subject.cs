//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Attendance_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subject
    {
        public Subject()
        {
            this.Attendances = new HashSet<Attendance>();
            this.Teacher_Course_Allocation = new HashSet<Teacher_Course_Allocation>();
        }
    
        public string courseCode { get; set; }
        public string name { get; set; }
        public int theoryHours { get; set; }
        public int labHours { get; set; }
    
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Teacher_Course_Allocation> Teacher_Course_Allocation { get; set; }
    }
}