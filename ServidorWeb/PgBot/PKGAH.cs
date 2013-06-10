using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public class PKGAH
    {

        private StaticDados sd = new StaticDados();

        private LastModified lastModified(string Realm)
        {
            AcessoWEB aweb = new AcessoWEB();
            string jlastModified;
            jlastModified = aweb.RecuperaJSON(sd.host + sd.AHData + Realm);
            return ConverteJsontoLastModified(jlastModified);
        }

        private LastModified ConverteJsontoLastModified(string jItem)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var Obj = (Dictionary<string, object>)js.DeserializeObject(jItem);
            LastModified Novo = new LastModified();

            var teste = Obj["files"];

            var teste1 = ((Object[])teste)[0];

            Novo.url = ((Dictionary<string, object>)teste1)["url"].ToString();
            Novo.lastModified = ((Dictionary<string, object>)teste1)["lastModified"].ToString();

            return Novo;
        }

        private string RetornaJsonAuction(LastModified l)
        {
            
            AcessoWEB aweb = new AcessoWEB();
             return aweb.RecuperaJSON(l.url);

        }

        private void AtualizaTodosItens(string JsonAuction)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 50000000;
            PKGItens pkg = new PKGItens();
            List<string> l = new List<string>();

            var Obj = (Dictionary<string, object>)js.DeserializeObject(JsonAuction);

            var Aucally = (Dictionary<string, object>)Obj["alliance"];
            Object[] Auct = (Object[])Aucally["auctions"];

            foreach (Dictionary<string, object> item in Auct)
            {

                if (!l.Any(x => x == item["item"].ToString()))
                {
                    pkg.AtualizarItem(item["item"].ToString());
                    l.Add(item["item"].ToString());
                }
            }
        }

        public void AtualizaTudo()
        {
            LastModified l = new LastModified();
            l = lastModified(sd.NEMESIS);
            AtualizaTodosItens(RetornaJsonAuction(l));

        }
    }
}