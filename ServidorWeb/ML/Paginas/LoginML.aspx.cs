using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MercadoLibre.SDK;

namespace ServidorWeb.ML.Paginas
{
    public partial class LoginML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                Meli m;
                string pg;

                string codigo = Request.QueryString["code"];

                if ((string)Session["pagina"] != null)
                {
                    pg = (string)Session["pagina"];
                }
                else
                {
                    pg = "InicioML.aspx";
                }

                if (Session["M"] == null)
                {
                    m = new Meli(5971480328026573, "HvQavElFhrbqlGCaTMWIrtQklsqnwlIM");
                }
                else
                {
                    m = (Meli)Session["M"];
                }
            
                string redirectUrl = ServidorWeb.Properties.Settings.Default.URL_Login;
                m.Authorize(codigo, redirectUrl);

                Session["M"] = m;

                Response.Redirect(pg);

            }
            catch (Exception ex)
            {
                
                throw new Exception ("Erro no load do login.aspx", ex) ;
            }



        }
    }
}