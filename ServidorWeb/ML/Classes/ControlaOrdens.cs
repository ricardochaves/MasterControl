using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaOrdens
    {
        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

        public ML_Order RetonarOrder(Order o, NSAADMEntities n)
        {
            try
            {

                ML_Order mo = (from p in n.ML_Order where p.id == o.id select p).First();

                return mo;

            }
            catch (Exception ex)
            {
                
                throw new Exception ("Erro na rotina RetonarOrder.",ex);
            }
        }

        public List<ML_Order> RetornaOrdens(TipoRetonaOrdens to, NSAADMEntities n)
        {
            try
            {
                List<ML_Order> mo = new List<ML_Order>();

                if (to == TipoRetonaOrdens.ABERTASPAGAS)
                {
                    mo = (from p in n.ML_Order where p.status == "paid" select p).ToList();
                }
                else
                {
                    throw new Exception("Tipo de Retorna Order não implementado ou inválido.");
                }
                return mo;
            }
            catch (Exception ex)
            {
                
                throw new Exception ("Erro na rotina RetornaOrdens.",ex);
            }
        }

        public void GravaOrdem(ML_Order o, NSAADMEntities n)
        {
            try
            {
                var or = (from p in n.ML_Order where p.id == o.id select p).First();

                or.status = o.status;
                or.date_closed = o.date_closed;
                or.status_detail = o.status_detail;

                //or.ML_Payment.Load();

                //if (o.ML_Payment != null)
                //{
                    
                //}

                n.SaveChanges();

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina GravaOrdem.", ex);
            }
        }
        public void GravaOrdem(Order o, NSAADMEntities n)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina GravaOrdem.", ex);
            }
        }

    }

    public enum TipoRetonaOrdens
    {
        ABERTAS = 0,
        FECHADAS = 1,
        ABERTASPAGAS = 2,
        ABERTASNAOPAGAS = 3,
    }


}