using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;

namespace teamwork.Controllers
{

    [AuthorizeAccess(UserType.Merchant,UserType.admin)]
    public class FileUploadController : BaseController
    {
        // GET: FileUpload

        [HttpDelete]
        public async Task<JsonResult> Removeimage(Int64 id, string type)
        {
            if (type != "thumbnail")
            {
                using (Context db = new Context())
                {
                    var product_image = await db.Product_Images.FindAsync(id);
                    if (product_image != null)
                    {
                        string absolutePath = Server.MapPath(product_image.file_path);
                        if (System.IO.File.Exists(absolutePath))
                        {
                            System.IO.File.Delete(absolutePath);
                        }
                        db.Product_Images.Remove(product_image);
                        await db.SaveChangesAsync();
                    }
                    return Json(true);
                }
            }
            else if (type == "thumbnail")
            {
                using (Context db = new Context())
                {
                    var product = await db.Products.FindAsync(id);
                    if (product != null)
                    {
                        string absolutePath = Server.MapPath(product.thumbnail);
                        if (System.IO.File.Exists(absolutePath))
                        {
                            System.IO.File.Delete(absolutePath);
                        }
                        product.thumbnail = null;
                        await db.SaveChangesAsync();
                    }
                    return Json(true);
                }
            }
           

            return Json(false);

        }

        [HttpPost]
        public async Task<JsonResult> Uploadimage()
        {
            try
            {
                string ProductType = Request.Form["ProductType"];
                string Category  = Request.Form["Category"];
                string ProductId = Request.Form["ProductId"];
                string ColorCode = Request.Form["ColorCode"];

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileName = fileContent.FileName.Split('.')[0];
                        string ext = fileContent.FileName.Split('.')[1];
                        fileName = Guid.NewGuid().ToString() + fileName + "." + ext;
                        
                       
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        //var fileName = Path.GetFileName(file);

                        //Manipulate the directory path
                        string year = DateTime.Now.Year.ToString(); string month = DateTime.Now.Month.ToString();
                        string fileuploadpath = "\\upload\\product\\" + ProductType + "\\" + Category + "\\colors\\" + year + "\\" + month + "\\" + ProductId.ToString() + "\\" + ColorCode;
                        var path = Server.MapPath(fileuploadpath);

                        //Create directory if not exists
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //autogenerate file name 
                        //path is absolute path
                        path = Path.Combine(path, fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }


                        //relative path
                        string imageUrl = Path.Combine(fileuploadpath, fileName);
                        //Db Code
                        using(Context db = new Context())
                        {
                            Product_Images product_Images = new Product_Images();
                            product_Images.product_id = Convert.ToInt64(ProductId);
                            product_Images.color_code = Convert.ToInt32(ColorCode);
                            product_Images.file_path = imageUrl;
                            product_Images.extension = "." + ext;
                            product_Images.created = DateTime.Now;
                            product_Images.is_trial_image = 0;
                            product_Images.is_synched = 1;

                            db.Product_Images.Add(product_Images);
                            await db.SaveChangesAsync();

                            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                            jsonResult.Add("Html", RenderRazorViewToString("~/Views/Shared/ULImage.cshtml", product_Images));
                            return Json(jsonResult, JsonRequestBehavior.AllowGet);
                        }

                        
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }



        [HttpPost]
        public async Task<JsonResult> Uploadtrialimage()
        {
            try
            {
                string ProductType = Request.Form["ProductType"];
                string Category  = Request.Form["Category"];
                string ProductId = Request.Form["ProductId"];

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileName = fileContent.FileName.Split('.')[0];
                        string ext = fileContent.FileName.Split('.')[1];
                        fileName = Guid.NewGuid().ToString() + fileName + "." + ext;
                        
                       
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        //var fileName = Path.GetFileName(file);

                        //Manipulate the directory path
                        string year = DateTime.Now.Year.ToString(); string month = DateTime.Now.Month.ToString();
                        string fileuploadpath = "\\upload\\product\\" + ProductType + "\\" + Category + "\\trail\\" + year + "\\" + month + "\\" + ProductId.ToString(); 
                        var path = Server.MapPath(fileuploadpath);

                        //Create directory if not exists
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //autogenerate file name 
                        //path is absolute path
                        path = Path.Combine(path, fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }


                        //relative path
                        string imageUrl = Path.Combine(fileuploadpath, fileName);
                        //Db Code
                        using(Context db = new Context())
                        {
                            Product_Images product_Images = new Product_Images();
                            product_Images.product_id = Convert.ToInt64(ProductId);
                            
                            product_Images.file_path = imageUrl;
                            product_Images.extension = "." + ext;
                            product_Images.created = DateTime.Now;
                            product_Images.is_trial_image = 1;
                            product_Images.is_synched = 1;

                            db.Product_Images.Add(product_Images);
                            await db.SaveChangesAsync();

                            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                            jsonResult.Add("Html", RenderRazorViewToString("~/Views/Shared/ULImage.cshtml", product_Images));
                            return Json(jsonResult, JsonRequestBehavior.AllowGet);
                        }

                        
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }


        [HttpPost]
        public async Task<JsonResult> Uploadthumbnail()
        {
            try
            {
                string ProductType = Request.Form["ProductType"];
                string Category  = Request.Form["Category"];
                string ProductId = Request.Form["ProductId"];

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileName = fileContent.FileName.Split('.')[0];
                        string ext = fileContent.FileName.Split('.')[1];
                        fileName = Guid.NewGuid().ToString() + fileName + "." + ext;
                        
                       
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        //var fileName = Path.GetFileName(file);

                        //Manipulate the directory path
                        string year = DateTime.Now.Year.ToString(); string month = DateTime.Now.Month.ToString();
                        string fileuploadpath = "\\upload\\product\\" + ProductType + "\\" + Category + "\\thumbnails\\" + year + "\\" + month + "\\" + ProductId.ToString(); 
                        var path = Server.MapPath(fileuploadpath);

                        //Create directory if not exists
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //autogenerate file name 
                        //path is absolute path
                        path = Path.Combine(path, fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }


                        //relative path
                        string imageUrl = Path.Combine(fileuploadpath, fileName);
                        //Db Code
                        using(Context db = new Context())
                        {

                            var product = db.Products.Find(Convert.ToInt64(ProductId));
                            if(product != null)
                            {
                                product.thumbnail = imageUrl;
                                await db.SaveChangesAsync();
                            }
                            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                            jsonResult.Add("Html", RenderRazorViewToString("~/Views/Shared/ULThumbnailImage.cshtml", product));
                            return Json(jsonResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }
        
        
    }
}