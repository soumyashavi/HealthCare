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
    public class fillinfoesController : Controller
    {
        private HealthCareEntities1 db = new HealthCareEntities1();

        // GET: fillinfoes
        public ActionResult Index()
        {
            return View(db.fillinfoes.ToList());
        }

        // GET: fillinfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fillinfo fillinfo = db.fillinfoes.Find(id);
            if (fillinfo == null)
            {
                return HttpNotFound();
            }
            return View(fillinfo);
        }

        // GET: fillinfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fillinfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,AdditionalInfo")] fillinfo fillinfo)
        {
            if (ModelState.IsValid)
            {
                db.fillinfoes.Add(fillinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fillinfo);
        }

        // GET: fillinfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fillinfo fillinfo = db.fillinfoes.Find(id);
            if (fillinfo == null)
            {
                return HttpNotFound();
            }
            return View(fillinfo);
        }

        // POST: fillinfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,AdditionalInfo")] fillinfo fillinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fillinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fillinfo);
        }

        // GET: fillinfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fillinfo fillinfo = db.fillinfoes.Find(id);
            if (fillinfo == null)
            {
                return HttpNotFound();
            }
            return View(fillinfo);
        }

        // POST: fillinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fillinfo fillinfo = db.fillinfoes.Find(id);
            db.fillinfoes.Remove(fillinfo);
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
