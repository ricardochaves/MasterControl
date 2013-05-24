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

        ControlaMeli cm;
        ControlaPerguntas cp = new ControlaPerguntas();
        ControlaCallBack cb = new ControlaCallBack();

        protected void Page_Load(object sender, EventArgs e)
        {
            cm = new ControlaMeli();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            LerML l = new LerML(cm.m);
            Usuario u = cm.RetornaUsuario( );

            ConverterObjetoMLparaEF c = new ConverterObjetoMLparaEF();

            ListOrder o = l.RetornarOrdens(u, 0);

            foreach (Order or in o.results)
            {
                try
                {
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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
                    ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
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