using RssApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RssApp.Controllers
{
    public class TreeviewController : Controller
    {
        private readonly int FEEDS = 2;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Simple()
        {
            List<MenuItem> all = new List<MenuItem>();
            using (RssDBEntities dc = new RssDBEntities())
            {
                all = dc.MenuItems.OrderBy(a => a.ParentMenuID).ToList();
            }
            ViewBag.rSSItems = all;
            return View();
            //return View(all);
        }

        [HttpPost]
        public ActionResult AddFeed(RSSFeed rSSFeed)
        {
            using (RssDBEntities dc = new RssDBEntities())
            {
                MenuItem menuItem = new MenuItem();
                menuItem.MenuName = rSSFeed.Title;
                menuItem.NavURL = rSSFeed.Link;
                menuItem.ParentMenuID = FEEDS;

                dc.MenuItems.Add(menuItem);
                dc.SaveChanges();
            }
            TempData["notice"] = "Successfully Added";
            return RedirectToAction("Simple");
        }
    }
}