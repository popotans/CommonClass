using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace CommonClass.Contract
{
    public interface IDB
    {
        object Insert(string sql, params IDataParameter[] param);
        int Update(string sql, params IDataParameter[] param);
        void Delete(string sql, params IDataParameter[] param);
        System.Data.DataTable GetDataTable(string sql, params IDataParameter[] param);
        IDataReader GetReader(string sql, params IDataParameter[] param);

        string ConnStr { get; set; }
    }
}