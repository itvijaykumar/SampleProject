using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace mvcForumapp.Controllers
{
    public class Question_AnswerController : Controller
    {
        newForumDBEntities db = new newForumDBEntities();
        int vwcnt = 0;

        [HttpGet]
        public ActionResult displayQuestionwithAnswers()
        {
            var paramQuesID = new SqlParameter("@QuestionID", SqlDbType.Int);
            var paramRepCnt = new SqlParameter("@viewcount", SqlDbType.Int);
            int quesID = Convert.ToInt16(Request.QueryString["QuestionID"].ToString());
            paramQuesID.Value = quesID;

            var viewcount = db.tblQuestions.Where(e1 => e1.QuestionID == quesID).FirstOrDefault();
            vwcnt = viewcount.viewCount;
            if (vwcnt == 0)
            {
                vwcnt++;
                paramRepCnt.Value = vwcnt;
                var v = db.ExecuteStoreCommand("UPDATE tblQuestions SET viewCount = @viewcount WHERE QuestionID = @QuestionID", paramRepCnt, paramQuesID);
            }

            else
            {
                vwcnt = vwcnt + 1;
                paramRepCnt.Value = vwcnt;
                var v = db.ExecuteStoreCommand("UPDATE tblQuestions SET viewCount = @viewcount WHERE QuestionID = @QuestionID", paramRepCnt, paramQuesID);
            }

            List<mvcForumapp.Questionwithreplys_Result> disp = db.Questionwithreplys(quesID).ToList();
            return View(disp);
        }

        [HttpGet]
        public ActionResult GetPhoto()
        {
            //RouteData.Values["QuesID"]
            int quesID = Convert.ToInt16(Request.QueryString["QuestionID"]);
            byte[] photo = null;

            var usrname = (from a in db.tblQuestions
                           where a.QuestionID == quesID
                           select new { a.UserName });
            var v = db.tblUsers.Where(p => p.UserName == usrname.FirstOrDefault().UserName).Select(img => img.Photo).FirstOrDefault();
            photo = v;
            return File(photo, "image/jpeg");
        }

        [HttpGet]
        public ActionResult ReplyPhoto()
        {
            //RouteData.Values["QuesID"]
            int quesID = Convert.ToInt16(Request.QueryString["QuestionID"]);
            byte[] photo = null;

            var usrname = (from a in db.tblReplies
                           where a.ReplyID == quesID
                           select new { a.UserName });
            var v = db.tblUsers.Where(p => p.UserName == usrname.FirstOrDefault().UserName).Select(img => img.Photo).FirstOrDefault();
            photo = v;
            return File(photo, "image/jpeg");
        }

    }
}
