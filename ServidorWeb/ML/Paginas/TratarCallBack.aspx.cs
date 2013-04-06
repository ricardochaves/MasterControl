using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;
using RestSharp;
using ServidorWeb.BD;


namespace ServidorWeb.ML.Paginas
{
    public partial class TratarCallBack : System.Web.UI.Page
    {

        Meli m;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack){

                try
                {
                    if ((Meli)Session["M"] != null)
                    {
                        TextBox2.Text = m.AccessToken;
                    }

                    TextBox1.Text = ServidorWeb.Properties.Settings.Default.URL_Login;

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no load do TrataCallBack", ex);
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Meli m = (Meli)Session["M"];


            ConstruirEF cf = new ConstruirEF();

            NSAADMEntities n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

            LerML ler = new LerML(m);
            ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

            

            var x = (from p in n.CallBackMLs where p.topic == "questions" select p);


            foreach (CallBackML c in x.ToList())
            {
                if (c.topic == "questions")
                {

                    Question q = ler.RetonarQuestion(c.resource);

                    TratarPerguntas t = new TratarPerguntas(m, n, c);
                    t.executa();

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                Session["pagina"] = "TratarCallBack.aspx";
                m = new Meli(5971480328026573, "HvQavElFhrbqlGCaTMWIrtQklsqnwlIM");

                Response.Redirect(m.GetAuthUrl(ServidorWeb.Properties.Settings.Default.URL_Login));

                Session["M"] = m;

                

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao relogar no TrataCallBack", ex);
            }



        }
    }
}