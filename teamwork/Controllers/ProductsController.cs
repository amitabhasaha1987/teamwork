using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace teamwork.Controllers
{

    [AuthorizeAccess(UserType.Merchant)]
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Add()
        {
            DataAccess.Models.Product product = new DataAccess.Models.Product();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Add(DataAccess.Models.Product product, string command)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    using (DataAccess.Context context = new DataAccess.Context())
                    {
                        product.sizes = product.SizeList.Where(x => x.IsSelected == true).ToList().Sum(s => (int)s.Size);
                        product.colors = product.ColorsList.Where(x => x.IsSelected == true).ToList().Sum(s=>(int)s.Color);
                        context.Products.Add(product);
                        context.SaveChanges();
                        TempData["Status"] = "1";
                        if(command == "2")
                        {
                            TempData["productId"] = product.id;
                            return RedirectToAction("ProductImage");
                        }
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            return View(product);
        }

        public ActionResult ProductImage()
        {
            return View();
        }
    }
}