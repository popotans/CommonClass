using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
namespace CommonClass
{
    public interface IDb
    {
        IDataReader GetReader(string sql, params IDataParameter[] p);
        DataTable GetTable(string sql, params IDataParameter[] p);
        int ExecNonQuery(string sql, params IDataParameter[] p);
        object ExecScalar(string sql, params IDataParameter[] p);
        int ExecScalarInt(string sql, params IDataParameter[] p);
        string ConnStr { get; set; }

        IDbDataParameter GetParam(string name, object val, DbType t);
        IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<DbType> t);

        IDbDataParameter GetParam(string name, object val, MySql.Data.MySqlClient.MySqlDbType t);
        IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<MySql.Data.MySqlClient.MySqlDbType> t);

    }
}