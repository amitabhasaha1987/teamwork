using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using teamwork.Models;
using System.Configuration;
using System.Web.Configuration;
using System.IO;
using System.Xml.Linq;

namespace teamwork.Controllers
{
    [AuthorizeAccess(UserType.admin,UserType.Merchant)]
    public class SettingsController : BaseController
    {
        // GET: Settings
        //[HttpGet]
        [AuthorizeAccess(UserType.admin, UserType.Merchant)]
        public ActionResult Profile()
        {
            string username = Convert.ToString(Session["username"]);
            using(Context db =new Context())
            {
                Merchant merchat = db.Merchants.FirstOrDefault(m => m.username == username);
                merchat.ConfirmPassword = merchat.password;
                return View(merchat);
            }
        }

        
        [AuthorizeAccess(UserType.admin, UserType.Merchant)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Profile(Merchant merchant)
        {
            if(ModelState.IsValid)
            {
                using (Context db = new Context())
                {
                    
                        Merchant _merchat = db.Merchants.Find(merchant.id);
                        _merchat.contact_email = merchant.contact_email;
                        _merchat.url = merchant.url;
                        _merchat.contact_phone = merchant.contact_phone;
                        _merchat.contact_fax = merchant.contact_fax;
                        _merchat.password = merchant.password;
                        _merchat.ConfirmPassword = merchant.ConfirmPassword;
                        db.Entry(_merchat).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Status = "1";
                        return View(_merchat);
                    
                }
            }
            return View(merchant);
        }

        [AuthorizeAccess(UserType.admin)]
        public ActionResult AdminEmailCredential()
        {
            string appDataPath = Server.MapPath("~/app_data");
            string file = Path.Combine(appDataPath + "\\EmailCredential.xml");
            XElement ele = XElement.Load(file);

            AdminEmail adminemail = new AdminEmail();
            adminemail.FormEmail = ele.Element("email").Value;
            adminemail.password = ele.Element("passowrd").Value;
            adminemail.SMTP = ele.Element("smtp").Value;
            adminemail.Port = ele.Element("port").Value;

            return View(adminemail);
        }

        [AuthorizeAccess(UserType.admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult AdminEmailCredential(AdminEmail adminemail)
        {
            string appDataPath = Server.MapPath("~/app_data");
            string file = Path.Combine(appDataPath + "\\EmailCredential.xml");
            XElement ele = XElement.Load(file);

            ele.Element("email").SetValue(adminemail.FormEmail);
            ele.Element("passowrd").SetValue(adminemail.password);
            ele.Element("smtp").SetValue(adminemail.SMTP);
            ele.Element("port").SetValue(adminemail.Port);
            ele.Save(file);
            TempData["Status"] = "1";
            return RedirectToAction("AdminEmailCredential");
        }
    }
}