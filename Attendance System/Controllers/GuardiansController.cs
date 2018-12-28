using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Mvc;
using Attendance_System.Models;
using Attendance_System.ViewModels;

namespace Attendance_System.Controllers
{
    public class GuardiansController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Guardians
        public async Task<ActionResult> Index()
        {
            return View(await db.Guardians.ToListAsync());
        }

        // GET: Guardians/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = await db.Guardians.FindAsync(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }
        
        // GET: Guardians/Create
        public ActionResult Create(string rollNo)
        {
            GuardianViewModel guardian = new GuardianViewModel();
            guardian.rollNo = rollNo;
            return View(guardian);
        }





        // POST: Guardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GuardianViewModel guardian)
        {
            if (ModelState.IsValid)
            {
                Guardian obj = new Guardian();
                obj.guardianName = guardian.name;
                obj.mobileNumber = guardian.mobileNumber;
                obj.relationship = guardian.relationship;
                db.Guardians.Add(obj);
                db.SaveChanges();
                int GuardianID = db.Guardians.Max(o => o.guardianID);
                Student std = db.Students.Find(guardian.rollNo);
                std.guardianID = GuardianID;
                db.Set<Student>().AddOrUpdate(std);
                db.SaveChanges();
                return RedirectToAction("index", "Students");
            }

            return View(guardian);
        }

        // GET: Guardians/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = await db.Guardians.FindAsync(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "guardianID,guardianName,relationship,mobileNumber")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guardian).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Students");
            }
            return View(guardian);
        }

        // GET: Guardians/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = await db.Guardians.FindAsync(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }

        // POST: Guardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Guardian guardian = await db.Guardians.FindAsync(id);
            db.Guardians.Remove(guardian);
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
