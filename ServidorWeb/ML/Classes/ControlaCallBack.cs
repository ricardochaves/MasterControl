using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaCallBack
    {
        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

        public List<CallBackML> RetornaCallBacks(TipoRetonaCallBacks tcallback, NSAADMEntities n)
        {
            try{
            
                List<CallBackML> lCall = new List<CallBackML>();
                if (tcallback == TipoRetonaCallBacks.PERGUNTAS)
                {
                    lCall = (from p in n.CallBackMLs where p.topic == "questions" select p).ToList();

                }
                else
                {
                    throw new Exception("Tipo de Retorno CallBack não implementado ou inválido.");
                }

                return lCall;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetornaCallBacks.", ex);
            }

        }

        public enum TipoRetonaCallBacks
        {
            PERGUNTAS = 0,
            ORDENS = 1,
            PAGAMENTOS = 2
        }

        public void LimpaCallBack()
        {
            ConstruirEF cf = new ConstruirEF();
            NSAADMEntities n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

            List<CallBackML> x = (from p in n.CallBackMLs where p.topic == "questions" select p).ToList();

            foreach (CallBackML item in x)
            {
                try
                {
                    decimal cod;

                    cod = Convert.ToDecimal(item.resource.Replace("/questions/",""));

                    var b = (from p in n.ML_Question where p.id_question == cod select p ).First();

                    n.CallBackMLs.DeleteObject(item);
                    

                }
                catch (Exception)
                {
                    
                    
                }

                n.SaveChanges();

            }

        }

    }
}