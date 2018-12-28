using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_System.ViewModels
{



    public class CreateUserAccount
    {
        [Display(Name = "ID")]
        public string userID { get; set; }

        [Display(Name = "Username (Must be unique.)")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Student Email:")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }



        public string imagePath { get; set; }
    }
    public class changePassword
    {
      

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }
    }


    public class loginviewmodel
    {
        [Required]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}