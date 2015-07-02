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

        public ActionResult Index()
        {
            using(Context db = new Context())
            {
                var brands = db.Brands.ToList();
                return View(brands);
            }
        }

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

        public ActionResult Delete(int id)
        {
            using(Context db = new Context())
            {
                var brand = db.Brands.Find(id);
                db.Brands.Remove(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}