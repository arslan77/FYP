using Attendance_System.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Role = "admin")]
        public ActionResult Classes()
        {
            ViewBag.Message = "Classes.";
            return RedirectToAction("index", "Classes");
        }
        [AuthorizeUser(Role = "admin")]
        public ActionResult Subjects()
        {
            ViewBag.Message = "Subjects.";
            return RedirectToAction("index", "Subjects");
        }

        [AuthorizeUser (Role = "admin, teacher")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AuthorizeUser(Role = "admin, teacher")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}