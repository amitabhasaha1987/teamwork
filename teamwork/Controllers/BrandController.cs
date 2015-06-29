using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess;

namespace teamwork.Controllers
{
    [AuthorizeAccess(UserType.admin)]
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if(ModelState.IsValid)
            {
                brand.created = DateTime.Now;
                brand.is_synched = 1;

                using(Context db = new Context())
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    TempData["Status"] = "1";
                    return RedirectToAction("Create");
                }
            }
            return View();
        }
    }
}