using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass.Contract;
using System.Data;
using System.Data.Common;
using System.Text;
namespace CommonClass
{
    /// <summary>
    /// 示例：
    /// <para>BaseClass target = new BaseClass(connstr);</para>
    /// <para>target.Insert(new ClassInfo());</para>
    ///
    /// </summary>
    public class BaseClass : IBaseClass
    {
        //public BaseClassDao(IDb db)
        //{

        //}

        public BaseClass(string connstr)
        {
            this.db = new AccessDB(connstr);
        }

        public IDb db
        {
            get
            ;
            set;
        }

        public virtual int Insert(ClassInfo ci)
        {
            string s = "select max(idx) from cls";
            int id = db.ExecScalarInt(s);
            if (id > 0) ci.IDx = id + 1; else ci.IDx = 1;

            string sql = "insert into cls (idx,TITLE,P1,P2,disable,orderidx,siteid,url)VALUES(@idx,@title,@p1,@p2,@disable,@orderidx,@siteid,@url)";
            List<string> names = new List<string>();
            names.Add("@idx");
            names.Add("@title");
            names.Add("@p1");
            names.Add("@p2");
            names.Add("@disable");
            names.Add("@orderidx");
            names.Add("@siteid");
            names.Add("@url");
            List<object> vals = new List<object>();
            vals.Add(ci.IDx);
            vals.Add(ci.Title);
            vals.Add(ci.P1);
            vals.Add(ci.P2);
            vals.Add(ci.Disable);
            vals.Add(ci.OrderIdx);
            vals.Add(ci.SiteID);
            vals.Add(ci.Url);
            List<object> ts = new List<object>();
            ts.Add(DbType.Int32);
            ts.Add(DbType.String);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.String);
            Object obj = db.ExecNonQuery(sql, db.GetParams(names, vals, ts));
            return ci.IDx;
        }

        public virtual int InsertRoot(ClassInfo ci)
        {
            ci.P1 = 0;
            ci.P2 = 0;
            int id = Insert(ci);
            return id;
        }

        public virtual int InsertP1(ClassInfo ci)
        {
            ci.P2 = 0;
            int id = Insert(ci);
            return id;
        }

        public virtual int InsertP1P2(ClassInfo ci)
        {
            int id = Insert(ci);
            return id;
        }


        public virtual ClassInfo Get(int idx)
        {
            ClassInfo ci = null;
            string sql = "select * from cls where [disable] <> 1 and  idx=" + idx;
            IDataReader dr = db.GetReader(sql);
            if (dr.Read())
            {
                ci = Convert(dr);
            }
            dr.Close();
            return ci;
        }

        public virtual int Update(ClassInfo ci)
        {
            string sql = "update cls set title=@title,p1=@p1,p2=@p2,disable=@disable,orderidx=@orderidx,siteid=@siteid,url=@url  where idx=" + ci.IDx;
            List<string> names = new List<string>();
            names.Add("@title");
            names.Add("@p1");
            names.Add("@p2");
            names.Add("@disable");
            names.Add("@orderidx");
            names.Add("@siteid");
            names.Add("@url");
            List<object> vals = new List<object>();
            vals.Add(ci.Title);
            vals.Add(ci.P1);
            vals.Add(ci.P2);
            vals.Add(ci.Disable);
            vals.Add(ci.OrderIdx);
            vals.Add(ci.SiteID);
            vals.Add(ci.Url);
            List<object> ts = new List<object>();
            ts.Add(DbType.String);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.Int32);
            ts.Add(DbType.String);
            int obj = db.ExecNonQuery(sql, db.GetParams(names, vals, ts));
            return obj;
        }

        private ClassInfo Convert(IDataReader dr)
        {
            return DaoUtil.Instance.Convert(dr);
        }

        /*
         * 0  0  1
         * 0   0   2
         * 0   0   3
         * 1 0     4
         * 1  0     5
         * 2   0    6
         * 1   4    7
         *   
         * 
         * 
         * 
         * **/

        public virtual List<ClassInfo> GetByIds(int[] idsArr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in idsArr)
            {
                sb.AppendFormat("{0},", i);
            }
            string sql = "select * from cls where ([disable] <> 1 or [disable] is null) and idx in(" + sb.ToString().TrimEnd(',') + ")";
            List<ClassInfo> list = new List<ClassInfo>();
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(Convert(dr));
            }
            dr.Close();
            return list;
        }
        public virtual List<ClassInfo> GetRoot(int siteid)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from cls where ([disable] <> 1 or [disable] is null) and p1=0 and p2=0 and siteid=" + siteid;
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(Convert(dr));
            }
            dr.Close();
            return list;
        }
        public virtual List<ClassInfo> GetByP1(int p1)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from cls where  ([disable] <> 1 or [disable] is null)  and p1=" + p1 + " and p2=0";
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(Convert(dr));
            }
            dr.Close();
            return list;
        }
        public virtual List<ClassInfo> GetByP1All(int p1)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from cls where   ([disable] <> 1 or [disable] is null)  and p1=" + p1;
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(Convert(dr));
            }
            dr.Close();
            return list;
        }
        public virtual List<ClassInfo> GetByP2(int p2)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from cls where   ([disable] <> 1 or [disable] is null)  and  p2=" + p2 + " and p1>0 ";
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(Convert(dr));
            }
            dr.Close();
            return list;
        }



        public List<ClassInfo> GetAll(int siteid)
        {
            throw new NotImplementedException();
        }


        public List<ClassInfo> GetRoot(List<ClassInfo> all)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> GetByP2(List<ClassInfo> all, int p2)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> GetByP1All(List<ClassInfo> all, int p1)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> GetByP1(List<ClassInfo> all, int p1)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> InitDropDownList(int siteid)
        {
            throw new NotImplementedException();
        }


        public List<ClassInfo> InitDropDownList(int siteid, bool has3)
        {
            throw new NotImplementedException();
        }
    }
}