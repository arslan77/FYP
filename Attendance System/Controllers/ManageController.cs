using Attendance_System.Models;
using Attendance_System.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Attendance_System.Controllers
{
    public class ManageController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: Manage
        public ActionResult Index()
        {
            String username="";
            if(!AccountManager.IsUserInRole("student")){
                int id = Int16.Parse(AccountManager.getId());
                username=db.Teachers.Find(id).username;
            }
            else{
                String rollNo = AccountManager.getId();
                username = db.Students.Find(rollNo).username;
            }
            Userinfo user = db.Userinfoes.Find(username);


            return View(user);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ViewModels.changePassword data)
        {

            if (data.Password != null)
            {
                if (data.Password == data.confirmPassword)
                {

                    String username = "";
                    if (!AccountManager.IsUserInRole("student"))
                    {
                        int id = Int16.Parse(AccountManager.getId());
                        username = db.Teachers.Find(id).username;
                    }
                    else
                    {
                        String rollNo = AccountManager.getId();
                        username = db.Students.Find(rollNo).username;
                    }
                    Userinfo user = db.Userinfoes.Find(username);
                    user.passwordHash = Cryptography.getMD5(data.Password);
                    db.Set<Userinfo>().AddOrUpdate(user);
                    db.SaveChanges();
                    return RedirectToAction("index");
            
                }
                else
                {
                    ViewBag.warning = "Password Not Matched";
                    return View();
                }
            }
            else
            {
                ViewBag.warning = "Password can not be null";
                return View();
            }

        }

    }
}