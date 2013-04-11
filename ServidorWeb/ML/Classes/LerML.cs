using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MercadoLibre.SDK;
using RestSharp;
using Newtonsoft.Json;

namespace ServidorWeb.ML.Classes
{
    public class LerML
    {
        private Meli m;

        public LerML(long ClientId, string ClientSecret)
        {
            m = new Meli(ClientId, ClientSecret);
        }

        public LerML(Meli meli)
        {
            m = meli;
        }

        public Usuario RetornaUsuarioLogado()
        {
            Usuario u = new Usuario();
            Parameter at = new Parameter();
            List<Parameter> param = new List<Parameter>();

            //Alimentando parametros
            at.Name = "access_token";
            at.Value = m.AccessToken;

            //Adicionando na lista
            param.Add(at);

            RestResponse resp = (RestResponse)m.Get("/users/me", param);

            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var a = new JsonSerializerSettings();
                u = JsonConvert.DeserializeObject<Usuario>(resp.Content);
            }

            return u;
        }

        public ListOrder RetornarOrdens(Usuario u, Int32 ofset)
        {
            ListOrder o;
            Parameter at = new Parameter();
            Parameter seller = new Parameter();
            Parameter offset = new Parameter();
            List<Parameter> param = new List<Parameter>();

            //Alimentando parametros
            at.Name = "access_token";
            at.Value = m.AccessToken;

            seller.Name = "seller";
            seller.Value = u.id;

            offset.Name = "offset";
            offset.Value = ofset;

            //Adicionando na lista
            param.Add(seller);
            param.Add(at);
            param.Add(offset);



            RestResponse resp = (RestResponse)m.Get("/orders/search", param);
            //offset
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var a = new JsonSerializerSettings();
                o = JsonConvert.DeserializeObject<ListOrder>(resp.Content);

                return o;
            }
            else
            {
                throw new Exception("Falha ao tentar recuperar a lista de ordens");
            }


        }
        
        public Usuario RetornaUsuario(string id)
        {
            Usuario u;
            Parameter at = new Parameter();
            Parameter at1 = new Parameter();
            List<Parameter> param = new List<Parameter>();


            //Alimentando parametros
            //at1.Name = "access_token";
            //at1.Value = m.AccessToken;

            at.Name = "user_id";
            at.Value = id;

            param.Add(at);
            //param.Add(at1);

            RestResponse resp = (RestResponse)m.Get("/users/" + id);


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var a = new JsonSerializerSettings();
                u = JsonConvert.DeserializeObject<Usuario>(resp.Content);

                return u;
            }
            else
            {
                throw new Exception("Falha ao tentar recuperar a Usuario");
            }
        }
    }
}