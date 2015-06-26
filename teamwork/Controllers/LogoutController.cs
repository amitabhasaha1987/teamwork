using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
            
        }
    }
}