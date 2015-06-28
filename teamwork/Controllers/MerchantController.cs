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

                    teamwork.Utility.Mailer.SendMail(subject, body, merchant.contact_email);
                }

                TempData["Status"] = "1";
            }
            return View();
        }

    }
}