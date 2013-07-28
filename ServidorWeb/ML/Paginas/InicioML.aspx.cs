using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MercadoLibre.SDK;
using ServidorWeb.ML.Classes;

namespace ServidorWeb.ML.Paginas
{
    public partial class Inicio : System.Web.UI.Page
    {

        
        Meli m;

        protected void Page_Load(object sender, EventArgs e)
        {

            //try
            //{


            //    if ((Meli)Session["M"] == null)
            //    {

            //        Session["M"] = new Meli(5971480328026573, "HvQavElFhrbqlGCaTMWIrtQklsqnwlIM");
                   
            //    }

            //    m = (Meli)Session["M"];

            //    if (m.AccessToken == null)
            //    {

            //        Session["pagina"] = "InicioML.aspx";

            //        string url = m.GetAuthUrl(ServidorWeb.Properties.Settings.Default.URL_Login);

            //        //Server.Transfer(url,true);
            //        Response.Redirect(url,false);

            //    }

            //    TextBox1.Text = m.AccessToken;
            //    //m.refreshToken();
            //    //TextBox2.Text = m.RefreshToken;

            //}
            //catch (Exception ex)
            //{
                
            //    throw new Exception("Erro no Load",ex) ;
            //}


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //m = new Meli(5971480328026573, "HvQavElFhrbqlGCaTMWIrtQklsqnwlIM");

            //Session["M"] = m;
            //Session["pagina"] = "ImportaVendasML.aspx";

            //string redirectUrl = m.GetAuthUrl(ServidorWeb.Properties.Settings.Default.URL_Login);
            ////string redirectUrl = m.GetAuthUrl("http://localhost:45506/ML/Paginas/LoginML.aspx");
            ////string redirectUrl = m.GetAuthUrl("http://www.nerdsa.com.br/ML/Paginas/LoginML.aspx");
            //Response.Redirect(redirectUrl);

            Response.Redirect("ImportaVendasML.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportaMovimentacaoMP.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("VendasProduto.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResumoPagamentoItem.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perguntas.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("TratarCallBack.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Session["pagina"] = "InicioML.aspx";

            m = new Meli(AplicacaoML.RetornaIDAplicacaoML(), AplicacaoML.RetornaKeyAplicacaoML());

            Response.Redirect(m.GetAuthUrl(AplicacaoML.RetornaURLLoginAplicacaoML()));



        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResumodePerda.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                ControlaMeli cm = new ControlaMeli();
                ControlaOrdens co = new ControlaOrdens();
                ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();
                co.GravaOrdem(cm.RetornaOrder("/orders/779622738"), cm.n);
            }
            catch (Exception ex)
            {

                throw new Exception("Erro no Button9_Click.", ex);
            }

        }

 
    }
}
