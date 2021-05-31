using DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ProductIO db = new ProductIO();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Product()
        {
            return View(db.GetListProduct().ToList());
        }

    }
}