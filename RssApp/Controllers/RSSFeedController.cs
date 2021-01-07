using RssApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace RssApp.Controllers
{
    public class RSSFeedController : Controller
    {
        // GET: RSSFeed
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string href)
        {
            TempData["rss"] = "";
            ViewBag.URL = href;

            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString(href);

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = (x.Element("title") == null) ? "" : (string)x.Element("title").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", ""),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   //PubDate = ((string)x.Element("pubDate"))                                   
                                   //PubDate = (x.Element("pubDate").Value.Equals("")) ? "" : (string)x.Element("pubDate").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "").Substring(0, 20) + "...",
                                   Source = (x.Element("enclosure") == null) ? "" : (string)x.Element("enclosure").Attribute("url").Value
                               });
            ViewBag.RSSFeed = RSSFeedData;
            TempData["rss"] = RSSFeedData;
            return View();
        }

        public ActionResult SearchDetails(string search)
        {
            var RSSFeedData = TempData["rss"] as IEnumerable<RSSFeed>;
            ViewBag.RSSFeed = RSSFeedData.Where(x => x.Title.Contains(search));
            return View();
        }


        public ActionResult RSSFavorites()
        {
            return View();
        }

    }
}