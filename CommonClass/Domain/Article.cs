using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass
{
    public partial class Article
    {
        public int IDx { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Icon { get; set; }
        public string Url { get; set; }
        public int Click { get; set; }
        public int AuthorID { get; set; }

        public int CID { get; set; }
        public int CP1 { get; set; }
        public int CP2 { get; set; }

        public DateTime InDate { get; set; }

        public string Kwd { get; set; }
        public string Desc { get; set; }
        public int IsTop { get; set; }

    }
}