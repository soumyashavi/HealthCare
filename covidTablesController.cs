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
    public class covidTablesController : Controller
    {
        private HealthCareEntities10 db = new HealthCareEntities10();

        // GET: covidTables
        public ActionResult Index()
        {
            return View(db.covidTables.ToList());
        }

        // GET: covidTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            covidTable covidTable = db.covidTables.Find(id);
            if (covidTable == null)
            {
                return HttpNotFound();
            }
            return View(covidTable);
        }

        // GET: covidTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: covidTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,AdharCard,EmailId,MobileNumber,Place,BookDateandTime")] covidTable covidTable)
        {
            if (ModelState.IsValid)
            {
                db.covidTables.Add(covidTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(covidTable);
        }

        // GET: covidTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            covidTable covidTable = db.covidTables.Find(id);
            if (covidTable == null)
            {
                return HttpNotFound();
            }
            return View(covidTable);
        }

        // POST: covidTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,AdharCard,EmailId,MobileNumber,Place,BookDateandTime")] covidTable covidTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(covidTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(covidTable);
        }

        // GET: covidTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            covidTable covidTable = db.covidTables.Find(id);
            if (covidTable == null)
            {
                return HttpNotFound();
            }
            return View(covidTable);
        }

        // POST: covidTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            covidTable covidTable = db.covidTables.Find(id);
            db.covidTables.Remove(covidTable);
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
        public ActionResult Message()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Message(covidTable tb)
        {
            using (HealthCareEntities10 db = new HealthCareEntities10())
            {
                db.covidTables.Add(tb);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult payment()
        {
            return View();
        }
        public ActionResult vaccine()
        {
            return View();
        }



        [HttpGet]
        public ActionResult sendmsg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sendmsg(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            //   bool status = false;

            using (HealthCareEntities dc = new HealthCareEntities())
            {
                var account = dc.NewForgotPaaswords.Where(a => a.EmailId == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EmailId, resetCode, "Confirmation");
                    account.ResetPassword = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "appointment confirmation Link has been sent to user.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }











        public ActionResult Confirmation(string id)
        {
            return View();
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            //if (string.IsNullOrWhiteSpace(id))
            //{
            //    return HttpNotFound();
            //}

            //using (HealthCareEntities dc = new HealthCareEntities())
            //{
            //    var user = dc.NewForgotPaaswords.Where(a => a.ResetPassword == id).FirstOrDefault();
            //    if (user != null)
            //    {
            //        ResetPasswordModel model = new ResetPasswordModel();
            //        model.ResetCode = id;
            //        return View(model);
            //    }
            //    else
            //    {
            //        return HttpNotFound();
            //    }
            //}
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmation()
        {
            //var message = "";
            //if (ModelState.IsValid)
            //{
            //    using (HealthCareEntities dc = new HealthCareEntities())
            //    {
            //        var user = dc.NewForgotPaaswords.Where(a => a.ResetPassword == model.ResetCode).FirstOrDefault();
            //        if (user != null)
            //        {
            //            user.Paasword = Crypto.Hash(model.NewPassword);
            //            user.ResetPassword = "";
            //            dc.Configuration.ValidateOnSaveEnabled = false;
            //            dc.SaveChanges();
            //            message = "New password updated successfully";
            //        }
            //    }
            //}
            //else
            //{
            //    message = "Something invalid";
            //}
            //ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)

        {

            bool Status = false;
            using (HealthCareEntities dc = new HealthCareEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = dc.NewForgotPaaswords.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/user/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("soumyashavi87@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Sou@14shav"; // Replace with actual password

            //string subject = "Your account is successfully created!";
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your apoointment booked!";
                body = "<br/><br/>We are excited to tell you that your account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "Confirmation")
            {
                subject = "Booking Confirmation";
                body = "Hi,<br/>br/>Your slots has been Booked for vaccination!";
                //"Please click on the below link" +
                //    "<br/><br/><a href=" + link + ">Payment  link</a>";
            }

            //string body = "<br/><br/>We are excited to tell you that your account is" +
            //    " successfully Registered. Please click on the below link to verify your account" +
            //    " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            //using (var message = new MailMessage(fromEmail, toEmail)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //    smtp.Send(message);
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }




        //cancel

        [HttpGet]
        public ActionResult cancel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cancel(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            //   bool status = false;

            using (HealthCareEntities dc = new HealthCareEntities())
            {
                var account = dc.NewForgotPaaswords.Where(a => a.EmailId == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail1(account.EmailId, resetCode, "ResetPassword");
                    account.ResetPassword = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "appointment cancellation mail has been sent to user.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }





        [NonAction]
        public void SendVerificationLinkEmail1(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/user/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("soumyashavi87@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Sou@14shav"; // Replace with actual password

            //string subject = "Your account is successfully created!";
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your apoointment booked!";
                body = "<br/><br/>We are excited to tell you that your account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Cancelled";
                body = "Hi,<br/>br/>Sorry! Your slots has been Cancelled,due to unavailbility ...Thanks for visting our Page!";
                //" Please click on the below link" +
                //  "<br/><br/><a href=" + link + ">Payment  link</a>";
            }

            //string body = "<br/><br/>We are excited to tell you that your account is" +
            //    " successfully Registered. Please click on the below link to verify your account" +
            //    " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            //using (var message = new MailMessage(fromEmail, toEmail)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //    smtp.Send(message);
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }



    }
}
