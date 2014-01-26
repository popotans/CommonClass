using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CommonClass.Code
{
    public class MySqlArticle
    {
        public MySqlArticle(string str) { this.db = new MySqlDB(str); }
        internal IDb db
        {
            get;
            set;
        }

        public Article Get(int idx)
        {
            Article art = null;
            string sql = "select * from `article` where `idx` = " + idx;
            IDataReader dr = db.GetReader(sql);
            if (dr.Read())
            {
                art = DaoUtil.Instance.ConvertArticle(dr);
            }
            dr.Close();
            dr.Dispose();
            return art;
        }

        public Article Update(Article art)
        {
            if (art.IDx <= 0) throw new Exception("no such article!");
            string sql = string.Format(@"update `article` set title=?title,content=?content,icon=?icon,
url=?url,click=?click,authorid=?authorid,cid=?cid,cp1=?cp1,cp2=?cp2,indate=?indate where idx=", art.IDx.ToString());
            //db.ExecNonQuery(
            List<string> names = new List<string>()
            {
                "?title","?content","?icon","?url","?click","?authorid","?cid","?cp1","?cp2","?indate"
            };
            List<object> vals = new List<object>()
            {
                art.Title,art.Content,art.Icon,art.Url,art.Click,art.AuthorID,art.CID,art.CP1,art.CP2,art.InDate
            };
            List<MySqlDbType> ts = new List<MySqlDbType>()
            {
                MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Datetime
            };
            IDbDataParameter[] idp = db.GetParams(names, vals, ts);
            db.ExecNonQuery(sql, idp);
            return art;
        }

        public int Insert(Article art)
        {
            return 0;
        }


    }
}