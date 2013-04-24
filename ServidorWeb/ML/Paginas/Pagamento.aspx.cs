using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;

namespace ServidorWeb.ML.Paginas
{
    public partial class Pagamento : System.Web.UI.Page
    {

        NSAADMEntities n;

        protected void Page_Load(object sender, EventArgs e)
        {

            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);


            Decimal codigo = Convert.ToDecimal(Request.QueryString["code"]);

            var x = (from p in n.MP_Payments where p.id == codigo select p);

            GridView1.DataSource = x;
            GridView1.DataBind();
        }
    }
}