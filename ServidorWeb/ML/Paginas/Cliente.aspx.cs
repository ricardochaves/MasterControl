using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.EntityContext;

namespace ServidorWeb.ML.Paginas
{
    public partial class Cliente : System.Web.UI.Page
    {

        NSAADMEntities n = EntityContextML.GetContext;


        protected void Page_Load(object sender, EventArgs e)
        {

            Decimal codigo = Convert.ToDecimal(Request.QueryString["code"]);

            var x = (from p in n.ML_Usuario where p.id == codigo select p);

            GridView1.DataSource = x;
            GridView1.DataBind();

            var usuario = (from p in n.ML_Usuario where p.id == codigo select p).First();
            var x1 = (from p in n.ML_Phone where p.id_Usuario == usuario.id select p);

            GridView2.DataSource = x1;
            GridView2.DataBind();

        }
    }
}