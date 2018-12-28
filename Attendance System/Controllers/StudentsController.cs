using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using Attendance_System.Models;
using Attendance_System.Attributes;
using Attendance_System.ViewModels;
using Attendance_System.Utility;
using System.IO;

namespace Attendance_System.Controllers
{

        [AuthorizeUser(Role = "admin")]
    public class StudentsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.Session).Include(s => s.Userinfo);
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( StudentViewModel student, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                Student std = new Student();
                Userinfo user = new Userinfo();
                if (db.Userinfoes.Find(student.username) != null)
                {
                    ViewBag.userMsg = "Username already exist.";
                    ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
            
                    return View(student);
                }
                else if (student.password == null)
                {
                    ViewBag.pswMsg = "Password can not be null";
                    ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
            
                    return View(student);
                }
                else if (student.password != student.confirmPassword)
                {
                    ViewBag.pswMsg = "Password are not same";
                    ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
            
                    return View(student);
                }
                user.username = student.username;
                user.passwordHash = Cryptography.getMD5( student.password);
                user.token = Cryptography.getMD5(student.username) + Cryptography.getMD5(student.password);
                user.role = "student";
                user.email = student.email;

                if (upload != null && upload.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                       Path.GetFileName(student.rollNo+".jpg"));
                    upload.SaveAs(path);
                    user.imagePath = "~/Images/"+student.rollNo+".jpg";
                }
                db.Userinfoes.Add(user);
                std.rollNo = student.rollNo;
                std.fullName = student.firstName;
                std.username = student.username;
                std.gender = student.gender;
                std.fatherName = student.fatherName;
                std.guardianID = student.guardianID;
                std.sessionID = student.sessionID;
                db.Students.Add(std);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
                return View(student);
            }
            
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            Userinfo user = await db.Userinfoes.FindAsync(student.username);

            if (student == null)
            {
                return HttpNotFound();
            }
            StudentViewModel std = new StudentViewModel(student, user);
            
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
            
            return View(std);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentViewModel student, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                string un = "";
                Student std = db.Students.Find(student.rollNo);
                if (std == null)
                {
                    Userinfo user = new Userinfo();
                
                    if (db.Userinfoes.Find(student.username) != null)
                    {
                        ViewBag.userMsg = "Username already exist.";
                        ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);

                        return View(student);
                    }
                    else if (student.password == null)
                    {
                        ViewBag.pswMsg = "Password can not be null";
                        ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);

                        return View(student);
                    }
                    else if (student.password != student.confirmPassword)
                    {
                        ViewBag.pswMsg = "Password are not same";
                        ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);

                        return View(student);
                    }
                    user.username = student.username;
                    user.passwordHash = Cryptography.getMD5(student.password);
                    user.token = Cryptography.getMD5(student.username) + Cryptography.getMD5(student.password);
                    user.role = "student";
                    user.email = student.email;

                    if (upload != null && upload.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                           Path.GetFileName(student.rollNo + ".jpg"));
                        upload.SaveAs(path);
                        user.imagePath = "~/Images/" + student.rollNo + ".jpg";
                    }

                    db.Userinfoes.Add(user);
                    un = user.username;
                }
                else
                {
                    Userinfo user = db.Userinfoes.Find(db.Students.Find(student.rollNo).username);
                    if (student.password != null)
                    {
                        if (student.password != student.confirmPassword)
                        {
                            ViewBag.pswMsg = "Password are not same";
                            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);

                            return View(student);
                        }
                        user.passwordHash = Cryptography.getMD5(student.password);
                        user.token = Cryptography.getMD5(user.username) + Cryptography.getMD5(student.password);
                    }
                    user.email = student.email;

                    if (upload != null && upload.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                           Path.GetFileName(student.rollNo + ".jpg"));
                        upload.SaveAs(path);
                        user.imagePath = "~/Images/" + student.rollNo + ".jpg";
                    }
                    un = user.username;

                    db.Set<Userinfo>().AddOrUpdate(user);

                    db.SaveChanges();
                    //  db.Entry(user).State = EntityState.Modified;
                    
                }
                std.rollNo = student.rollNo;
                std.fullName = student.firstName;
                std.username = un;
                std.fatherName = student.fatherName;
                std.sessionID = student.sessionID;
                db.Set<Student>().AddOrUpdate(std);
                //db.Entry(std).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", student.sessionID);
            ViewBag.username = new SelectList(db.Userinfoes, "username", "password", student.username);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        public ActionResult AddUserAccount(string rollNo)
        {
            CreateUserAccount user = new CreateUserAccount();
            user.userID = rollNo;
            return View(user);
        }

        [HttpPost, ActionName("AddUserAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult InsertUserAccount(CreateUserAccount student, HttpPostedFileBase upload)
        {
            Userinfo user = new Userinfo();

            if (db.Userinfoes.Find(student.username) != null)
            {
                ViewBag.userMsg = "Username already exist.";
               
                return View(student);
            }
            else if (student.password == null)
            {
                ViewBag.pswMsg = "Password can not be null";
                
                return View(student);
            }
            else if (student.password != student.confirmPassword)
            {
                ViewBag.pswMsg = "Password are not same";
                
                return View(student);
            }
            user.username = student.username;
            user.passwordHash = Cryptography.getMD5(student.password);
            user.token = Cryptography.getMD5(student.username) + Cryptography.getMD5(student.password);
            user.role = "student";
            user.email = student.email;

            if (upload != null && upload.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Images"),
                                   Path.GetFileName(student.userID + ".jpg"));
                upload.SaveAs(path);
                user.imagePath = "~/Images/" + student.userID + ".jpg";
            }

            db.Userinfoes.Add(user);
            
            string un = user.username;

            Student std = db.Students.Find(student.userID);
            try { 
            std.username = un;
                }
            catch (Exception)
            {

            }
            db.Set<Student>().AddOrUpdate(std);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Student student = await db.Students.FindAsync(id);
            //var obj = db.Students.Include(a => a.Classes).SingleOrDefault(a => a.rollNo == id);
            var obj = student.Classes;
            if (obj != null)
            {
                foreach (var item in obj.ToList())
                {
                    obj.Remove(item);
                }
            }
            List<Attendance> list = db.Attendances.Where(a => a.rollNo == id).ToList<Attendance>();
            foreach (Attendance item in list)
            {
                db.Attendances.Remove(item);
            }
            Userinfo user = db.Userinfoes.Find(student.username);
            if (user != null) 
            db.Userinfoes.Remove(user);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        

        public ActionResult AddStudentToClass()
        {
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name");
            return View();
        }

        
    }
}
