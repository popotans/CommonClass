using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonClass.Contract;
using CommonClass.Domain;

namespace CommonClass.API
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IClass dclass = new DefaultClass();

            List<IClass> list = dclass.GetAll(1);
            foreach (IClass c in list)
            {
                Response.Write(c.Title + "<br/>");
            }
        }
    }
}