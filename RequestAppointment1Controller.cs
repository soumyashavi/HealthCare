using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthCareproject.Models;

namespace HealthCareproject.Controllers
{
    public class RequestAppointment1Controller : Controller
    {
        private HealthCareEntities4 db = new HealthCareEntities4();

        // GET: RequestAppointment1
        public ActionResult Index()
        {
            return View(db.RequestAppointment1.ToList());
        }

        // GET: RequestAppointment1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment1 requestAppointment1 = db.RequestAppointment1.Find(id);
            if (requestAppointment1 == null)
            {
                return HttpNotFound();
            }
            return View(requestAppointment1);
        }

        // GET: RequestAppointment1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestAppointment1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,id,BookTime")] RequestAppointment1 requestAppointment1)
        {
            if (ModelState.IsValid)
            {
                db.RequestAppointment1.Add(requestAppointment1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestAppointment1);
        }

        // GET: RequestAppointment1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment1 requestAppointment1 = db.RequestAppointment1.Find(id);
            if (requestAppointment1 == null)
            {
                return HttpNotFound();
            }
            return View(requestAppointment1);
        }

        // POST: RequestAppointment1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,id,BookTime")] RequestAppointment1 requestAppointment1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestAppointment1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestAppointment1);
        }

        // GET: RequestAppointment1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment1 requestAppointment1 = db.RequestAppointment1.Find(id);
            if (requestAppointment1 == null)
            {
                return HttpNotFound();
            }
            return View(requestAppointment1);
        }

        // POST: RequestAppointment1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestAppointment1 requestAppointment1 = db.RequestAppointment1.Find(id);
            db.RequestAppointment1.Remove(requestAppointment1);
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
        public ActionResult message()
        {
            return View();
        }
    }
}
