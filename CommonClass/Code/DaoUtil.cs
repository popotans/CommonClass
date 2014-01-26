using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
namespace CommonClass
{
    internal class DaoUtil
    {
        static private DaoUtil _daoutil = new DaoUtil();
        private DaoUtil() { }


        public static DaoUtil Instance
        {
            get
            {
                return _daoutil;
            }
        }


        public ClassInfo Convert(IDataReader dr)
        {
            ClassInfo bc = new ClassInfo();
            bc.IDx = int.Parse(dr["idx"].ToString());
            bc.P1 = int.Parse(dr["P1"].ToString());
            bc.P2 = int.Parse(dr["P2"].ToString());
            bc.Title = dr["TITLE"].ToString();
            if (dr["disable"] != null && dr["disable"] != DBNull.Value)
            {
                bc.Disable = int.Parse(dr["disable"].ToString());
            }
            if (dr["orderidx"] != null && dr["orderidx"] != DBNull.Value)
            {
                bc.OrderIdx = int.Parse(dr["orderidx"].ToString());
            }
            if (dr["siteid"] != null && dr["siteid"] != DBNull.Value)
            {
                bc.SiteID = int.Parse(dr["siteid"].ToString());
            }
            if (dr["url"] != null && dr["url"] != DBNull.Value)
            {
                bc.Url = dr["url"].ToString();
            }
            return bc;
        }

        public Article ConvertArticle(IDataReader dr)
        {
            Article a = new Article();
            a.IDx = int.Parse(dr["idx"].ToString());
            a.Title = dr["title"].ToString();
            a.Content = dr["content"].ToString();
            a.Icon = dr["icon"] == DBNull.Value ? string.Empty : dr["icon"].ToString();
            a.Url = dr["Url"] == DBNull.Value ? string.Empty : dr["Url"].ToString();
            if (dr["click"] != null && dr["click"] != DBNull.Value)
            {
                a.Click = int.Parse(dr["click"].ToString());
            }
            else a.Click = 0;

            if (dr["indate"] != null && dr["indate"] != DBNull.Value)
            {
                a.InDate = DateTime.Parse(dr["indate"].ToString());
            }
            else a.InDate = new DateTime(2000, 1, 1);

            if (dr["authorid"] != null && dr["authorid"] != DBNull.Value)
            {
                a.AuthorID = int.Parse(dr["authorid"].ToString());
            }
            else a.AuthorID = 0;

            if (dr["cid"] != null && dr["cid"] != DBNull.Value)
            {
                a.CID = int.Parse(dr["cid"].ToString());
            }
            else a.CID = 0;

            if (dr["cp1"] != null && dr["cp1"] != DBNull.Value)
            {
                a.CP1 = int.Parse(dr["cp1"].ToString());
            }
            else a.CP1 = 0;

            if (dr["cp2"] != null && dr["cp2"] != DBNull.Value)
            {
                a.CP2 = int.Parse(dr["cp2"].ToString());
            }
            else a.CP2 = 0;


            return a;
        }

        public User ConvertUser(IDataReader dr)
        {
            User u = new User();
            u.IDx = int.Parse(dr["idx"].ToString());
            u.Title = dr["Title"].ToString();
            u.Pwd = dr["Pwd"].ToString();
            u.Icon = dr["icon"].ToString();
            u.InDate = DateTime.Parse(dr["indate"].ToString());
            return u;
        }

    }
}