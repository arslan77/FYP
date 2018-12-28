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
using Attendance_System.ViewModels;
using Attendance_System.Utility;
using Attendance_System.Attributes;

namespace Attendance_System.Controllers
{
    [AuthorizeUser(Role = "admin")]
    public class ClassesController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Classes
        public async Task<ActionResult> Index()
        {
            var classes = db.Classes.Include(a => a.Section).Include(a => a.Session);
            return View(await classes.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
           // ViewBag.sectionID = new SelectList(db.Sections, "sectionID", "name");
            
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name");
            DataManager dm = new DataManager();
            var classes = new NewClass { Students = dm.Populatestudentdata() };
            return View(classes);
        }
        public JsonResult getSection(int? sessionID)
        {

           // List<String> obj = new List<string>();
            
            if (sessionID.HasValue)
            {
              var query = from section in db.Sections
                        where section.sessionID == sessionID
                        select new { section.name, section.sectionID };
                //foreach (var item in list)
                //{
                //    obj.Add(item.name);
                //}

              return Json(query, JsonRequestBehavior.AllowGet);
            }
            else

                return Json(null, JsonRequestBehavior.AllowGet);
        }
        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewClass obj)
        {
            if (ModelState.IsValid)
            {
                Class @class = new Class();
                try { 
                @class.sectionID = int.Parse( Request.Form["section"].ToString());
                    }
                catch (Exception)
                {
                    //@class.sectionID = -1;
                }
                @class.isMorning = obj.isMorning;
                @class.isSupply = obj.isSupply;
                @class.isTheory = obj.isTheory;
                @class.name = obj.name;
                @class.sessionID = obj.sessionID;
                //foreach (var item in obj.Students)
                //{
                //    if (item.Assigned)
                //    {
                //        @class.Students.Add(db.Students.Find(item.rollno));
                //    }
                //}

                db.Classes.Add(@class);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.sectionID = new SelectList(db.Sections, "sectionID", "name", obj.sectionID);
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", obj.sessionID);
            return View(obj);
        }

        // GET: Classes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.sectionID = new SelectList(db.Sections, "sectionID", "name", @class.sectionID);
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", @class.sessionID);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "classID,name,sectionID,sessionID,isMorning,isTheory,isSupply")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.sectionID = new SelectList(db.Sections, "sectionID", "name", @class.sectionID);
            ViewBag.sessionID = new SelectList(db.Sessions, "sessionID", "name", @class.sessionID);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            db.Classes.Remove(@class);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public JsonResult getStudents(int? sessionID)
        {
            if (sessionID.HasValue)
            {
                List<StudentInformation> students = new List<StudentInformation>();
                DataManager dm = new DataManager();
                students = dm.getStudentsFromSession(sessionID);
                //foreach (var item in list)
                //{
                //    obj.Add(item.name);
                //}

                return Json(students, JsonRequestBehavior.AllowGet);
            }
            else

                return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddStudents(int id)
        {
            
            ViewBag.classID = id;
            List<Student> list = db.Students.ToList();
            
            return View(list);
        }

        [HttpPost]
        public ActionResult AddStudents(AddStudents obj)
        {
            
           Class @class = db.Classes.Find(obj.classID);
           foreach (String student in obj.AssignedStudent)
           {
               if (student == "false")
               {

               }
               else
               {
                   @class.Students.Add(db.Students.Find(student));
               }
           }
           db.SaveChanges();



            return RedirectToAction("index");
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
