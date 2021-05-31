using DataIO;
using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class StoreController : Controller
    {
        private CategoryIO db = new CategoryIO();
        private ProductIO db1 = new ProductIO();
        // GET: StoreFont
        public ActionResult Index(string searchname, string id = "CP01")
        {
            if (!string.IsNullOrEmpty(searchname))
            {
                var cate = db.GetListCate();
                var prolist = db1.GetListSearch(searchname);
                var tuple = new Tuple<List<Category>, List<Product>>(cate, prolist);
                return View(tuple);
            }
            else
            {
                var cate = db.GetListCate();
                var prolist = db1.GetProductsByID(id);
                var tuple = new Tuple<List<Category>, List<Product>>(cate, prolist);
                return View(tuple);
            }
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db1.GetDetails(id);
            int propr = db1.GetDetails(id).PRICE;
            int proprice = propr + (propr / 100 * 30);
            ViewBag.proPrice = proprice;
            string idca = db1.GetDetails(id).IDCATE;
            var cate = db.GetDetails(idca).CATEGORYNAME;
            ViewBag.idCate = cate;
            if (pro == null)
            {
                return HttpNotFound();
            }

            return View(pro);
        }
    }
}