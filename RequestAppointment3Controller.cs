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
    public class RequestAppointment3Controller : Controller
    {
        private HealthCareEntities7 db = new HealthCareEntities7();

        // GET: RequestAppointment3
        public ActionResult Index()
        {
            var requestAppointment3 = db.RequestAppointment3.Include(r => r.NewForgotPaasword);
            return View(requestAppointment3.ToList());
        }

        // GET: RequestAppointment3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment3 requestAppointment3 = db.RequestAppointment3.Find(id);
            if (requestAppointment3 == null)
            {
                return HttpNotFound();
            }
            return View(requestAppointment3);
        }

        // GET: RequestAppointment3/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.NewForgotPaaswords, "MemberId", "MemberName");
            return View();
        }

        // POST: RequestAppointment3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,patientid,id,BookTime")] RequestAppointment3 requestAppointment3)
        {
            if (ModelState.IsValid)
            {
                db.RequestAppointment3.Add(requestAppointment3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.NewForgotPaaswords, "MemberId", "MemberName", requestAppointment3.id);
            return View(requestAppointment3);
        }

        // GET: RequestAppointment3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment3 requestAppointment3 = db.RequestAppointment3.Find(id);
            if (requestAppointment3 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.NewForgotPaaswords, "MemberId", "MemberName", requestAppointment3.id);
            return View(requestAppointment3);
        }

        // POST: RequestAppointment3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,patientid,id,BookTime")] RequestAppointment3 requestAppointment3)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestAppointment3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.NewForgotPaaswords, "MemberId", "MemberName", requestAppointment3.id);
            return View(requestAppointment3);
        }

        // GET: RequestAppointment3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestAppointment3 requestAppointment3 = db.RequestAppointment3.Find(id);
            if (requestAppointment3 == null)
            {
                return HttpNotFound();
            }
            return View(requestAppointment3);
        }

        // POST: RequestAppointment3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestAppointment3 requestAppointment3 = db.RequestAppointment3.Find(id);
            db.RequestAppointment3.Remove(requestAppointment3);
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
