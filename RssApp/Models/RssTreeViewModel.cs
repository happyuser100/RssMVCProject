using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssApp.Models
{
    public class RssTreeViewModel
    {
        public List<RssApp.MenuItem> rSSItems { get; set; }
        public RSSFeed rSSFeed { get; set; }
}
}