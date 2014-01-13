﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
namespace CommonClass.Code
{
    public class MySqlDB : IDb
    {
        public MySqlDB(string connstr)
        {
            this.ConnStr = connstr;
        }

        MySqlConnection GetConn()
        {
            return new MySqlConnection(this.ConnStr);
        }

        public IDataReader GetReader(string sql, params IDataParameter[] p)
        {
            MySqlConnection conn = GetConn();
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
            MySqlConnection conn = GetConn();
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
            MySqlConnection conn = GetConn();
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
    }
}