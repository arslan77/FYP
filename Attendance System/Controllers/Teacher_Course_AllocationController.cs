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

namespace Attendance_System.Controllers
{
    [AuthorizeUser(Role = "admin")]
    public class Teacher_Course_AllocationController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Teacher_Course_Allocation
        public async Task<ActionResult> Index()
        {
            var teacher_Course_Allocation = db.Teacher_Course_Allocation.Include(t => t.Class).Include(t => t.Subject).Include(t => t.Teacher);
            return View(await teacher_Course_Allocation.ToListAsync());
        }

        // GET: Teacher_Course_Allocation/Details/5
        public ActionResult Details(int teacherID, int classID, String courseCode)
        {
            Teacher_Course_Allocation teacher_Course_Allocation = db.Teacher_Course_Allocation.Where(o => o.teacherID == teacherID && o.classID == classID && o.courseCode == courseCode).First();
            if (teacher_Course_Allocation == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Course_Allocation);
        }

        // GET: Teacher_Course_Allocation/Create
        public ActionResult Create()
        {
            ViewBag.classID = new SelectList(db.Classes, "classID", "name");
            ViewBag.courseCode = new SelectList(db.Subjects, "courseCode", "name");
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "name");
            return View();
        }

        // POST: Teacher_Course_Allocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "teacherID,classID,courseCode,isCurrent")] Teacher_Course_Allocation teacher_Course_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Course_Allocation.Add(teacher_Course_Allocation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.classID = new SelectList(db.Classes, "classID", "name", teacher_Course_Allocation.classID);
            ViewBag.courseCode = new SelectList(db.Subjects, "courseCode", "name", teacher_Course_Allocation.courseCode);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "name", teacher_Course_Allocation.teacherID);
            return View(teacher_Course_Allocation);
        }

        // GET: Teacher_Course_Allocation/Edit/5
        public ActionResult Edit(int teacherID, int classID, String courseCode)
        {
           
            Teacher_Course_Allocation teacher_Course_Allocation = db.Teacher_Course_Allocation.Where(o=>o.teacherID==teacherID&&o.classID==classID&&o.courseCode==courseCode).First();
            if (teacher_Course_Allocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.classID = new SelectList(db.Classes, "classID", "name", teacher_Course_Allocation.classID);
            ViewBag.courseCode = new SelectList(db.Subjects, "courseCode", "name", teacher_Course_Allocation.courseCode);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "name", teacher_Course_Allocation.teacherID);
            return View(teacher_Course_Allocation);
        }

        // POST: Teacher_Course_Allocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "teacherID,classID,courseCode,isCurrent")] Teacher_Course_Allocation teacher_Course_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Course_Allocation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.classID = new SelectList(db.Classes, "classID", "name", teacher_Course_Allocation.classID);
            ViewBag.courseCode = new SelectList(db.Subjects, "courseCode", "name", teacher_Course_Allocation.courseCode);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "name", teacher_Course_Allocation.teacherID);
            return View(teacher_Course_Allocation);
        }

        // GET: Teacher_Course_Allocation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Course_Allocation teacher_Course_Allocation = await db.Teacher_Course_Allocation.FindAsync(id);
            if (teacher_Course_Allocation == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Course_Allocation);
        }

        // POST: Teacher_Course_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Teacher_Course_Allocation teacher_Course_Allocation = await db.Teacher_Course_Allocation.FindAsync(id);
            db.Teacher_Course_Allocation.Remove(teacher_Course_Allocation);
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
