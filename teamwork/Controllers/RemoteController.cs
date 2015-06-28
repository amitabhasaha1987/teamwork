using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teamwork.Controllers
{
    public class RemoteController : BaseController
    {
        // GET: Remote
        public JsonResult IsMerchantExists(string username)
        {
            string user = HttpUtility.HtmlEncode(username);
            //bool exists = false;
            using(Context db = new Context())
            {
                var merchant = db.Merchants.FirstOrDefault(x => x.username == user);
                if (merchant != null)
                {
                    return Json("User name is not available..!", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}