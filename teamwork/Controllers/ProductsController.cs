using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace teamwork.Controllers
{

    [RoutePrefix("Products")]
    [AuthorizeAccess(UserType.Merchant)]
    public class ProductsController : BaseController
    {

        //List Product
        public ActionResult Index()
        {
            List<DataAccess.Models.Product> productViewModel = new List<DataAccess.Models.Product>();
            UserType userrole = (UserType)(Convert.ToByte(System.Web.HttpContext.Current.Session["role"]));

            using(Context db = new Context())
            {
                string username = (string)Session["username"];

                DataAccess.Models.Merchant merchant = db.Merchants.FirstOrDefault(x => x.username == username);

                (from p in db.Products
                    join b in db.Brands
                    on p.brand_id equals b.id
                    //where (userrole == UserType.Merchant ? (p.merchant_id == merchant.id) : (1 == 1))
                    select new
                    {
                        p,
                        b.name
                    }).OrderByDescending(x=>x.p.id).ToList()
                    .ForEach(x => 
                    {
                        x.p.BrandName = x.name;
                        productViewModel.Add(x.p);
                    });

                foreach (var item in productViewModel)
                {
                    long value = Convert.ToInt64(item.colors);
                    long[] colors = Utility.BitwiseAnd.getFectors(value);

                    foreach (var c in colors)
                    {
                        DataAccess.Models.enumColor enumcolor = (DataAccess.Models.enumColor)c;
                        item.ColorsList.ForEach(x => 
                        { 
                            if(x.Color.Equals(enumcolor))
                            {
                                x.IsSelected = true;
                            }
                        });
                    }
                }

            }

            return View(productViewModel);
        }
        // GET: Products
        public ActionResult Add()
        {
            DataAccess.Models.Product product = new DataAccess.Models.Product();

            using (Context db = new Context())
            {
                ViewData["Brands"] =   db.Brands.ToDictionary(i => i.id, i => i.name)
                                       .Select(x =>
                                                    new SelectListItem
                                                    {
                                                        Text = x.Value,
                                                        Value = x.Key.ToString()
                                                    })
                                       .ToList();
                
            }

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
                    using (Context context = new DataAccess.Context())
                    {
                        string username = (string)Session["username"];

                        DataAccess.Models.Merchant merchant = context.Merchants.FirstOrDefault(x => x.username == username);

                        product.sizes = product.SizeList.Where(x => x.IsSelected == true).ToList().Sum(s => (int)s.Size);
                        product.colors = product.ColorsList.Where(x => x.IsSelected == true).ToList().Sum(s=>(int)s.Color);
                        product.merchant_id = merchant.id;
                        context.Products.Add(product);
                        context.SaveChanges();
                        
                        if(command == "2")
                        {
                            return RedirectToAction("ProductImage","Products", new { id = product.id, type = Enum.GetName(typeof(DataAccess.Models.ProductType), product.type), category = Enum.GetName(typeof(DataAccess.Models.Category), product.category) });
                        }

                        //confirmation for save done
                        TempData["Status"] = "1";
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            return View(product);
        }


        public ActionResult Edit(int id)
        {
            DataAccess.Models.Product product = new DataAccess.Models.Product();

            using (Context db = new Context())
            {
                ViewData["Brands"] = db.Brands.ToDictionary(i => i.id, i => i.name)
                                       .Select(x =>
                                                    new SelectListItem
                                                    {
                                                        Text = x.Value,
                                                        Value = x.Key.ToString()
                                                    })
                                       .ToList();

                product = db.Products.Find(id);

                //Set Size
                long sizevalue = Convert.ToInt64(product.sizes);
                long[] sizearray = Utility.BitwiseAnd.getFectors(sizevalue);

                foreach (var s in sizearray)
                {
                    DataAccess.Models.enumSize enumsize = (DataAccess.Models.enumSize)s;
                    product.SizeList.ForEach(x =>
                    {
                        if (x.Size.Equals(enumsize))
                        {
                            x.IsSelected = true;
                        }
                    });
                }

                //set color
                long colorvalue = Convert.ToInt64(product.colors);
                long[] colorsarray = Utility.BitwiseAnd.getFectors(colorvalue);

                foreach (var c in colorsarray)
                {
                    DataAccess.Models.enumColor enumcolor = (DataAccess.Models.enumColor)c;
                    product.ColorsList.ForEach(x =>
                    {
                        if (x.Color.Equals(enumcolor))
                        {
                            x.IsSelected = true;
                        }
                    });
                }
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Edit(DataAccess.Models.Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Context db = new DataAccess.Context())
                    {
                        //var entity = db.Products.Find(product.id);

                        product.sizes = product.SizeList.Where(x => x.IsSelected == true).ToList().Sum(s => (int)s.Size);
                        product.colors = product.ColorsList.Where(x => x.IsSelected == true).ToList().Sum(s => (int)s.Color);
                        product.modified = DateTime.Now;
                        //entity = product;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();

                        //confirmation for save done
                        TempData["Status"] = "1";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View(product);
        }



        [Route("{id:int}/Image/{edit?}/{type}/{category}", Name = "EditImage")]
        [Route("{id:int}/Image/{type}/{category}", Name = "ImageAdd")]
        public ActionResult ProductImage(Int64 id, string edit,string type, string category)
        {

            ViewDataDictionary data = new ViewDataDictionary 
             { 
                 new KeyValuePair<string, object>("id", id), 
                 new KeyValuePair<string, object>("type", type.ToLower()),
                 new KeyValuePair<string, object>("category", category.ToLower()) 
             };

           if(edit != null)
           {
               data.Add(new KeyValuePair<string,object>("edit",true));
           }
            

            //var data = new ViewDataDictionary 
            // { 
            //     new KeyValuePair<string, object>("id", 1), 
            //     new KeyValuePair<string, object>("type", "MEN"),
            //     new KeyValuePair<string, object>("category", "TOP") 
            // };
            return View(data);
        }


        public ActionResult RelatedProducts()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProductsByCategory(int category)
        {
            using(Context db = new Context())
            {
                var prod = (from p in db.Products
                            where p.category.Equals(category)
                            select new
                            {
                                p.id,
                                p.name
                            }).ToList();
                return Json(prod);       
            }
        }

        [HttpPost]
        public JsonResult AddRelatedProduct(int productid, int relatedprodid, string relatedProdName)
        {
            UserType userrole = (UserType)(Convert.ToByte(System.Web.HttpContext.Current.Session["role"]));

            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            

            if(userrole == UserType.Merchant)
            {
                string username = (string)Session["username"];
                using(Context db = new Context())
                {
                    Merchant merchant = db.Merchants.FirstOrDefault(x => x.username == username);
                    Related_Products related_Products = new Related_Products();
                    related_Products.merchant_id = merchant.id;
                    related_Products.primary_product_id = productid;
                    related_Products.related_product_id = relatedprodid;
                    related_Products.is_synched = 1;
                    related_Products.created = DateTime.Now;

                    db.Related_Products.Add(related_Products);
                    db.SaveChanges();

                    ViewDataDictionary data = new ViewDataDictionary 
                    { 
                        new KeyValuePair<string, object>("ProductName", relatedProdName), 
                        new KeyValuePair<string, object>("RelatedProductId", related_Products.id)
                    };

                    jsonResult.Add("Html", RenderRazorViewToString("~/Views/Shared/_RelatedProduct.cshtml", data));
                    jsonResult.Add("success",1);
                    return Json(jsonResult, JsonRequestBehavior.AllowGet);
                }
                    
            }
            else if(userrole == UserType.admin)
            {

            }

            jsonResult.Add("success", 0);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult removeRelatedProduct(int relatedProdMapid)
        {
            using (Context db = new Context())
            {
                Related_Products related_Products = db.Related_Products.Find(relatedProdMapid);
                if(related_Products != null)
                {
                    db.Related_Products.Remove(related_Products);
                    db.SaveChanges();
                    return Json(1);
                }
            }
            return Json(0);
        }

        [HttpPost]
        public JsonResult LoadRelatedProduct(int prodId)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            using (Context db = new Context())
            {
                var related_Products = (from r in db.Related_Products
                                       join p in db.Products
                                       on r.related_product_id equals p.id
                                       where r.primary_product_id == prodId
                                       select new
                                       {
                                           r.id,
                                           p.name
                                       }).ToList();

                if (related_Products != null)
                {
                    
                    foreach (var item in related_Products)
                    {

                        ViewDataDictionary data = new ViewDataDictionary 
                        { 
                            new KeyValuePair<string, object>("ProductName", item.name), 
                            new KeyValuePair<string, object>("RelatedProductId", item.id)
                        };
                        sb.Append(RenderRazorViewToString("~/Views/Shared/_RelatedProduct.cshtml", data));
                    }
                }
            }
            jsonResult.Add("Html", sb.ToString());
            return Json(jsonResult);
        }
    }
}