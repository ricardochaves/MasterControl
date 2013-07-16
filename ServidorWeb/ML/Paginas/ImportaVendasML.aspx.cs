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

                int cont = 0;
                WSNerdMoney.NerdMoney w = new WSNerdMoney.NerdMoney();
                ConstruirEF cef = new ConstruirEF();
                NSAADMEntities n = (NSAADMEntities)cef.RecuperaEntity(Entities.MercadoLivre);
                ConverterObjetoMLparaEF cf = new ConverterObjetoMLparaEF();
                Usuario u = cm.RetornaUsuario();
                ConverterObjetoMLparaEF c = new ConverterObjetoMLparaEF();
                ListOrder o;

                do
                {
                    o = cm.RetornarOrdens(u, cont);
                    cont = o.results.Count + cont;

                    foreach (Order or in o.results)
                    {
                        ML_Order teste =  cf.ConverteOrdem2(or, n);
                        n.ML_Order.AddObject(teste);
                        n.SaveChanges();
                    }
                } while (o.results.Count == 50);
     

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina Button1_Click",ex);
            }
        }
    }
}
