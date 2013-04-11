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
        
        Decimal codigo;
        ML_Question mlq;
        ControlaMeli cm;
        ControlaPerguntas cp = new ControlaPerguntas();

        protected void Page_Load(object sender, EventArgs e)
        {

            cm = new ControlaMeli();
            codigo = Convert.ToDecimal(Request.QueryString["code"]);
            TextBox2.Text = " Não deixe de ver nossos outros produto. Muito Obrigado.";

            mlq = cp.RetornaQuestion(codigo, cm.n);

            txtPergunta.Text = mlq.text;

            //try
            //{
            //    var y = (from a in n.ML_Usuario where a.id == mlq.id_from select a).First();
            //    TextBox1.Text = "Olá " + y.nickname + ". ";
            //}
            //catch (Exception)
            //{

                TextBox1.Text = "Olá amigo, ";
            //}
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Posts p = new Posts(Session["M"]);
            p.ResponderPergunta(codigo, TextBox1.Text + txtResposta.Text + TextBox2.Text);

        }
    }
}