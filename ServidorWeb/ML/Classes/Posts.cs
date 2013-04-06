using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MercadoLibre.SDK;
using RestSharp;
using Newtonsoft.Json;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class Posts
    {

        private Meli m;

        public Posts(Meli meli)
        {
            m = meli;
            m.refreshToken();
        }
        
        public Posts(Object meli)
        {
            m = (Meli)meli;
            m.refreshToken();
        }


        public void ResponderPergunta(decimal id, string texto)
        {

            NSAADMEntities n;

            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);


            try
            {
                Parameter at = new Parameter();
                List<Parameter> param = new List<Parameter>();

                ML_Question mlQ = (from p in n.ML_Question where p.id == id select p).First();


                //Alimentando parametros
                at.Name = "access_token";
                at.Value = m.AccessToken;

                param.Add(at);

                m.Post("/answers", param, new { question_id = mlQ.id_question, text = texto });

                //DEPOIS DA CONVERSA ATUALIZAR O BD, VERIFICAR SE NÃO SERIA MELHOR PEGAR DE NOVO NO ML
                ML_Question q = (from p in n.ML_Question where p.id == id select p).First();
                q.answered_questions = 1;
                q.Answer_text = texto;
                q.Answer_status = "active";

                n.SaveChanges();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

  



        }
    
    }
}