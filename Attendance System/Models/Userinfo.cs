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
    
    public partial class Userinfo
    {
        public Userinfo()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
        }
    
        public string username { get; set; }
        public string passwordHash { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public string imagePath { get; set; }
    
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}