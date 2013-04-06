using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServidorWeb.Estruturas;
using ServidorWeb.BD;

namespace ServidorWeb
{
    /// <summary>
    /// Summary description for WSBot
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSBot : System.Web.Services.WebService
    {

        [WebMethod]
        public string RecebeDados(estDados datax, string key)
        {

            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }

            BotWoWEntities b = EntityContextBot.GetContext;
            dado d = new dado();

            d.xp = datax.xp;
            d.xp_needed = datax.xp_needed;
            d.name = datax.name;

            b.dados.AddObject(d);

            b.SaveChanges();

            return "Sucesso";
        }

        [WebMethod]
        public decimal IniciaSessao(estSessao s, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }
            BotWoWEntities b = EntityContextBot.GetContext;
            Sessao s1 = new Sessao();
            s1.bot = s.bot;
            s1.dt_inicio = s.dtInicio;
            s1.nome = s.nome;
            s1.profile = s.profile;
            s1.dirWoW = s.dirWoW;
            s1.lvl = s.lvl;
            s1.versao = s.versao;
            s1.server = s.server;


            b.Sessaos.AddObject(s1);
            b.SaveChanges();

            return s1.id;
        }

        [WebMethod]
        public void FinalizaSessao(estSessao s, DateTime d, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }
            BotWoWEntities b = EntityContextBot.GetContext;

            Sessao s1 = (from p in b.Sessaos where p.id == s.id select p).First();

            s1.dt_fim = d;

            b.SaveChanges();

        }

        [WebMethod]
        public void IncluiNovaMorte(estSessao s, estMorte m, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }
            BotWoWEntities b = EntityContextBot.GetContext;
            Morte m1 = new Morte();
            m1.mortes = m.mortes;
            m1.morteshora = m.morteshora;
            m1.id_sessao = s.id;
            m1.dt = m.dt;
            m1.RealZoneText = m.RealZoneText;
            m1.SubZoneText = m.SubZoneText;

            b.Mortes.AddObject(m1);
            b.SaveChanges();
        }

        [WebMethod]
        public void IncluirLevelUp(estSessao s, estLevelUp u, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }
            BotWoWEntities b = EntityContextBot.GetContext;
            levelup l = new levelup();
            l.dt = u.data;
            l.lvl = u.lvl;
            l.id_sessao = s.id;

            b.levelups.AddObject(l);
            b.SaveChanges();
        }

        [WebMethod]
        public void IncluirLoot(estSessao s, estLoot l, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }

            BotWoWEntities b = EntityContextBot.GetContext;
            loot l1 = new loot();

            l1.dt = l.data;
            l1.id_Item = l.idItem;
            l1.qtd = l.qtd;
            l1.id_sessao = s.id;

            b.loots.AddObject(l1);
            b.SaveChanges();

        }

        [WebMethod]
        public void IncluirDetalhe(estSessao s, estDetalhe d, string key)
        {
            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }

            BotWoWEntities b = EntityContextBot.GetContext;
            detalhe d1 = new detalhe();

            d1.bglost = Convert.ToDecimal(d.bglost);
            d1.bgwin = Convert.ToDecimal(d.bgwin);
            d1.gold = Convert.ToDecimal(d.gold);
            d1.honor = Convert.ToDecimal(d.honor);
            d1.honorh = Convert.ToDecimal(d.honorh);
            d1.id_sessao = s.id;
            d1.kills = Convert.ToDecimal(d.kills);
            d1.killsh = Convert.ToDecimal(d.killsh);
            //d1.nodeh = Convert.ToDecimal(d.nodeh);
            d1.runningtime = Convert.ToDecimal(d.runningtime);
            d1.timetolevel = Convert.ToDecimal(d.timetolevel);
            d1.xp_needed = Convert.ToDecimal(d.xp_needed);
            d1.xp = Convert.ToDecimal(d.xp);
            d1.xph = Convert.ToDecimal(d.xph);
            d1.lvl = Convert.ToDecimal(d.lvl);
            d1.RealZoneText = d.RealZoneText;
            d1.SubZoneText = d.SubZoneText;

            b.detalhes.AddObject(d1);
            b.SaveChanges();
        }

        [WebMethod]
        public Morte Teste(string key)
        {

            if (key != "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43")
            {
                throw new Exception("Chave inválida");
            }

            BotWoWEntities b = EntityContextBot.GetContext;

            Morte m;

            m = (from p in b.Mortes select p).First();

            return m;

        }
    }
}
