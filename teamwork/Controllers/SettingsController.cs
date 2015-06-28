using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;

namespace teamwork.Controllers
{
    [AuthorizeAccess(UserType.admin,UserType.Merchant)]
    public class SettingsController : BaseController
    {
        // GET: Settings
        //[HttpGet]
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
    }
}