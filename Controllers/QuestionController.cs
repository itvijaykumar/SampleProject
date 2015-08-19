using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcForumapp.Controllers
{
    public class QuestionController : Controller
    {
        [HttpGet]
        public ActionResult PostQuestion()
        {
            int techID = Convert.ToInt16(Request.QueryString["TechID"].ToString());
            return View();
        }

        [HttpPost]
        public ActionResult PostQuestion(mvcForumapp.Models.Questions user)
        {
            int techID = 0;
            if (ModelState.IsValid)
            {
                techID = Convert.ToInt16(Request.QueryString["TechID"].ToString());
                using (var db = new newForumDBEntities())
                {
                    var userdets = db.tblQuestions.CreateObject();
                    userdets.TechID = Convert.ToInt16(Request.QueryString["TechID"].ToString());
                    userdets.QuestionTitle = user.TopicTitle;
                    userdets.QuestionDesc = user.TopicContent;
                    userdets.DatePosted = DateTime.Now;
                    userdets.UserName = Session["UserName"].ToString();
                    userdets.viewCount = 0;
                    userdets.ReplyCount = 0;
                    db.tblQuestions.AddObject(userdets);
                    db.SaveChanges();
                    return RedirectToAction("DisplayQuestions", "Technology", new { TechID = techID });
                }
            }
            return View(user);
        }

    }
}
