using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthCareproject.Models;
using System.Net.Mail;

namespace HealthCareproject.Controllers
{
    public class adminPagesController : Controller
    {
        private HealthCareEntities5 db = new HealthCareEntities5();

        // GET: adminPages
        public ActionResult Index()
        {
            return View(db.adminPages.ToList());
        }

        // GET: adminPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminPage adminPage = db.adminPages.Find(id);
            if (adminPage == null)
            {
                return HttpNotFound();
            }
            return View(adminPage);
        }

        // GET: adminPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Password")] adminPage adminPage)
        {
            if (ModelState.IsValid)
            {
                db.adminPages.Add(adminPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminPage);
        }

        // GET: adminPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminPage adminPage = db.adminPages.Find(id);
            if (adminPage == null)
            {
                return HttpNotFound();
            }
            return View(adminPage);
        }

        // POST: adminPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Password")] adminPage adminPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminPage);
        }

        // GET: adminPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminPage adminPage = db.adminPages.Find(id);
            if (adminPage == null)
            {
                return HttpNotFound();
            }
            return View(adminPage);
        }

        // POST: adminPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adminPage adminPage = db.adminPages.Find(id);
            db.adminPages.Remove(adminPage);
            db.SaveChanges();
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
    
