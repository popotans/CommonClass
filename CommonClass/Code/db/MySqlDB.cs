using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;
namespace CommonClass
{
    public class MySqlDB : IDb
    {
        public MySqlDB(string connstr)
        {
            this.ConnStr = connstr;
        }

        MySqlConnection GetConnection()
        {
            return new MySqlConnection(this.ConnStr);
        }

        public IDataReader GetReader(string sql, params IDataParameter[] p)
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.Connection = conn;
            conn.Open();
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public DataTable GetTable(string sql, params IDataParameter[] p)
        {
            throw new NotImplementedException();
        }

        public int ExecNonQuery(string sql, params IDataParameter[] p)
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.Connection = conn;
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        public object ExecScalar(string sql, params IDataParameter[] p)
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
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

        public string ConnStr
        {
            get
           ;
            set;
        }
        public IDbDataParameter GetParam(string name, object val, MySqlDbType t)
        {
            MySqlParameter p = new MySqlParameter(name, val);
            p.MySqlDbType = t;
            return p;
        }

        public IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<MySqlDbType> t)
        {
            IDbDataParameter[] arr = new MySqlParameter[names.Count];
            if (names.Count != vals.Count && names.Count != t.Count) throw new ApplicationException("参数不匹配");
            for (int i = 0; i < names.Count; i++)
            {
                arr[i] = GetParam(names[i], vals[i], t[i]);
            }
            return arr;
        }

        public IDbDataParameter GetParam(string name, object val, DbType t)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<DbType> t)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter[] GetParams(Dictionary<string, MySqlDbType> dic, List<object> vals)
        {
            IDbDataParameter[] arr = new MySqlParameter[dic.Count];
            int i = 0;
            foreach (KeyValuePair<string, MySqlDbType> item in dic)
            {
                arr[i] = GetParam(item.Key, vals[i], item.Value);
                i++;
            }
            return arr;
        }

        public int ExecNonQuery(DbConnection conn, string sql, params IDataParameter[] p)
        {
            // MySqlConnection conn = GetConnection();
            DbCommand cmd = new MySqlCommand(sql);
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            int i = cmd.ExecuteNonQuery();
            //  cmd.Connection.Close();
            return i;
        }

        public int ExecScalarInt(DbConnection conn, string sql, params IDataParameter[] p)
        {
            object obj = ExecScalar(conn, sql, p);
            if (obj == null) return -1;
            return int.Parse(obj.ToString());
        }


        public object ExecScalar(DbConnection conn, string sql, params IDataParameter[] p)
        {
            //MySqlConnection conn = GetConnection();
            DbCommand cmd = new MySqlCommand(sql);
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            object obj = cmd.ExecuteScalar();
            //cmd.Connection.Close();
            if (obj != null && obj != DBNull.Value) return obj;
            return null;
        }


        public DbConnection GetConn()
        {
            return GetConn(ConnStr);
        }

        public DbConnection GetConn(string connStr)
        {
            return new MySqlConnection(connStr);
        }
    }
}