using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataIO;
using DataProvider;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        ProductIO db = new ProductIO();
        CategoryIO db1 = new CategoryIO();
        public ActionResult Index()
        {
            var listpro1 = db.GetTopNew("CP01");
            var listpro2 = db.GetTopNew("CP02");
            var listpro3 = db.GetTopNew("CP03");
            var listpro4 = db.GetTopNew("CP04");
            ViewBag.list1 = listpro1;
            ViewBag.list2 = listpro2;
            ViewBag.list3 = listpro3;
            ViewBag.list4 = listpro4;
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