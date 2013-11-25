using CMS2.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CMS2.WEB.Controllers
{
    public class ContentController : Controller
    {
     

        public ActionResult Create(int page_id)
        {
            var model = new ContentCreateViewModel
            {
                Content = new Content { PageId = page_id}
            };
            using (var ctx = new CMS2DBEntities())
            {
                model.Content.Page = ctx.Pages.SingleOrDefault(p => p.Id == model.Content.PageId);
                model.Pages = ctx.Pages.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Content content)
        {
            using (var ctx = new CMS2DBEntities())
            {
                ctx.Contents.Add(content);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "Page");
        }

        public ActionResult Delete(Content content)
        {
            using (var ctx = new CMS2DBEntities())
            {
                ctx.Contents.Attach(content);
                ctx.Entry(content).State = System.Data.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "Page");
        }

        public ActionResult Edit(int id)
        {
            var model = new ContentCreateViewModel { };
            using (var ctx = new CMS2DBEntities())
            {
                var query = ctx.Contents.Include("Page").Where(c=>c.Id == id);
                ViewBag.query = query.ToString();
                model.Content = query.SingleOrDefault();
                model.Pages = ctx.Pages.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Content content)
        {
            using (var ctx = new CMS2DBEntities())
            {
                ctx.Contents.Attach(content);
                ctx.Entry(content).State = System.Data.EntityState.Modified;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "Page");
        }  

    }
}
