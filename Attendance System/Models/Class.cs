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
    
    public partial class Class
    {
        public Class()
        {
            this.Attendances = new HashSet<Attendance>();
            this.Teacher_Course_Allocation = new HashSet<Teacher_Course_Allocation>();
            this.Students = new HashSet<Student>();
        }
    
        public int classID { get; set; }
        public string name { get; set; }
        public Nullable<int> sectionID { get; set; }
        public int sessionID { get; set; }
        public bool isMorning { get; set; }
        public bool isTheory { get; set; }
        public bool isSupply { get; set; }
    
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual Section Section { get; set; }
        public virtual Session Session { get; set; }
        public virtual ICollection<Teacher_Course_Allocation> Teacher_Course_Allocation { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
