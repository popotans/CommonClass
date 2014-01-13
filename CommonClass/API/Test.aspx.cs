using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonClass
{
    public partial class Test : System.Web.UI.Page
    {
        BaseClass bcd = new BaseClass(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\class.mdb");
        protected void Page_Load(object sender, EventArgs e)
        {
            btnInsert.Click += new EventHandler(btnInsert_Click);

        }

        void btnInsert_Click(object sender, EventArgs e)
        {
            int id = bcd.Insert(new ClassInfo()
              {
                  P1 = 0,
                  P2 = 0,
                  Title = "关于我们"
              });

            Response.Write("insert suc,idx is =" + id);
        }
    }
}