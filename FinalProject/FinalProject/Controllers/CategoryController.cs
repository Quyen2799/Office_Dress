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
    public class CategoryController : Controller
    {
        CategoryIO db = new CategoryIO();
        // GET: CategoriesProduct
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
                var listcatebysearch = db.GetListSearch(searchname).ToList();
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(listcatebysearch.ToPagedList(pageNumber,pageSize));
            }   
            else
            {
                var listcate = db.GetListCate().ToList();
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(listcate.ToPagedList(pageNumber,pageSize));
            }    
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.GetDetails(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }
        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CATEGORYNAME")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.AddCate(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.GetDetails(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CATEGORYNAME")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.UpdateCate(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.GetDetails(id);
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
            Category category = db.GetDetails(id);
            db.DeleteCate(category);
            return RedirectToAction("Index");
        }
    }
}