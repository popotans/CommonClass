using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace CommonClass
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
url=?url,click=?click,authorid=?authorid,cid=?cid,cp1=?cp1,cp2=?cp2,indate=?indate,kwd=?kwd,`desc`=?desc where idx={0}", art.IDx.ToString());
            //db.ExecNonQuery(
            List<string> names = new List<string>()
            {
                "?title","?content","?icon","?url","?click","?authorid","?cid","?cp1","?cp2","?indate","?kwd","?desc"
            };
            List<object> vals = new List<object>()
            {
                art.Title,art.Content,art.Icon,art.Url,art.Click,art.AuthorID,art.CID,art.CP1,art.CP2,art.InDate,art.Kwd,art.Desc
            };
            List<object> ts = new List<object>()
            {
                MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.VarChar,MySqlDbType.Int32,
                MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Int32,MySqlDbType.Int32,
                MySqlDbType.Datetime,MySqlDbType.VarChar,MySqlDbType.VarChar
            };
            IDbDataParameter[] idp = db.GetParams(names, vals, ts);
            db.ExecNonQuery(sql, idp);
            return art;
        }

        public int Insert(Article art)
        {
            DbConnection conn = db.GetConn();
            string insert = @"insert into `article`(title,content,icon,url,click,authorid,cid,cp1,cp2,indate,kwd,`desc`)values(?title,?content,?icon,?url,?click,
?authorid,?cid,?cp1,?cp2,?indate,?kwd,?desc)";
            string max = @"select idx from `article` order by `idx` desc limit 0,1 ";
            int result = 0;
            using (conn)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("?title", MySqlDbType.VarChar);
                dic.Add("?content", MySqlDbType.VarChar);
                dic.Add("?icon", MySqlDbType.VarChar);
                dic.Add("?url", MySqlDbType.VarChar);
                dic.Add("?click", MySqlDbType.Int32);
                dic.Add("?authorid", MySqlDbType.Int32);
                dic.Add("?cid", MySqlDbType.Int32);
                dic.Add("?cp1", MySqlDbType.Int32);
                dic.Add("?cp2", MySqlDbType.Int32);
                dic.Add("?indate", MySqlDbType.Datetime);
                dic.Add("?kwd", MySqlDbType.VarChar);
                dic.Add("?desc", MySqlDbType.VarChar);
                List<object> vals = new List<object>();
                vals.Add(art.Title);
                vals.Add(art.Content);
                vals.Add(art.Icon);
                vals.Add(art.Url);
                vals.Add(art.Click);
                vals.Add(art.AuthorID);
                vals.Add(art.CID);
                vals.Add(art.CP1);
                vals.Add(art.CP2);
                vals.Add(art.InDate);
                vals.Add(art.Kwd);
                vals.Add(art.Desc);
                IDbDataParameter[] arr = db.GetParams(dic, vals);
                db.ExecNonQuery(conn, insert, arr);
                result = db.ExecScalarInt(conn, max);
                conn.Close();
            }
            return result;
        }

        public void Delete(int idx)
        {
            string sql = @"delete from article where idx=" + idx.ToString();
            db.ExecNonQuery(sql);
        }

        private string BuildWhere(Article art)
        {
            string where = "";
            if (art != null)
            {
                string wh = " ";
                if (art.IDx != 0) wh += " and idx=" + art.IDx;
                if (!string.IsNullOrEmpty(art.Kwd)) wh += " and( title like '%" + art.Kwd + "%' or kwd like '%" + art.Kwd + "%' or `desc` like '%" + art.Kwd + "%'  )";
                if (art.CID != 0) wh += " and cid= " + art.CID;
                if (art.CP1 != 0) wh += " and  cp1=" + art.CP1;
                if (art.CP2 != 0) wh += " and  CP2=" + art.CP2;
                if (art.AuthorID != 0) wh += " and  AuthorID=" + art.AuthorID;
                if (!string.IsNullOrEmpty(wh))
                {
                    where = " where  1=1 " + wh;
                }
            }
            return where;
        }

        public Helper.PageModel GetPageModelList(int page, int pageSize, Article art)
        {
            List<Article> l = GetList(page, pageSize, art);
            Helper.PageModel pm = new Helper.PageModel();
            pm.List = l;
            string recordcountSql = "select count(1) from `article` " + BuildWhere(art);
            int recordcount = db.ExecScalarInt(recordcountSql);
            int pagecount = (int)Math.Ceiling(recordcount * 1.0 / pageSize);
            pm.Page = page;
            pm.Pagesize = pageSize;
            pm.TotalRecord = recordcount;
            return pm;
        }

        public List<Article> GetList(int page, int pageSize, Article art)
        {
            if (page < 1) page = 1;
            string where = BuildWhere(art);

            List<Article> l = new List<Article>();
            int beginIndex = 0;
            if (page > 1) { beginIndex = (page - 1) * pageSize; }
            string sql = string.Format(@"select * from `article` {0} order by idx desc limit {1},{2}",
                where, beginIndex, pageSize);
            IDataReader dr = db.GetReader(sql);
            using (dr)
            {
                while (dr.Read())
                {
                    l.Add(DaoUtil.Instance.ConvertArticle(dr));
                }
            }
            return l;
        }
    }
}