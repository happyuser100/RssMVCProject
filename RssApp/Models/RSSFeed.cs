using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssApp.Models
{
    public class RSSFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        //public string PubDate { get; set; }

        public string Source { get; set; }

    }
}