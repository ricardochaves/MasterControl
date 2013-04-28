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
        ML_Item mlItem;
        ControlaPerguntas cp = new ControlaPerguntas();
        ControlaProdutos cprod = new ControlaProdutos();
        ControlaMeli cm;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                cm = new ControlaMeli();
                codigo = Convert.ToDecimal(Request.QueryString["code"]);


                TextBox2.Text = " Não deixe de ver nossos outros produtos. Muito Obrigado.";


                mlq = cp.RetornaPergunta(codigo, cm.n);
                mlItem = cprod.RetornaProduto(mlq.item_id, cm.n);

                Label1.Text = mlItem.title;

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

                decimal[] ids = {codigo};
                
                var OutrasPerguntas = cp.RetonaPerguntas(Convert.ToDecimal(mlq.id_from), cm.n);
                var a = (from p in OutrasPerguntas where !ids.Contains(Convert.ToDecimal(p.id_question)) select new { p.text, p.Answer_text });
                GridView1.DataSource = a;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina Page_Load", ex);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                cm.RespondeQuestion(codigo, TextBox1.Text + txtResposta.Text + TextBox2.Text);
                Response.Redirect("Perguntas.aspx");



            }
            catch (Exception ex)
            {
                
                throw new Exception(String.Format("Erro ao tentar responder pergunta. {0} codigo: {1} {0}",Environment.NewLine,codigo),ex);
            }


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
    }
}