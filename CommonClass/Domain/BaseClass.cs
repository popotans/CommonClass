using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass.Contract;
using System.Data;
using System.Data.Common;
namespace CommonClass.Domain
{
    public class AccessClass : IClassDao
    {
        public AccessClass()
        {

        }

        public AccessClass(IDB db)
        {
            this.db = db;
        }
        public int IDx { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public string Title { get; set; }

        public IDB db { get; set; }

        public virtual int Insert()
        {
            return (int)db.Insert("");
        }
        public virtual int Update()
        {
            return db.Update("");
        }

        private ClassInfo Convert(IDataReader dr)
        {
            ClassInfo bc = new ClassInfo();
            return bc;
        }

        public ClassInfo GetInfo(int idx)
        {
            ClassInfo bc = null;
            string sql = "select * from cls where idx=" + idx;
            IDataReader dr = db.GetReader(sql);
            if (dr.Read())
            {
                bc = Convert(dr);
            }
            dr.Close();
            return bc;
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

        public virtual List<ClassInfo> GetRoot()
        {
            string sql = "select * from cls where p1=0 and p2=0";
            return null;
        }
        public virtual List<ClassInfo> GetByP1(int p1)
        {
            string sql = "select * from cls where p1=" + p1 + " and p2=0";
            return null;
        }
        public virtual List<ClassInfo> GetByP1All(int p1)
        {
            string sql = "select * from cls where p1=0";
            return null;
        }
        public virtual List<ClassInfo> GetByP2(int p2)
        {
            string sql = "select * from cls where p2=" + p2 + " and p1!=0 ";
            return null;
        }
    }
}