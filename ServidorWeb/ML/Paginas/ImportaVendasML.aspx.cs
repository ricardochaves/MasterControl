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
            try
            {
                cm = new ControlaMeli();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Load do ImportaVendasML.aspx.", ex);
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                Usuario u = cm.RetornaUsuario();

                ConverterObjetoMLparaEF c = new ConverterObjetoMLparaEF();

                ListOrder o = cm.RetornarOrdens(u, 0);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }

                }


                o = cm.RetornarOrdens(u, 50);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }
                }

                o = cm.RetornarOrdens(u, 100);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }
                }

                o = cm.RetornarOrdens(u, 150);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }
                }

                o = cm.RetornarOrdens(u, 200);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }
                }

                o = cm.RetornarOrdens(u, 250);

                foreach (Order or in o.results)
                {
                    try
                    {
                        ML_Order orde = (from p in cm.n.ML_Order where p.id == or.id select p).First();
                        c.AtualizaOrdem(orde, or, cm.n);
                    }
                    catch (InvalidOperationException)
                    {
                        c.ConverteOrdem(or, cm.n);
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Erro no rotina Button1_Click.", ex);
            }

        }
    }
}