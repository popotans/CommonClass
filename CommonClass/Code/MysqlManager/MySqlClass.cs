﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

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
            string sql = "select * from `cls` where  (`disable` != 1 or `disable` is null)  and `p1`=" + p1;
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
            string sql = "select * from `cls` where   (`disable` != 1 or `disable` is null)  and（ `p1`=" + p1 + " or `p2`=1)";
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
            string sql = "select * from `cls` where (`disable` != 1 or `disable` is null) and `p1`=0 and `p2`=-1 and `siteid`=" + siteid;
            IDataReader dr = db.GetReader(sql);
            while (dr.Read())
            {
                list.Add(DaoUtil.Instance.Convert(dr));
            }
            dr.Close();
            return list;
        }

        public List<ClassInfo> GetAll(int siteid)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            string sql = "select * from `cls` where (`disable` != 1 or `disable` is null) and `siteid`=" + siteid;
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
            List<object> ts = new List<object>();
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
            ci.P2 = -1;
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
            ci.P2 = -1;
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
            List<object> ts = new List<object>();
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

        /// <summary>
        /// 初始化分类表数据库，请谨慎操作，将删除所有数据
        /// </summary>
        public void InitDatabase()
        {
            //db
            //  db.ExecNonQuery("drop database if exists `nq`");
            db.ExecNonQuery("CREATE DATABASE IF NOT EXISTS `nq`  DEFAULT CHARACTER SET utf8 ;");
            //色条encoding
            //            db.ExecNonQuery(@"set character_set_client=utf8;
            //set character_set_connection=utf8;
            //
            //set character_set_database=utf8;
            //
            //set character_set_results=utf8;
            //
            //set character_set_server=utf8;");
            //tb
            db.ExecNonQuery("USE `nq`;");
            db.ExecNonQuery("DROP TABLE IF EXISTS `cls`;");
            db.ExecNonQuery(@"CREATE TABLE `cls` (
  `idx` int(11) NOT NULL,
  `title` varchar(200) NOT NULL,
  `p1` int(11) NOT NULL,
  `p2` int(11) NOT NULL,
  `disable` int(11) NOT NULL,
  `orderidx` int(11) NOT NULL,
  `SiteID` int(11) NOT NULL,
  `Url` varchar(1000) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;");

            db.ExecNonQuery("DROP TABLE IF EXISTS `article`;");
            db.ExecNonQuery(@"CREATE TABLE `article` (
  `idx` int(10) unsigned NOT NULL auto_increment,
  `title` varchar(200) NOT NULL,
  `content` text NOT NULL,
  `icon` varchar(155) default NULL,
  `url` varchar(105) NOT NULL,
  `click` int(10) unsigned NOT NULL default 0,
  `authorid` int(10) unsigned default 0,
  `cid` int(10) unsigned NOT NULL,
  `cp1` int(11) NOT NULL default -1,
  `cp2` int(11) NOT NULL default -1,
  `indate` datetime NOT NULL,
  `kwd` varchar(200) default NULL,
  `desc` varchar(300) default NULL,
  `istop` int default 0,
  PRIMARY KEY  (`idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
");
            db.ExecNonQuery("DROP TABLE IF EXISTS `user`;");
            db.ExecNonQuery(@"
CREATE TABLE `user` (
  `idx` int(11) NOT NULL auto_increment,
  `title` varchar(100) NOT NULL,
  `pwd` varchar(100) NOT NULL,
  `icon` varchar(155) default NULL,
  `indate` datetime default '2000-01-01 00:00:00',
  PRIMARY KEY  (`idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
");
        }





        public List<ClassInfo> GetRoot(List<ClassInfo> all)
        {
            List<ClassInfo> lcin = new List<ClassInfo>();
            foreach (ClassInfo ci in all)
            {
                if (-1 == ci.P2 && ci.P1 == 0)
                    lcin.Add(ci);
            }
            return lcin;
        }

        public List<ClassInfo> GetByP2(List<ClassInfo> all, int p2)
        {
            List<ClassInfo> lcin = new List<ClassInfo>();
            foreach (ClassInfo ci in all)
            {
                if (ci.P1 == p2)
                    lcin.Add(ci);
            }
            return lcin;
        }

        public List<ClassInfo> GetByP1All(List<ClassInfo> all, int p1)
        {
            List<ClassInfo> lcin = new List<ClassInfo>();
            foreach (ClassInfo ci in all)
            {
                if (p1 == ci.P1 || ci.P2 == p1)
                    lcin.Add(ci);
            }
            return lcin;
        }

        public List<ClassInfo> GetByP1(List<ClassInfo> all, int p1)
        {
            List<ClassInfo> lcin = new List<ClassInfo>();
            foreach (ClassInfo ci in all)
            {
                if (p1 == ci.P1 && ci.P2 == -1)
                    lcin.Add(ci);
            }
            return lcin;
        }


        public List<ClassInfo> InitDropDownList(int siteid)
        {
            return InitDropDownList(siteid, false);
        }

        public List<ClassInfo> InitDropDownList(int siteid, bool hasLevel3)
        {
            List<ClassInfo> source = new List<ClassInfo>();

            List<ClassInfo> all = GetAll(siteid);
            foreach (ClassInfo ci in GetRoot(all))
            {
                ci.Depth = 1;
                source.Add(ci);
                foreach (ClassInfo ci1 in GetByP1(all, ci.IDx))
                {
                    ci1.Depth = 2;
                    source.Add(ci1);
                    if (hasLevel3)
                        foreach (ClassInfo ci3 in GetByP2(all, ci1.IDx))
                        {
                            ci3.Depth = 3;
                            source.Add(ci3);
                        }
                }
            }
            return source;
        }
    }
}