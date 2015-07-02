using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teamwork.Controllers
{
    [AuthorizeAccess(UserType.admin)]
    public class TagController : Controller
    {

        public ActionResult Index()
        {
            using (Context db = new Context())
            {
                var tags = db.Tags.ToList();
                return View(tags);
            }
        }
        // GET: Tag
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                tag.created = DateTime.Now;
                tag.is_synched = 1;

                using (Context db = new Context())
                {
                    db.Tags.Add(tag);
                    db.SaveChanges();
                    TempData["Status"] = "1";
                    return RedirectToAction("Create");
                }
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            using (Context db = new Context())
            {
                var brand = db.Tags.Find(id);
                db.Tags.Remove(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}