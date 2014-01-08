using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Contract
{
    public class AccessDB : IDB
    {
        public AccessDB(string path)
        {
            this.ConnStr = path;
        }
        public object Insert(string sql, params System.Data.IDataParameter[] param)
        {
            throw new NotImplementedException();
        }

        public int Update(string sql, params System.Data.IDataParameter[] param)
        {
            throw new NotImplementedException();
        }

        public void Delete(string sql, params System.Data.IDataParameter[] param)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDataTable(string sql, params System.Data.IDataParameter[] param)
        {
            throw new NotImplementedException();
        }

        public System.Data.IDataReader GetReader(string sql, params System.Data.IDataParameter[] param)
        {
            throw new NotImplementedException();
        }

        public string ConnStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}