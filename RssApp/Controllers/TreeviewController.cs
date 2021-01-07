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
            try
            {
                List<MenuItem> all = new List<MenuItem>();
                using (RssDBEntities dc = new RssDBEntities())
                {
                    all = dc.MenuItems.OrderBy(a => a.ParentMenuID).ToList();
                }
                ViewBag.rSSItems = all;
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        public ActionResult AddFeed(RSSFeed rSSFeed)
        {
            try
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }
    }
}