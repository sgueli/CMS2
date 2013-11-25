using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.WEB.Controllers
{
    public class PageController : Controller
    {

        public ActionResult Index()
        {
            List<Page> pages = null;

            using (var ctx = new CMS2DBEntities())
            {
                var query = ctx.Pages.Include("Contents").OrderByDescending(p => p.IsHomePage).ThenBy(p => p.Priority);
                ViewBag.query = query.ToString();
                pages = query.ToList();
            }

            return View(pages);
        }

        public ActionResult Create()
        {
            var page = new Page();
            return View(page);
        }

        [HttpPost]
        public ActionResult Create(Page page)
        {
            if (String.IsNullOrEmpty(page.Title))
            {
                ModelState.AddModelError("Title", "this is required!!!!!");
                return View(page);
            }

            using (var ctx = new CMS2DBEntities())
            {
                ctx.Pages.Add(page);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Page page)
        {
            using (var ctx = new CMS2DBEntities())
            {
                ctx.Pages.Attach(page);
                ctx.Entry(page).State = System.Data.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Page page = null;
            using (var ctx = new CMS2DBEntities())
            {
                page = ctx.Pages.SingleOrDefault(p => p.Id == id);
            }
            return View(page);
        }

        [HttpPost]
        public ActionResult Edit(Page page)
        {
            using (var ctx = new CMS2DBEntities())
            {
                ctx.Pages.Attach(page);
                ctx.Entry(page).State = System.Data.EntityState.Modified;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
