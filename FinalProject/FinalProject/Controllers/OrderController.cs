using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataIO;
using DataProvider;
using FinalProject.ViewModels;
using PagedList;

namespace FinalProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private OrderIO db = new OrderIO();
        private OrderDetailIO db1 = new OrderDetailIO();
        public ActionResult Index(int? id)
        {
            var viewmodel = new CoachIndex();
            viewmodel.Orders = db.GetListOrder().ToList();
            if (id != null)
            {
                ViewBag.OrderID = id;
                viewmodel.OrderDetails = db1.GetListByOrderID(id);
            }
            return View(viewmodel);
        }
    }
}