﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcForumapp.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Technology");
        }

    }
}
