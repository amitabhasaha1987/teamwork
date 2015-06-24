using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork.Controllers
{
    [AuthorizeAccess]
    public class MerchantController : Controller
    {
        // GET: Merchant
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                Session["role"] = UserType.Merchant;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(login);
            }
           
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}