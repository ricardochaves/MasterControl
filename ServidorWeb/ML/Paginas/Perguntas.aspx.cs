using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.EntityContext;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;

namespace ServidorWeb.ML.Paginas
{
    public partial class Perguntas : System.Web.UI.Page
    {

        NSAADMEntities n = new NSAADMEntities();
        Meli m;

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Meli)Session["M"] == null)
            {
                Response.Redirect("InicioML.aspx");

            }
            else
            {
                Meli m1 = (Meli)Session["M"];
                if (m1.AccessToken == null)
                {
                    Response.Redirect("InicioML.aspx");
                }
            }

            m = (Meli)Session["M"];
            TextBox1.Text = m.AccessToken;

            var x = (from p in n.ML_Question where p.answered_questions == 0 select new { p.id, p.text });

            GridView1.DataSource = x;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            var x =  (from p in n.ML_Question where p.status == "UNANSWERED" select p );


            foreach (ML_Question item in x.ToList())
            {

                TratarPerguntas t = new TratarPerguntas(m, n, Convert.ToDecimal(item.id_question));

                t.executa();

            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["pagina"] = "Perguntas.aspx";
            
            Response.Redirect(m.GetAuthUrl(ServidorWeb.Properties.Settings.Default.URL_Login));
        }
    }
}