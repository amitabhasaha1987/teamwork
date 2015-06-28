using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teamwork.Controllers
{
    [AuthorizeAccess(UserType.admin,UserType.Merchant)]
    public class UnauthorizedController : BaseController
    {
        // GET: Unauthorized
        public ActionResult Access()
        {
            return View();
        }
    }
}