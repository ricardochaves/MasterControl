using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MercadoLibre.SDK;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;


namespace ServidorWeb.ML.Paginas
{
    public partial class ImportaVendasML : System.Web.UI.Page
    {
        

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
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Meli m = (Meli)Session["M"];

            LerML l = new LerML(m);
            Usuario u = l.RetornaUsuarioLogado();

            NSAADMEntities n;
            
            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

            ConverterObjetoMLparaEF c = new ConverterObjetoMLparaEF();

            ListOrder o = l.RetornarOrdens(u, 0);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }

            }


            o = l.RetornarOrdens(u, 50);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }
            }

            o = l.RetornarOrdens(u, 100);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }
            }

            o = l.RetornarOrdens(u, 150);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }
            }

            o = l.RetornarOrdens(u, 200);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }
            }

            o = l.RetornarOrdens(u, 250);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in n.ML_Order where p.id == or.id select p).First();
                    c.AtualizaOrdem(orde, or);
                }
                catch (InvalidOperationException)
                {
                    c.ConverteOrdem(or);
                }
            }

        }
    }
}