using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teamwork.Models;

namespace teamwork.Controllers
{

    [AuthorizeAccess(UserType.admin)]
    public class AdminController : BaseController
    {
        // GET: Admin

        
        public ActionResult Index()
        {
            return View();
        }


    }
}