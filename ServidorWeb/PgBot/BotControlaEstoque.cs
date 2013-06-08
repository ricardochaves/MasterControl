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

    }
}