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
            PKGItens pkg = new PKGItens();
            List<string> l = new List<string>();

            Object[] Auct = RetornaArraydeAuctions(JsonAuction);

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

        public void AtualizaTudoValorItens()
        {

            PKGItens pkg = new PKGItens();
            List<string> l = new List<string>();
            List<Auction> ac = new List<Auction>();
            LastModified las = new LastModified();
            Object[] Auct;
            decimal valor;

            las = lastModified(sd.NEMESIS);
            Auct = RetornaArraydeAuctions(RetornaJsonAuction(las));

            foreach (Dictionary<string, object> item in Auct)
            {
                Auction a = new Auction();
                a.bid = Convert.ToDecimal(item["bid"].ToString());
                a.buyout = Convert.ToDecimal(item["buyout"].ToString());
                a.idItem = Convert.ToInt32(item["item"].ToString());
                ac.Add(a);
            }

            foreach (Dictionary<string, object> item in Auct)
            {

                if (!l.Any(x => x == item["item"].ToString()))
                {

                    valor = (from p in ac where p.idItem == Convert.ToInt32(item["item"].ToString()) orderby p.buyout select p.buyout).First();

                    pkg.AtualizaValorItem(Convert.ToDecimal(item["item"].ToString()), valor);
                    l.Add(item["item"].ToString());
                }
            }
        }

        private Object[] RetornaArraydeAuctions(string JsonAuction)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 50000000;
            PKGItens pkg = new PKGItens();
            List<string> l = new List<string>();

            var Obj = (Dictionary<string, object>)js.DeserializeObject(JsonAuction);

            var Aucally = (Dictionary<string, object>)Obj["alliance"];
            Object[] Auct = (Object[])Aucally["auctions"];
            return Auct;
        }


    }


}