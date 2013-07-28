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

        #region Basico
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
        private void FinalizaML(string aToken, string aRefres)
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
        #endregion

        #region Perguntas

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
                    throw new Exception(string.Format("Falha ao tentar recuperar a pergunta {0}  resp.StatusCode = {1} {0} resource = {2} {0}", Environment.NewLine, resp.StatusCode, resource));
                }

                

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarQuestion.", ex);
            }


        }
        public void RespondeQuestion(decimal idQuestion, string texto)
        {
            Posts p = new Posts(m, n);

            p.ResponderPergunta(idQuestion, texto);

            FinalizaML(m.AccessToken, m.RefreshToken);

        }

        #endregion

        #region usuario
        public Usuario RetornaUsuario()
        {

            try
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

                    FinalizaML(m.AccessToken, m.RefreshToken);
                    return u;
                }
                else
                {
                    throw new Exception(String.Format("Usuario não encontrado. {0} codigo: {1} {0}", Environment.NewLine));
                }

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Erro na rotina RetornaUsuario. {0} codigo: {1} {0}", Environment.NewLine), ex);
            }
        }
        public Usuario RetornaUsuario(string codigo)
        {
            return new Usuario();
        }
        #endregion

        #region Ordens
        public Order RetornaOrder(string codigo)
        {
            try
            {

                Order q;
                Parameter at = new Parameter();
                List<Parameter> param = new List<Parameter>();

                //Alimentando parametros
                at.Name = "access_token";
                at.Value = m.AccessToken;

                //Adicionando na lista
                param.Add(at);

                RestResponse resp = (RestResponse)m.Get(codigo, param);

                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var a = new JsonSerializerSettings();
                    q = JsonConvert.DeserializeObject<Order>(resp.Content);

                    FinalizaML(m.AccessToken, m.RefreshToken);

                    return q;
                }
                else
                {
                    throw new Exception(String.Format("Ordem não encontrada. {0} codigo: {1} {0}",Environment.NewLine, codigo));
                }

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Erro na rotina RetornaOrder: {0} codigo: {1} {0}", Environment.NewLine, codigo), ex);
            }
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

                FinalizaML(m.AccessToken, m.RefreshToken);

                return o;
            }
            else
            {
                throw new Exception("Falha ao tentar recuperar a lista de ordens");
            }


        }

        #endregion

        #region Itens

        public Item RetornaItem(string codigo)
        {
            try
            {

                Item q;
                Parameter at = new Parameter();
                //Parameter ItemID = new Parameter();
                List<Parameter> param = new List<Parameter>();

                //Alimentando parametros
                at.Name = "access_token";
                at.Value = m.AccessToken;

                //ItemID.Name = "item_id";
                //ItemID.Value = codigo;

                //Adicionando na lista
                //param.Add(ItemID);
                param.Add(at);

                RestResponse resp = (RestResponse)m.Get("items/" + codigo, param);

                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var a = new JsonSerializerSettings();
                    q = JsonConvert.DeserializeObject<Item>(resp.Content);

                    FinalizaML(m.AccessToken, m.RefreshToken);

                    return q;
                }
                else
                {
                    throw new Exception(String.Format("Item não encontrado. {0} codigo: {1} {0} resp.Content: {2}  {0} ", Environment.NewLine, codigo, resp.Content));
                }

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Erro na rotina RetornaItem: {0} codigo: {1} {0}", Environment.NewLine, codigo), ex);
            }

        }

        #endregion

    }
}