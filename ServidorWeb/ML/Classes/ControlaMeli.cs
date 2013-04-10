using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;
using MercadoLibre.SDK;
using RestSharp;
using Newtonsoft.Json;

namespace ServidorWeb.ML.Classes
{
    public class ControlaMeli
    {

        public NSAADMEntities n;
        public Meli m;
        private DadosML d;

        public ControlaMeli()
        {

            try
            {
                ConstruirEF cf = new ConstruirEF();
                n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);
                d = (from p in n.DadosMLs where p.id == "Meli" select p).First();
                m = new Meli(Convert.ToInt64(d.ClientId), d.ClientSecret, d.AccessToken, d.RefreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro no New do ControlaMeli"), ex);
            }


            
        }

        public Question RetonarQuestion(string resource)
        {

            try
            {
                Question q;
                Parameter at = new Parameter();
                List<Parameter> param = new List<Parameter>();


                //Alimentando parametros
                at.Name = "access_token";
                at.Value = m.AccessToken;

                param.Add(at);

                RestResponse resp = (RestResponse)m.Get(resource, param);

                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var a = new JsonSerializerSettings();
                    q = JsonConvert.DeserializeObject<Question>(resp.Content);

                    FinalizaML(m.AccessToken, m.RefreshToken);

                    return q;
                    

                }
                else
                {
                    throw new Exception("Falha ao tentar recuperar a pergunta");
                }

                
                
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarQuestion.", ex);
            }


        }

        private void FinalizaML ( string aToken, string aRefres)
        {
            try
            {
                d.AccessToken = aToken;
                d.RefreshToken = aRefres;

                n.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro no FinalizaML.", ex);
            }



        }



    }
}