using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;
using DataAccess;
using DataAccess.Models;
using System.Net.Mail;
using teamwork.Utility;

namespace teamwork.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            /*
                 catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                 {
                     Exception raise = dbEx;
                     foreach (var validationErrors in dbEx.EntityValidationErrors)
                     {
                         foreach (var validationError in validationErrors.ValidationErrors)
                         {
                             string message = string.Format("{0}:{1}",
                                 validationErrors.Entry.Entity.ToString(),
                                 validationError.ErrorMessage);
                             // raise a new exception nesting  
                             // the current instance as InnerException  
                             raise = new InvalidOperationException(message, raise);
                         }
                     }
                     throw raise;
                 }*/


            Login login = new Login();
            //if (Request.Cookies["username"] != null)
            //    login.username = Convert.ToString(Request.Cookies["username"].Value);

            //if (Request.Cookies["passowrd"] != null)
            //    login.username = Convert.ToString(Request.Cookies["passowrd"].Value);
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {

                using (Context db = new Context())
                {
                    Merchant merchant = db.Merchants.FirstOrDefault(x => x.username == login.username);

                    if (merchant != null)
                    {
                        if (login.RememberMe)
                        {
                            /*
                            HttpCookie cookie = new HttpCookie("username", login.username);
                            //cookie["username"] = login.username;
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);

                            cookie["passowrd"] = login.password;
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);
                             * */
                        }

                        Session["username"] = merchant.username;
                        if (merchant.super_user == UserType.Merchant)
                        {
                            Session["role"] = (int)UserType.Merchant;

                            return RedirectToAction("Index", "Merchant");
                        }
                        else
                        {
                            Session["role"] = (int)UserType.admin;
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(login);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(login);
            }

        }


        [HttpPost]
        public JsonResult SendEmail(string email)
        {
                using (Context db = new Context())
                {
                    var merchant = db.Merchants.FirstOrDefault(u => u.contact_email == email);
                    if (merchant != null)
                    {
                        string body = "Hi <b>" + merchant.incharge_name + "</b>,<br/>" + "Your Password is <b>" + merchant.password + "<b>.<br> Thanks and Regards <br> Admin.";
                        
                        string subject = " Forget Password";

                        return Json(teamwork.Utility.Mailer.SendMail(subject, body, email) == true ? "YOUR PASSWORD SUCCESSFULLY SEND TO YOUE MAIL" : "Recovery failed, try again later.");
                    }
                    else
                    {
                        return Json("INVALID EMAIL ADDRESS");
                    }
                }
        }
    }
}