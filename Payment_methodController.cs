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
    public class Payment_methodController : Controller
    {
        private HealthCareEntities9 db = new HealthCareEntities9();

        // GET: Payment_method
        public ActionResult Index()
        {
            return View(db.Payment_method.ToList());
        }

        // GET: Payment_method/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // GET: Payment_method/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment_method/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NameOnCard,CreditCardNumber,ExpMonth,CVV,ExpYear")] Payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.Payment_method.Add(payment_method);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment_method);
        }

        // GET: Payment_method/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // POST: Payment_method/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NameOnCard,CreditCardNumber,ExpMonth,CVV,ExpYear")] Payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_method).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment_method);
        }

        // GET: Payment_method/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // POST: Payment_method/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_method payment_method = db.Payment_method.Find(id);
            db.Payment_method.Remove(payment_method);
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

        public ActionResult upi()
        {
            return View();
        }

        public ActionResult message()
        {
            return View();
        }
        [HttpPost]
        public ActionResult message(Payment_method p)
        {
            using (HealthCareEntities9 db = new HealthCareEntities9())
            {
                db.Payment_method.Add(p);
                db.SaveChanges();
            }
                return View();
        }
    }
}
