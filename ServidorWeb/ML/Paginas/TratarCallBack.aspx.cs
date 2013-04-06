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
using ServidorWeb.EntityContext;

namespace ServidorWeb.ML.Paginas
{
    public partial class TratarCallBack : System.Web.UI.Page
    {

        Meli m;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //if ((Meli)Session["M"] == null)
                //{
                //    Response.Redirect("InicioML.aspx");

                //}
                //else
                //{
                //    m = (Meli)Session["M"];

                TextBox1.Text = ServidorWeb.Properties.Settings.Default.URL_Login;
                //   // TextBox2.Text = m.RefreshToken;

                //}

            }
            catch (Exception)
            {
                
                //throw new Exception(ex.Message, ex);

                
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Meli m = (Meli)Session["M"];

            NSAADMEntities n = new NSAADMEntities();

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

            Session["pagina"] = "TratarCallBack.aspx";
            m = new Meli(5971480328026573, "HvQavElFhrbqlGCaTMWIrtQklsqnwlIM");

            Response.Redirect(m.GetAuthUrl(ServidorWeb.Properties.Settings.Default.URL_Login));

            TextBox1.Text = m.AccessToken;
            TextBox2.Text = m.RefreshToken;

        }
    }
}