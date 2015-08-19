using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcForumapp.Controllers
{
    public class displayImageController : Controller
    {
        //
        // GET: /displayImage/
        newForumDBEntities db = new newForumDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowImage()
        {
            string user = Session["UserName"] as string;
            byte[] photo = null;
            var v = db.tblUsers.Where(p => p.UserName == user).Select(img => img.Photo).FirstOrDefault();
            photo = v;
            return File(photo, "image/jpeg");
        }
    }
}
