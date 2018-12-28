using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System.Utility
{
    public class MyHtmlHelper
    {
        private static ProjectEntities db = new ProjectEntities();
        public static int getTotalStudentInClass(int classID)
        {
            return db.Students.Where(o => o.Classes.Any(c => c.classID == classID)).ToArray().Length;
        }
    }
}