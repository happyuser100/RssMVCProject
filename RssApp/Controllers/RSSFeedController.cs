﻿using RssApp.Models;
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

        public ActionResult Details(string href, string menuName)
        {
            try
            {
                if (string.IsNullOrEmpty(href))
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Parameter search not found");

                Session["rss"] = "";
                ViewBag.URL = href;
                ViewBag.Search = "";
                ViewBag.MenuName = menuName;

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
                Session["rss"] = RSSFeedData;
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult SearchDetails(string search, string menuName)
        {
            try
            {
                if (search == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Parameter search not found");

                ViewBag.MenuName = menuName;

                var RSSFeedData = Session["rss"] as IEnumerable<RSSFeed>;
                if (RSSFeedData != null)
                {
                    ViewBag.RSSFeed = RSSFeedData.Where(x => x.Title.Contains(search));
                    ViewBag.Search = search;
                    return View();
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "RSSFeedData in Session not found");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }


        public ActionResult RSSFavorites()
        {
            return View();
        }

    }
}