using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
namespace CommonClass
{
    public class AccessDB : IDb
    {
        // public AccessDB() { }
        public AccessDB(string conn)
        {
            this.ConnStr = conn;
        }
        public System.Data.IDataReader GetReader(string sql, params System.Data.IDataParameter[] p)
        {
            OleDbConnection conn = GetConn();
            OleDbCommand cmd = new OleDbCommand(sql);
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.Connection = conn;
            conn.Open();
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //   cmd.Connection.Close();
            return dr;
        }

        public System.Data.DataTable GetTable(string sql, params System.Data.IDataParameter[] p)
        {
            throw new NotImplementedException();
        }

        public int ExecNonQuery(string sql, params System.Data.IDataParameter[] p)
        {
            OleDbConnection conn = GetConn();
            OleDbCommand cmd = new OleDbCommand(sql);
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.Connection = conn;
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;

        }

        public string ConnStr
        {
            get
           ;
            set;
        }

        public OleDbConnection GetConn()
        {

            return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.ConnStr);
        }

        public IDbDataParameter GetParam(string name, object val, object t)
        {
            OleDbParameter p = new OleDbParameter(name, val);
            p.DbType = (DbType)t;
            return p;
        }

        public IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<object> t)
        {
            IDbDataParameter[] arr = new OleDbParameter[names.Count];
            //  IDbDataParameter[] psa=new OleDbParameter[]();
            if (names.Count != vals.Count && names.Count != t.Count) throw new ApplicationException("参数不匹配");
            for (int i = 0; i < names.Count; i++)
            {
                arr[i] = GetParam(names[i], vals[i], t[i]);
            }
            return arr;
        }

        public IDbDataParameter[] GetParams(Dictionary<string, object> dic, List<object> vals)
        {
            IDbDataParameter[] arr = new OleDbParameter[dic.Count];
            int i = 0;
            foreach (KeyValuePair<string, object> item in dic)
            {
                arr[i] = GetParam(item.Key, vals[i], item.Value);
                i++;
            }
            return arr;
        }


        public object ExecScalar(string sql, params IDataParameter[] p)
        {
            OleDbConnection conn = GetConn();
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Connection = conn;
            conn.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null && obj != DBNull.Value) return obj;
            return null;
        }
        public int ExecScalarInt(string sql, params IDataParameter[] p)
        {
            object obj = ExecScalar(sql, p);
            if (obj == null) return -1;
            return int.Parse(obj.ToString());
        }

        public int ExecNonQuery(DbConnection conn, string sql, params IDataParameter[] p)
        {
            throw new NotImplementedException();
        }

        public int ExecScalarInt(DbConnection conn, string sql, params IDataParameter[] p)
        {
            throw new NotImplementedException();
        }


        public object ExecScalar(DbConnection conn, string sql, params IDataParameter[] p)
        {
            throw new NotImplementedException();
        }


        DbConnection IDb.GetConn()
        {
            throw new NotImplementedException();
        }

        public DbConnection GetConn(string connStr)
        {
            throw new NotImplementedException();
        }
    }
}