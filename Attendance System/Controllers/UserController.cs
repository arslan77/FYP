using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance_System.Attributes;
using Attendance_System.Utility;
using Attendance_System.ViewModels;
using Attendance_System.Models;
namespace Attendance_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        //Signout the user by deleting session
        public ActionResult LogOff()
        {
            AccountManager.Signout();
            return RedirectToAction("login", "user");
        }

        //This method will show login form if user is not signin.
        [HttpGet]
        public ActionResult login()
        {
            bool login = SessionManager.CheckSession("login");
            if (login)
            {

                return RedirectToAction("index", "Home");
            }
            return View();
        }
        //Login method for sign in purpose
        [HttpPost]
        public ActionResult login(loginviewmodel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                AccountManager.SigninResult result = AccountManager.Signin(user.username, user.Password);
                switch (result)
                {
                    case AccountManager.SigninResult.OK:
                        return RedirectToAction("index", "Home");
                    case AccountManager.SigninResult.WrongPassword:
                        ViewBag.msg = "Wrong password you have entered";
                        return View();
                    case AccountManager.SigninResult.WrongUsername:
                        ViewBag.msg = "This username does not exist in records";
                        return View();
                }
                return View(returnUrl);

            }
            else
                return View(returnUrl);
        }
        //end

        //This method for Un-Authorized request
        public ActionResult NotAuthorized()
        {
            return View();
        }
        private ProjectEntities db = new ProjectEntities();
        public ActionResult GetAllStudent()
        {
            var students = db.Students.ToList();
            List<AssignedStudentData> std = new List<AssignedStudentData>();
            foreach(var item in students)
            {
                AssignedStudentData temp = new AssignedStudentData();
                temp.full_name = item.fullName  ;
                temp.rollno = item.rollNo;
                temp.Assigned = false;
                std.Add(temp);
                
            }
            return Json(std, JsonRequestBehavior.AllowGet);
        }

    }
}