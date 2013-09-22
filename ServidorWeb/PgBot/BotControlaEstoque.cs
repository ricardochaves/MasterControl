using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public class BotControlaEstoque
    {
        public void AtualizarEstoque(BotItemEstoque estoque)
        {

            BotWoWEntities n = new BotWoWEntities();
            Estoque est = new Estoque();

            try
            {
                est = (from p in n.Estoques where p.idItem == estoque.itemID && p.NomePersonagem == estoque.Personagem select p).First();

                est.Qtd = estoque.Qtd;
                est.dtAtualizado = DateTime.Now;

                n.SaveChanges();

            }
            catch (InvalidOperationException)
            {
                est.idItem = estoque.itemID;
                est.NomePersonagem = estoque.Personagem;
                est.Qtd = estoque.Qtd;
                est.dtAtualizado = DateTime.Now;
                est.dtFabricado = DateTime.Now;

                n.Estoques.AddObject(est);
                n.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na rotina BotControlaEstoque.AtualizarEstoque. {0} itemID: {1} {0}",Environment.NewLine, estoque.itemID), ex);
            }
        }

        public int RetornaQTDaProduzir(int idItem, string NomePersonagem)
        {
            BotWoWEntities n = new BotWoWEntities();

            try
            {
                var x = (from p in n.Estoques where p.idItem == idItem && p.NomePersonagem == NomePersonagem && p.dtAtualizado > p.dtFabricado select p).First();
                var y = (from p in n.ConfiguracaoEstoques where p.idItem == idItem && p.NomePersonagem == NomePersonagem select p).First();

                x.dtFabricado = DateTime.Now;
                n.SaveChanges();


                return (int)y.Qtd - (int)x.Qtd;
            }
            catch (Exception)
            {

                return 0;
            }





        }

        public void ZeraEstoque(string nome)
        {
            BotWoWEntities n = new BotWoWEntities();
            
            var x = (from p in n.Estoques where p.NomePersonagem == nome select p);
            foreach (Estoque item in x)
	        {
                item.Qtd = 0;
	        }
            n.SaveChanges();

        }

        public List<BotItemEstoque> RetornaGrupoGlphy(string nome)
        {
            List<BotItemEstoque> l = new List<BotItemEstoque>();
            const int VALOR_EM_ESTOQUE = 12;
            const decimal VALOR_MINIMO_AH = 119999;
            BotWoWEntities n = new BotWoWEntities();
            BotWoWEntities n1 = new BotWoWEntities();

            var a = (from i in n.Items
                     join
                         isp in n.SpellItems on i.id equals isp.idItem
                     join
                         est in n.Estoques on isp.idItem equals est.idItem
                     where i.itemClass == 16 && est.Qtd < VALOR_EM_ESTOQUE && est.NomePersonagem == nome
                           && est.dtFabricado < est.dtAtualizado
                           && i.ValorMinnaAH > VALOR_MINIMO_AH
                     select new { idItem = i.id, idSpell = isp.idSpell, qtd = (VALOR_EM_ESTOQUE - est.Qtd) });

            foreach (var item in a)
            {
                BotItemEstoque b = new BotItemEstoque();
                b.itemID =(int)item.idItem;
                b.Personagem = nome;
                b.Qtd = (int)item.qtd;
                b.SpellQueCriaOItem = (int)item.idSpell;
                l.Add(b);


                var x = (from p in n1.Estoques where p.idItem == item.idItem && p.NomePersonagem == nome select p).First();
                x.dtFabricado = DateTime.Now;
                n1.SaveChanges();
            
            }

            return l;
        }

        public List<BotItemEstoque> RetornaGrupoGemas(string nome)
        {
            List<BotItemEstoque> l = new List<BotItemEstoque>();
            const int VALOR_EM_ESTOQUE = 18;
            const decimal VALOR_MINIMO_AH = 249999;
            BotWoWEntities n = new BotWoWEntities();
            BotWoWEntities n1 = new BotWoWEntities();

            var a = (from i in n.Items
                     join
                         isp in n.SpellItems on i.id equals isp.idItem
                     join
                         est in n.Estoques on isp.idItem equals est.idItem
                     where i.itemClass == 3 && est.Qtd < VALOR_EM_ESTOQUE && est.NomePersonagem == nome
                           && est.dtFabricado < est.dtAtualizado
                           && i.ValorMinnaAH > VALOR_MINIMO_AH
                     select new { idItem = i.id, idSpell = isp.idSpell, qtd = (VALOR_EM_ESTOQUE - est.Qtd) });
          

            foreach (var item in a)
            {
                BotItemEstoque b = new BotItemEstoque();
                b.itemID = (int)item.idItem;
                b.Personagem = nome;
                b.Qtd = (int)item.qtd;
                b.SpellQueCriaOItem = (int)item.idSpell;
                l.Add(b);
                

                var x = (from p in n1.Estoques where p.idItem == item.idItem && p.NomePersonagem == nome select p).First();
                x.dtFabricado = DateTime.Now;
                n1.SaveChanges();

            }

            return l;
        }
        public List<BotItemEstoque> RetornaGrupoGemas()
        {
            List<BotItemEstoque> l = new List<BotItemEstoque>();
            const int VALOR_EM_ESTOQUE = 18;
            const decimal VALOR_MINIMO_AH = 249999;
            BotWoWEntities n = new BotWoWEntities();
            BotWoWEntities n1 = new BotWoWEntities();

            var a = (from i in n.Items
                     join
                         isp in n.SpellItems on i.id equals isp.idItem
                     join
                         est in n.Estoques on isp.idItem equals est.idItem
                     where i.itemClass == 3 && est.Qtd < VALOR_EM_ESTOQUE
                           && i.ValorMinnaAH > VALOR_MINIMO_AH
                           && est.dtFabricado < est.dtAtualizado
                     select new { idItem = i.id, idSpell = isp.idSpell, qtd = (VALOR_EM_ESTOQUE - est.Qtd), nome = est.NomePersonagem });

            foreach (var item in a)
            {

                BotItemEstoque b = new BotItemEstoque();
                b.itemID = (int)item.idItem;
                b.Personagem = item.nome;
                b.Qtd = (int)item.qtd;
                b.SpellQueCriaOItem = (int)item.idSpell;
                l.Add(b);


                var x = (from p in n1.Estoques where p.idItem == item.idItem && p.NomePersonagem == item.nome select p).First();
                x.dtFabricado = DateTime.Now;
                n1.SaveChanges();

            }

            return l;
        }

        public List<BotItemEstoque> RetornaItensEnchantInscr(string Real, string Faccao)
        {

            List<BotItemEstoque> l = new List<BotItemEstoque>();
            List<decimal> IDs = new List<decimal>();

            const int VALOR_EM_ESTOQUE = 18;

            IDs.Add(87559);
            IDs.Add(87560);
            IDs.Add(83007);
            IDs.Add(83006);

            BotWoWEntities n = new BotWoWEntities();
            BotWoWEntities n1 = new BotWoWEntities();

            var a = (from i in n.Items
                     join
                         isp in n.SpellItems on i.id equals isp.idItem
                     join
                         est in n.Estoques on isp.idItem equals est.idItem
                     where IDs.Any(x => x == i.id)
                           && est.Qtd < VALOR_EM_ESTOQUE
                           && est.dtFabricado < est.dtAtualizado
                     select new { idItem = i.id, idSpell = isp.idSpell, qtd = (VALOR_EM_ESTOQUE - est.Qtd), nome = est.NomePersonagem });

            foreach (var item in a)
            {
                BotItemEstoque b = new BotItemEstoque();
                b.itemID = (int)item.idItem;
                b.Personagem = item.nome;
                b.Qtd = (int)item.qtd;
                b.SpellQueCriaOItem = (int)item.idSpell;
                l.Add(b);

                 
                var x = (from p in n1.Estoques where p.idItem == item.idItem && p.NomePersonagem == item.nome select p).First();
                x.dtFabricado = DateTime.Now;
                n1.SaveChanges();

            }

            return l;

        }

        public List<BotItemEstoque> RetornaItensEnchant(string Real, string Faccao)
        {

            List<BotItemEstoque> l = new List<BotItemEstoque>();
       
            const int VALOR_EM_ESTOQUE = 18;

            BotWoWEntities n = new BotWoWEntities();
            BotWoWEntities n1 = new BotWoWEntities();

            var a = (from i in n.Items
                     join
                         isp in n.SpellItems on i.id equals isp.idItem
                     join
                         est in n.Estoques on isp.idItem equals est.idItem
                     where est.Qtd < VALOR_EM_ESTOQUE
                           && est.dtFabricado < est.dtAtualizado
                           && i.itemClass == 0
                     select new { idItem = i.id, idSpell = isp.idSpell, qtd = (VALOR_EM_ESTOQUE - est.Qtd), nome = est.NomePersonagem });

            foreach (var item in a)
            {
                BotItemEstoque b = new BotItemEstoque();
                b.itemID = (int)item.idItem;
                b.Personagem = item.nome;
                b.Qtd = (int)item.qtd;
                b.SpellQueCriaOItem = (int)item.idSpell;
                l.Add(b);


                var x = (from p in n1.Estoques where p.idItem == item.idItem && p.NomePersonagem == item.nome select p).First();
                x.dtFabricado = DateTime.Now;
                n1.SaveChanges();

            }

            return l;

        }


        public void PegarSpelleItem(decimal spell, decimal item)
        {
            BotWoWEntities n = new BotWoWEntities();

            try
            {
                var x = (from p in n.SpellItems where p.idItem == item && p.idSpell == spell select p).First();
            }
            catch (Exception)
            {

                SpellItem s = new SpellItem();

                s.idSpell = spell;
                s.idItem = item;

                n.SpellItems.AddObject(s);
                n.SaveChanges();


            }

        }
    
        
    
    }
}