﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppTest.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}