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
    public class DiciplinesController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Diciplines
        public async Task<ActionResult> Index()
        {
            var diciplines = db.Diciplines.Include(d => d.Department);
            return View(await diciplines.ToListAsync());
        }

        // GET: Diciplines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dicipline dicipline = await db.Diciplines.FindAsync(id);
            if (dicipline == null)
            {
                return HttpNotFound();
            }
            return View(dicipline);
        }

        // GET: Diciplines/Create
        public ActionResult Create()
        {
            ViewBag.deptID = new SelectList(db.Departments, "deptID", "name");
            return View();
        }

        // POST: Diciplines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "dicipID,name,deptID,totalSemester")] Dicipline dicipline)
        {
            if (ModelState.IsValid)
            {
                db.Diciplines.Add(dicipline);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.deptID = new SelectList(db.Departments, "deptID", "name", dicipline.deptID);
            return View(dicipline);
        }

        // GET: Diciplines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dicipline dicipline = await db.Diciplines.FindAsync(id);
            if (dicipline == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptID = new SelectList(db.Departments, "deptID", "name", dicipline.deptID);
            return View(dicipline);
        }

        // POST: Diciplines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "dicipID,name,deptID,totalSemester")] Dicipline dicipline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dicipline).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.deptID = new SelectList(db.Departments, "deptID", "name", dicipline.deptID);
            return View(dicipline);
        }

        // GET: Diciplines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dicipline dicipline = await db.Diciplines.FindAsync(id);
            if (dicipline == null)
            {
                return HttpNotFound();
            }
            return View(dicipline);
        }

        // POST: Diciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Dicipline dicipline = await db.Diciplines.FindAsync(id);
            db.Diciplines.Remove(dicipline);
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
