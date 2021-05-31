using DataIO;
using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        ProductIO db = new ProductIO();
        // GET: Products
        public ActionResult Index(string searchname, string currentFilter, int? page)
        {
            if (searchname != null)
            {
                page = 1;
            }
            else
            {
                searchname = currentFilter;
            }
            ViewBag.CurrentFilter = searchname;

            if (!string.IsNullOrEmpty(searchname))
            {
                var listprobysearch = db.GetListSearch(searchname).ToList();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(listprobysearch.ToPagedList(pageNumber,pageSize));
            }    
            else
            {
                var listprobysearch = db.GetListProduct().ToList();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(listprobysearch.ToPagedList(pageNumber, pageSize));
            }    
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product category = db.GetDetails(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
        public ActionResult Create()
        {
            CategoryIO p = new CategoryIO();
            List<Category> list = p.GetListCate().ToList();
            SelectList catelist = new SelectList(list, "ID", "CATEGORYNAME");
            ViewBag.cateList = catelist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PRODUCTNAME,IDCATE,SIZE,PRICE,MATERIAL,HDBQ,PRODUCTIMAGE")] Product products)
        {
            if (ModelState.IsValid)
            {
                db.AddProduct(products);
                return RedirectToAction("Index");
            }

            return View(products);
        }

        public ActionResult Edit(string id)
        {
            CategoryIO p = new CategoryIO();
            List<Category> list = p.GetListCate().ToList();
            SelectList catelist = new SelectList(list, "ID", "CATEGORYNAME");
            ViewBag.cateList = catelist;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.GetDetails(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PRODUCTNAME,IDCATE,SIZE,PRICE,MATERIAL,HDBQ,PRODUCTIMAGE")] Product products)
        {
            if (ModelState.IsValid)
            {
                db.UpdatePro(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product category = db.GetDetails(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product products = db.GetDetails(id);
            db.DeletePro(products);
            return RedirectToAction("Index");
        }
    }
}