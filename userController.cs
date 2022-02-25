using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareproject.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;


namespace HealthCareproject.Controllers
{
    public class userController : Controller
    {
        // GET: user
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")]  NewForgotPaasword user)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                var isexist = IsEmailExist(user.EmailId);
                if (isexist)
                {
                    ModelState.AddModelError("Email Exist", "Email already exists");
                   

                }

                user.ActivationCode = Guid.NewGuid();

                user.Paasword = Crypto.Hash(user.Paasword);
                //  user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                user.IsEmailVerified = false;
                using (HealthCareEntities dc = new HealthCareEntities())
                {
                    dc.NewForgotPaaswords.Add(user);
                    dc.SaveChanges();
                    SendVerificationLinkEmail(user.EmailId, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + user.EmailId;
                    Status = true;

                }
            }
            else
            {
                message = "invalid request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);


        }


        [NonAction]
        public bool IsEmailExist(string emailid)
        {
            using (HealthCareEntities dc = new HealthCareEntities())
            {
                var v = dc.NewForgotPaaswords.Where(a => a.EmailId == emailid).FirstOrDefault();
                return v != null;
            }



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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (HealthCareEntities dc = new HealthCareEntities())
            {
                var v = dc.NewForgotPaaswords.Where(a => a.EmailId == login.EmailId).FirstOrDefault();
                if (v != null)
                {
                    //if (!v.IsEmailVerified)
                    //{
                    //    ViewBag.Message = "Please verify your email first";
                    //    return View();
                    //}
                    if (string.Compare(Crypto.Hash(login.Password), v.Paasword) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailId, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index1", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "user");
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
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
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





        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
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
                    SendVerificationLinkEmail(account.EmailId, resetCode, "ResetPassword");
                    account.ResetPassword = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (HealthCareEntities dc = new HealthCareEntities())
            {
                var user = dc.NewForgotPaaswords.Where(a => a.ResetPassword == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (HealthCareEntities dc = new HealthCareEntities())
                {
                    var user = dc.NewForgotPaaswords.Where(a => a.ResetPassword == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Paasword = Crypto.Hash(model.NewPassword);
                        user.ResetPassword = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(adminPage tb1)
        {
            HealthCareEntities5 db1 = new HealthCareEntities5();
            var check = db1.adminPages.Where(x => x.Name.Equals(tb1.Name) && x.Password.Equals(tb1.Password)).FirstOrDefault();
            if (check != null)
            {
                Session["Username"] = tb1.Name.ToString();
                Session["Password"] = tb1.Password.ToString();
                ViewBag.name = Session["Username"];

                return RedirectToAction("index");
            }
            else
            {
                Response.Write("Invalid Username or password!!!");
            }

            return View();

        }
        public ActionResult welcomeadmin()
        {
            return View();
        }



    }
    }





