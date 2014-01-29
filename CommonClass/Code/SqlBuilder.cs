using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Data;
using CommonClass;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace CommonClass
{
    public class SqlBuilder
    {
        private int type = 0;
        private string tbname = "";
        private XmlDocument doc;
        string sp1 = "", sp2 = "", sp3 = "";

        /// <summary>
        /// 0 access,1 mysql
        /// </summary>
        /// <param name="type"></param>
        public SqlBuilder(int type, string tbname)
        {
            this.type = type;
            this.tbname = tbname;
            doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\DbMap\\" + tbname + ".xml");

            if (type == 0) { sp1 = "["; sp2 = "]"; sp3 = "@"; }
            else if (type == 1)
            {
                sp1 = sp2 = "`";
                sp3 = "?";
            }

        }

        public string Insert<T>(T t)
        {
            StringBuilder sb = new StringBuilder("insert into ");

            sb.AppendFormat(" {1}{0}{2} (", tbname, sp1, sp2);

            StringBuilder sbname = new StringBuilder();
            StringBuilder sbp = new StringBuilder();
            //add sql
            int i = 0;
            XmlNodeList xnl = doc.SelectNodes("//col");
            foreach (XmlNode xn in xnl)
            {
                string name = xn.Attributes["name"].InnerText;
                string dtype = xn.Attributes["type"].InnerText;
                if (i != xnl.Count - 1)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        sbname.AppendFormat("{1}{0}{2},", name, sp1, sp2);
                        sbp.AppendFormat("{1}{0},", name, sp3);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        sbname.AppendFormat("{1}{0}{2})values(", name, sp1, sp2);
                        sbp.AppendFormat("{1}{0})", name, sp3);
                    }
                }
                i++;
            }
            sb.AppendFormat(sbname.ToString()).AppendFormat(sbp.ToString())
                .AppendFormat("\r\n");

            //add parameter
            string prameter = AppendParameter<T>(xnl, t);
            sb.AppendFormat(prameter);
            return sb.ToString();
        }

        public string Update<T>(T t)
        {
            StringBuilder sb = new StringBuilder(" update  ");
            sb.AppendFormat("{1}{0}{2} set ", tbname, sp1, sp2);
            int i = 0;
            XmlNodeList xnl = doc.SelectNodes("//col");
            string key = "";
            foreach (XmlNode xn in xnl)
            {
                string name = xn.Attributes["name"].InnerText;
                string dtype = xn.Attributes["type"].InnerText;
                if (xn.Attributes["key"] != null)
                {
                    key = name;
                    i++;
                    continue;
                }

                if (i != xnl.Count - 1)
                {
                    sb.AppendFormat(" {1}{0}{2}={3}{0}, ", name, sp1, sp2, sp3);
                }
                else
                {
                    sb.AppendFormat(" {1}{0}{2}={3}{0}  where {5}{4}{6}={3}{4} \r\n", name, sp1, sp2, sp3, key, sp1, sp2);
                }
                i++;
            }
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("必须指定一个用于更新的键: key=\"1\" ");
            //..parameter
            string pStr = AppendParameter<T>(xnl, t);
            sb.AppendFormat(pStr);

            return sb.ToString();
        }

        #region apend parameter
        private string AppendParameter<T>(XmlNodeList xnl, T t)
        {
            IDbDataParameter[] ps = null;
            if (type == 0) ps = new OleDbParameter[xnl.Count];
            else if (type == 1) ps = new MySqlParameter[xnl.Count];
            IDbDataParameter p = null;

            StringBuilder sbparam = new StringBuilder();
            if (type == 0) { }
            else if (type == 1)
            {
                sbparam.AppendFormat(" IDbDataParameter []paras=new MySqlParameter[{0}]; \r\n", xnl.Count);
            }
            int i = 0;
            foreach (XmlNode xn in xnl)
            {
                string name = xn.Attributes["name"].InnerText;
                string dtype = xn.Attributes["type"].InnerText;
                if (type == 0) { p = new OleDbParameter(); }
                else if (type == 1)
                {
                    p = new MySqlParameter();
                }

                FillParameter<T>(ref p, name, dtype, t);
                ps[i] = p;
                if (type == 0) { }
                else if (type == 1)
                {
                    sbparam.AppendFormat(" paras[{0}]=new MySqlParameter(\"{1}\",{2});\r\n ", i, p.ParameterName, GetValDisplay(p.Value));
                    sbparam.AppendFormat(" paras[{0}].DbType={1};\r\n ", i, GetDbTypeDisplay(p.DbType));
                }
                i++;
            }
            return sbparam.ToString();
        }

        /// <summary>
        /// 对参数进行赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="para"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="t"></param>
        private void FillParameter<T>(ref IDbDataParameter para, string name, string type, T t)
        {
            PropertyInfo[] pis = t.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name.ToLower() == name.ToLower())
                {
                    para.ParameterName = string.Format("{1}{0}", pi.Name, sp3);
                    para.Value = pi.GetValue(t, null);
                    para.DbType = GetDbType(type);
                }
            }
        }
        private DbType GetDbType(string str)
        {
            str = str.ToLower();
            DbType dt = DbType.String;
            if (str == "string")
            {
                return DbType.String;
            }
            else if (str == "int") return DbType.Int32;
            else if (str == "float") return DbType.Decimal;
            else if (str == "date" || str == "datetime") return DbType.DateTime;

            return dt;
        }

        private object GetValDisplay(object val)
        {
            if (val.GetType() == typeof(String) || val.GetType() == typeof(DateTime))
            {
                return string.Format("\"" + val + "\"");
            }
            return val;
        }
        //private string GetDbTypeDisplay(string str)
        //{
        //    str = str.ToLower();
        //    string dt = "DbType.String";
        //    if (str == "string")
        //    {
        //        return "DbType.String";
        //    }
        //    else if (str == "int") return "DbType.Int32";
        //    else if (str == "float") return "DbType.Decimal";
        //    else if (str == "date" || str == "datetime") return "DbType.DateTime";

        //    return dt;
        //}
        private string GetDbTypeDisplay(DbType dtype)
        {
            if (dtype == DbType.String)
            {
                return "DbType.String";
            }
            else if (dtype == DbType.Int32)
            {
                return "DbType.Int32";
            }
            else if (dtype == DbType.Decimal)
            {
                return "DbType.Decimal";
            }
            else if (dtype == DbType.DateTime)
            {
                return "DbType.DateTime";
            }
            return "DbType.String";
        }
        #endregion

    }
}