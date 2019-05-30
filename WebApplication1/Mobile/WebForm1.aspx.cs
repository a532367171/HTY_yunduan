using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Mobile
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Server.Transfer("http://localhost:65061/Mobile/api.ashx?u=bj001&p=000&t=login&mtype=3&uuid=xxx", true);
            //Response.Write("http://localhost:65061/Mobile/api.ashx?u=bj001&p=000&t=login&mtype=3&uuid=xxx");
            //Response.End();

            Response.Redirect("http://localhost:65061/Mobile/api.ashx?u=bj001&p=000&t=login&mtype=3&uuid=xxx", true);

        }
    }
}