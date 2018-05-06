using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "描述页面";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "联系页面";

            return View();
        }
    }
}