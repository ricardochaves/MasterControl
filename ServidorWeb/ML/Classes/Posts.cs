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
        private NSAADMEntities n;

        public Posts(Meli meli, NSAADMEntities nsaadm)
        {
            m = meli;
            n = nsaadm;
        }

        public void ResponderPergunta(decimal id, string texto)
        {

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


            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina ResponderPergunta", ex);
            }





        }



    }
}
