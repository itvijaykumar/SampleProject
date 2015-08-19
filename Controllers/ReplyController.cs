using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace mvcForumapp.Controllers
{
    public class ReplyController : Controller
    {
        newForumDBEntities db = new newForumDBEntities();

        [HttpGet]
        public ActionResult PostReply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostReply(mvcForumapp.Models.Replys reply)
        {
            int repcnt = 0;
            int quesID = 0;
            int techID = 0;
            if (ModelState.IsValid)
            {
                using (var db = new newForumDBEntities())
                {
                    quesID = Convert.ToInt16(Request.QueryString["QuestionID"].ToString());
                    techID = Convert.ToInt16(Request.QueryString["TechID"].ToString());
                    var postrep = db.tblReplies.CreateObject();
                    postrep.TechID = Convert.ToInt16(Request.QueryString["TechID"].ToString());
                    postrep.QuestionID = Convert.ToInt16(Request.QueryString["QuestionID"].ToString());
                    postrep.date = DateTime.Now;
                    postrep.ReplyMsg = reply.ReplyContent;
                    postrep.UserName = Session["UserName"].ToString();
                    db.tblReplies.AddObject(postrep);
                    db.SaveChanges();
                    var paramQuesID = new SqlParameter("@QuestionID", SqlDbType.Int);
                    var paramRepCnt = new SqlParameter("@Repcount", SqlDbType.Int);
                    paramQuesID.Value = quesID;
                    var repcount = db.tblQuestions.Where(e1 => e1.QuestionID == quesID).FirstOrDefault();
                    repcnt = repcount.ReplyCount;

                    if (repcnt == 0)
                    {
                        repcnt++;
                        paramRepCnt.Value = repcnt;
                        var v = db.ExecuteStoreCommand("UPDATE tblQuestions SET ReplyCount = @Repcount WHERE QuestionID = @QuestionID", paramRepCnt, paramQuesID);
                    }
                    else
                    {
                        repcnt = repcnt + 1;
                        paramRepCnt.Value = repcnt;
                        var v = db.ExecuteStoreCommand("UPDATE tblQuestions SET ReplyCount = @Repcount WHERE QuestionID = @QuestionID", paramRepCnt, paramQuesID);
                    }

                    return RedirectToAction("displayQuestionwithAnswers", "Question_Answer", new { QuestionID = quesID });
                }
            }
            return View(reply);
        }

    }
}
