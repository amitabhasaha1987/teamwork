using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork.Controllers
{

    public class MerchantController : BaseController
    {
        // GET: Merchant


        [AuthorizeAccess(UserType.Merchant)]
        public ActionResult Index()
        {
            return View();
        }


        [AuthorizeAccess(UserType.admin)]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [AuthorizeAccess(UserType.admin)]
        [HttpGet]
        public ActionResult Details()
        {
            using(Context db = new Context())
            {
                var merchantList = db.Merchants.ToList();
                return View(merchantList);
            }
            
        }

        [AuthorizeAccess(UserType.admin)]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (Context db = new Context())
            {
                var merchant = db.Merchants.Find(id);
                db.Merchants.Remove(merchant);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

        }

        [AuthorizeAccess(UserType.admin)]
        [HttpPost]
        public ActionResult Create(Merchant merchant, string command)
        {
            if(ModelState.IsValid)
            {
                //assigning default values
                merchant.super_user = 0;
                merchant.created = DateTime.Now;
                merchant.status = 1;
                merchant.is_synched = 1;

            
                using(Context db = new Context())
                {
                    db.Merchants.Add(merchant);
                    db.SaveChanges();
                }

                if(command == "2")//send email
                {
                    string body = "Hi <b>" + merchant.merchant_name + "</b>,<br/>" + "Your account is successfully created with UserName - <b>"+ merchant.username + "</b>, password - " + " <b>" + merchant.password + "</b>.<br/> Thanks and Regards <br> Admin.";

                    string subject = "New Account is created for Stylistics.";
                    string appDataPath = Server.MapPath("~/app_data");
                    teamwork.Utility.Mailer.SendMail(subject, body, merchant.contact_email, appDataPath);
                }

                TempData["Status"] = "1";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using(Context db = new Context())
            {
                var merchant = db.Merchants.Find(id);
                if (merchant != null)
                {
                    return View(merchant);
                }
                else
                {
                    return RedirectToAction("Details");
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(Merchant merchant)
        {
            if(ModelState.IsValid)
            {
                using (Context db = new Context())
                {
                    var M = db.Merchants.Find(merchant.id);
                    M = merchant;
                    M.modified = DateTime.Now;
                    TempData["Status"] = "1";

                    return View();
                    
                }
            }
            return View();
        }

    }
}