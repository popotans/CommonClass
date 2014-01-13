using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

/*

create table `cls`
(
idx int not null,
title varchar(200) not null,
p1 int not null,
p2 int not null,
disable int not null,
orderidx int not null,
SiteID int not null,
Url varchar(1000)
)

 

**/
namespace CommonClass
{
    public class MySqlClass : CommonClass.IBaseClass
    {
        public MySqlClass(string str) { this.db = new MySqlDB(str); }
        public IDb db
        {
            get;
            set;
        }

        public ClassInfo Get(int idx)
        {
            ClassInfo ci = null;
            string sql = "select * from `cls` where `disable` != 1 and  `idx`=" + idx;
            IDataReader dr = db.GetReader(sql);
            if (dr.Read())
            {
                ci = DaoUtil.Instance.Convert(dr);
            }
            dr.Close();
            return ci;
        }

        public List<ClassInfo> GetByIds(int[] idsArr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in idsArr)
            {
                sb.AppendFormat("{0},", i);
            }
            string sql = "select * from `cls` where (`disable` != 1 or `disable` is null) and `idx` in(" + sb.ToString().TrimEnd(',') + ")";
            List<ClassInfo> list = new List<ClassInfo>();
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public List<ClassInfo> GetByP1(int p1)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from `cls` where  (`disable` != 1 or `disable` is null)  and `p1`=" + p1 + " and `p2`=0";
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public List<ClassInfo> GetByP1All(int p1)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from `cls` where   (`disable` != 1 or `disable` is null)  and `p1`=" + p1;
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public List<ClassInfo> GetByP2(int p2)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from `cls` where   (`disable` != 1 or `disable` is null)  and  `p2`=" + p2 + " and `p1`>0 ";
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public List<ClassInfo> GetRoot(int siteid)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from `cls` where (`disable` != 1 or `disable` is null) and `p1`=0 and `p2`=0 and `siteid`=" + siteid;
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public int Insert(ClassInfo ci)
        {
            if (ci.Url == null) ci.Url = string.Empty;
            string s = "select max(`idx`) from cls";
            int id = db.ExecScalarInt(s);
            if (id > 0) ci.IDx = id + 1; else ci.IDx = 1;

            string sql = "insert into `cls` (`idx`,`TITLE`,`P1`,`P2`,`disable`,`orderidx`,`siteid`,`url`)VALUES(?idx,?title,?p1,?p2,?disable,?orderidx,?siteid,?url)";
            List<string> names = new List<string>();
            names.Add("?idx");
            names.Add("?title");
            names.Add("?p1");
            names.Add("?p2");
            names.Add("?disable");
            names.Add("?orderidx");
            names.Add("?siteid");
            names.Add("?url");
            List<object> vals = new List<object>();
            vals.Add(ci.IDx);
            vals.Add(ci.Title);
            vals.Add(ci.P1);
            vals.Add(ci.P2);
            vals.Add(ci.Disable);
            vals.Add(ci.OrderIdx);
            vals.Add(ci.SiteID);
            vals.Add(ci.Url);
            List<MySqlDbType> ts = new List<MySqlDbType>();
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.String);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.String);
            Object obj = db.ExecNonQuery(sql, db.GetParams(names, vals, ts));
            return ci.IDx;
        }

        public int InsertP1(ClassInfo ci)
        {
            ci.P2 = 0;
            int id = Insert(ci);
            return id;
        }

        public int InsertP1P2(ClassInfo ci)
        {
            int id = Insert(ci);
            return id;
        }

        public int InsertRoot(ClassInfo ci)
        {
            ci.P1 = 0;
            ci.P2 = 0;
            int id = Insert(ci);
            return id;
        }

        public int Update(ClassInfo ci)
        {
            string sql = "update `cls` set `title`=?title,`p1`=?p1,`p2`=?p2,`disable`=?disable,`orderidx`=?orderidx,`siteid`=?siteid,`url`=?url  where `idx`=" + ci.IDx;
            List<string> names = new List<string>();
            names.Add("?title");
            names.Add("?p1");
            names.Add("?p2");
            names.Add("?disable");
            names.Add("?orderidx");
            names.Add("?siteid");
            names.Add("?url");
            List<object> vals = new List<object>();
            vals.Add(ci.Title);
            vals.Add(ci.P1);
            vals.Add(ci.P2);
            vals.Add(ci.Disable);
            vals.Add(ci.OrderIdx);
            vals.Add(ci.SiteID);
            vals.Add(ci.Url);
            List<MySqlDbType> ts = new List<MySqlDbType>();
            ts.Add(MySqlDbType.String);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.Int32);
            ts.Add(MySqlDbType.String);
            int obj = db.ExecNonQuery(sql, db.GetParams(names, vals, ts));
            return obj;
        }
    }
}