using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;



namespace HealthCareproject.Controllers
{
    public class ContactusController : Controller
    {
        // GET: Contactus
        HealthCareEntities13 db = new HealthCareEntities13();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(contactu vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("soumyashavi87@gmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("soumyashavi87@gmail.com", "Sou@14shav");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting Us! ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                    //ViewBag.Message = $" Thank you for Contacing us {ex.Message}";
                }
            }

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Expert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Expert(contactu bk)
        {
            using (HealthCareEntities13 db = new HealthCareEntities13())

            {
                db.contactus.Add(bk);
                db.SaveChanges();
                int id = bk.id;
              





                ViewBag.Id = id;
                
            }
            return View();
        }



    }
}
    
