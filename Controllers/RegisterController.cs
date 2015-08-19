using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace mvcForumapp.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /User/

        newForumDBEntities db = new newForumDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult IsUserNameAvailable(string UserName)
        {
            var usrAvailable = db.tblUsers.Where(p => p.UserName == UserName).Select(img => img.UserName).FirstOrDefault();

            if (usrAvailable == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //string html = "<span style='color:Red;'> Username in use</span>";
            return Json("<span style='color:Red;'> Username in already in use</span>", JsonRequestBehavior.AllowGet);

        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsDisplayAvailable(string Displayname)
        {
            var usrAvailable = db.tblUsers.Where(p => p.DisplayName == Displayname).Select(img => img.DisplayName).FirstOrDefault();

            if (usrAvailable == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //string html = "<span style='color:Red;'> Username in use</span>";
            return Json("<span style='color:Red;'> Display name in already use</span>", JsonRequestBehavior.AllowGet);

        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsEmailAvailable(string Email)
        {
            var usrAvailable = db.tblUsers.Where(p => p.EmailID == Email).Select(img => img.EmailID).FirstOrDefault();

            if (usrAvailable == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //string html = "<span style='color:Red;'> Username in use</span>";
            return Json("<span style='color:Red;'> Emai in already in use</span>", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Register(mvcForumapp.Models.Register user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        using (var db = new newForumDBEntities())
                        {
                            Byte[] imgByte = null;
                            HttpPostedFileBase File = file;
                            //Create byte Array with file len
                            imgByte = new Byte[File.ContentLength];
                            //force the control to load data in array
                            File.InputStream.Read(imgByte, 0, File.ContentLength);
                            var userdets = db.tblUsers.CreateObject();
                            userdets.UserName = user.Username;
                            userdets.DateJoined = DateTime.Now;
                            userdets.DisplayName = user.Displayname;
                            userdets.EmailID = user.Email;
                            userdets.Password = user.password;
                            userdets.Photo = imgByte;
                            //var encrppass = Encrypt(user.password, true);
                            //var userdets = db.tblUsers.CreateObject();
                            //userdets.EmailID = user.Email;
                            //userdets.password = encrppass;

                            db.tblUsers.AddObject(userdets);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Technology");
                        }
                    }
                }

            }
            return View(user);
        }
    }
}
