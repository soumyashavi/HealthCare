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
    public class appoinmentrequestnewController : Controller
    {
        private HealthCareEntities7 db = new HealthCareEntities7();

        // GET: appoinmentrequestnew
        public ActionResult Index()
        {
            var requestAppointment3 = db.RequestAppointment3.Include(r => r.NewForgotPaasword);
            return View(requestAppointment3.ToList());
        }

        // GET: appoinmentrequestnew/Details/5
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

        // GET: appoinmentrequestnew/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.NewForgotPaaswords, "MemberId", "MemberName");
            return View();
        }

        // POST: appoinmentrequestnew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,patientid,id,BookTime,EmailId")] RequestAppointment3 requestAppointment3)
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

        // GET: appoinmentrequestnew/Edit/5
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

        // POST: appoinmentrequestnew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Age,Gender,Diseases,Symptoms,AffectedArea,patientid,id,BookTime,EmailId")] RequestAppointment3 requestAppointment3)
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

        // GET: appoinmentrequestnew/Delete/5
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

        // POST: appoinmentrequestnew/Delete/5
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
        //public ActionResult Message()
        //{
        //    return View();
        //}
        public ActionResult Welcome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Welcome(RequestAppointment3 bk)
        {
            using (HealthCareEntities7 db = new HealthCareEntities7()) 

            {
                db.RequestAppointment3.Add(bk);
                db.SaveChanges();
                int id = bk.patientid;
                string name = bk.EmailId;
               DateTime dt = bk.BookTime ?? DateTime.Now;





                ViewBag.Id = id;
                ViewBag.Name = name;
                ViewBag.dt = dt;
            }

            return View();
        }



        public ActionResult payment()

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
                subject = "Bookinng Confirmation";
                  body = "Hi,<br/>br/>Your appointment has been Booked!";  
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
                body = "Hi,<br/>br/>Sorry! Your appointment has been Cancelled,due to unavailbility of doctor..Thanks for visting our Page!"; 
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