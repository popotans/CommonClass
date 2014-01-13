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
        IDbConnection GetConn1();
        IDbDataParameter GetParam(string name, object val, DbType t);
        IDbDataParameter[] GetParams(List<string> names, List<object> vals, List<DbType> t);

    }
}