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
        ControlaPerguntas cp = new ControlaPerguntas();
        ControlaMeli cm;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                cm = new ControlaMeli();
                codigo = Convert.ToDecimal(Request.QueryString["code"]);


                TextBox2.Text = " Não deixe de ver nossos outros produto. Muito Obrigado.";


                mlq = cp.RetornaPergunta(codigo, cm.n);


                txtPergunta.Text = mlq.text;


                try
                {
                    var y = cm.RetornaUsuario(mlq.id_from.ToString());
                    TextBox1.Text = "Olá " + y.nickname + ". ";
                }
                catch (Exception)
                {

                    TextBox1.Text = "Olá amigo, ";
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina Page_Load", ex);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cm.RespondeQuestion(codigo, TextBox1.Text + txtResposta.Text + TextBox2.Text);

        }
    }
}