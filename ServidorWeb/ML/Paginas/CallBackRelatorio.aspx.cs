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
    public partial class CallBackRelatorio : System.Web.UI.Page
    {

        NSAADMEntities n;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ConstruirEF cf = new ConstruirEF();
                n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int primeiro = Convert.ToInt16(TextBox1.Text);
            int segundo = Convert.ToInt16(TextBox2.Text);



            var b = (from p in n.CallBackMLs where p.id >= primeiro && p.id <= segundo select p);

            GridView1.DataSource = b;
            GridView1.DataBind();

        }
    }
}