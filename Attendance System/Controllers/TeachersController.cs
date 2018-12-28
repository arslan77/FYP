using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance_System.Models;
using Attendance_System.Attributes;
using Attendance_System.ViewModels;
using Attendance_System.Utility;

namespace Attendance_System.Controllers
{
    [AuthorizeUser(Role = "admin")]
    public class TeachersController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Teachers
        public async Task<ActionResult> Index()
        {
            var teachers = db.Teachers.Include(t => t.Userinfo);
            return View(await teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                Userinfo u = db.Userinfoes.Find(teacher.username);
                if (u != null)
                {
                    ViewBag.userMsg = "Username Exist";
                    return View();
                }
                else if (teacher.password == null)
                {
                    ViewBag.userMsg = "Password should not be null";
                    return View();
                }
                else if (teacher.password != teacher.confirmPassword)
                {
                    ViewBag.userMsg = "Password not matched";
                    return View();
                }

                Userinfo user = new Userinfo();
                user.username = teacher.username;
                user.role = teacher.role;
                user.email = teacher.email;
                user.passwordHash = Cryptography.getMD5( teacher.password);
                user.token = Cryptography.getMD5(teacher.username) + Cryptography.getMD5( teacher.password);
                db.Userinfoes.Add(user);
                db.SaveChanges();
                Teacher tchr = new Teacher();
                tchr.name = teacher.teacherName;
                tchr.email = teacher.email;
                tchr.username = teacher.username;
                db.Teachers.Add(tchr);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
           // ViewBag.username = new SelectList(db.Userinfoes, "username", "password", teacher.username);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "teacherID,name,email,username")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.username = new SelectList(db.Userinfoes, "username", "password", teacher.username);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
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
    }
}
