using DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class BlogsController : Controller
    {
        private BlogIO db = new BlogIO();
        public ActionResult Index()
        {
            var listBlog = db.GetListBlog();
            return View(listBlog);
        }
    }
}