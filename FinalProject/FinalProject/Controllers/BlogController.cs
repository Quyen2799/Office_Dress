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
    public class BlogController : Controller
    {
        BlogIO db = new BlogIO();
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
                var listblog = db.GetListSearch(searchname).ToList();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(listblog.ToPagedList(pageNumber,pageSize));
            }   
            else
            {
                var listblog = db.GetListBlog().ToList();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(listblog.ToPagedList(pageNumber,pageSize));
            }    
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.GetDetails(id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TITLE,CONTENT,DATEPOST,BLOGIMAGE")] Blog blogs)
        {
            if (ModelState.IsValid)
            {
                db.AddBlog(blogs);
                return RedirectToAction("Index");
            }

            return View(blogs);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.GetDetails(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TITLE,CONTENT,DATEPOST,BLOGIMAGE")] Blog blogs)
        {
            if (ModelState.IsValid)
            {
                db.UpdateBlog(blogs);
                return RedirectToAction("Index");
            }
            return View(blogs);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.GetDetails(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Blog blog = db.GetDetails(id);
            db.DeleteBlog(blog);
            return RedirectToAction("Index");
        }
    }
}