using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcForumapp.Controllers
{
    public class TechnologyController : Controller
    {
        //
        // GET: /Technology/
        newForumDBEntities db = new newForumDBEntities();

        public ActionResult Index()
        {
            List<mvcForumapp.TechResult_Result> userview = db.TechResult().ToList();
            return View(userview);
        }

        public ActionResult DisplayQuestions()
        {
            int TechID = Convert.ToInt16(Request.QueryString["TechID"].ToString());

            List<mvcForumapp.QuestionList_Result> disp = db.QuestionList(TechID).ToList();
            return View(disp);

        }

    }
}
