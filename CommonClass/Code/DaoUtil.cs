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
    }
}