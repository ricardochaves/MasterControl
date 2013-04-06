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
    public partial class Pergunta : System.Web.UI.Page
    {
        NSAADMEntities n;
        Decimal codigo;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

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


            TextBox2.Text = " Não deixe de ver nossos outros produto. Muito Obrigado.";
            

            codigo = Convert.ToDecimal(Request.QueryString["code"]);

            var x = (from p in n.ML_Question where p.id == codigo select p).First();


            txtPergunta.Text = x.text;

            try
            {
                var y = (from a in n.ML_Usuario where a.id == x.id_from select a).First();
                TextBox1.Text = "Olá " + y.nickname + ". ";
            }
            catch (Exception)
            {

                TextBox1.Text = "Olá amigo, ";
            }
            

            


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Posts p = new Posts(Session["M"]);
            p.ResponderPergunta(codigo, TextBox1.Text + txtResposta.Text + TextBox2.Text);
        
        }
    }
}