using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcForumapp.Controllers
{
    public class LoginController : Controller
    {
        newForumDBEntities db = new newForumDBEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(mvcForumapp.Models.userModel user)
        {
            if (ModelState.IsValid)
            {
                if (isValid(user.Email, user.password))
                {
                    var user1 = db.tblUsers.FirstOrDefault(u => u.EmailID == user.Email).UserName;
                    Session["UserName"] = user1;
                    //FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Technology");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is Incorrect");
                }
            }
            return View(user);
        }

        private bool isValid(string Email, string password)
        {
            //string crypto = Encrypt(password, true);
            bool isvalid = false;
            using (var db = new newForumDBEntities())
            {
                var user = db.tblUsers.FirstOrDefault(u => u.EmailID == Email);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        isvalid = true;
                    }
                }

            }
            return isvalid;
        }
    }
}
