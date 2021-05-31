using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        MyShopHTQDB db = new MyShopHTQDB();
        const string PromoCode = "FREE";
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    var cart = ShoppingCart.GetCart(this.HttpContext);

                    order.EMAIL = User.Identity.Name;
                    order.TOTALPRICE = cart.GetTotal();
                    //Save Order
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //Process the order

                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.ID });
                }

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(o => o.ID == id && o.EMAIL == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}