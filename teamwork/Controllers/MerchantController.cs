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
        

        
        public ActionResult Index()
        {
            return View();
        }
    }
}