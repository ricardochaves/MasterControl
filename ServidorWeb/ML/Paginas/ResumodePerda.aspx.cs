using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;

namespace ServidorWeb.ML.Paginas
{
    public partial class ResumodePerda : System.Web.UI.Page
    {

        ControlaMeli cm;

        protected void Page_Load(object sender, EventArgs e)
        {
            cm = new ControlaMeli();


            var a = (from p in cm.n.ML_Payment where p.status != "approved" select p);

            GridView1.DataSource = a;
            GridView1.DataBind();







        }
    }
}