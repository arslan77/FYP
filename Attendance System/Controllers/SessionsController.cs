﻿using System;
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
    public class SessionsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Sessions
        public async Task<ActionResult> Index()
        {
            var sessions = db.Sessions.Include(s => s.Dicipline);
            return View(await sessions.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            ViewBag.dicipID = new SelectList(db.Diciplines, "dicipID", "name");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "sessionID,name,dicipID,year")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.dicipID = new SelectList(db.Diciplines, "dicipID", "name", session.dicipID);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.dicipID = new SelectList(db.Diciplines, "dicipID", "name", session.dicipID);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "sessionID,name,dicipID,year")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.dicipID = new SelectList(db.Diciplines, "dicipID", "name", session.dicipID);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Session session = await db.Sessions.FindAsync(id);
            db.Sessions.Remove(session);
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