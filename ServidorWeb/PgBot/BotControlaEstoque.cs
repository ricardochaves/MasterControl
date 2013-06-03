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

                n.SaveChanges();

            }
            catch (InvalidOperationException)
            {
                est.idItem = estoque.itemID;
                est.NomePersonagem = estoque.Personagem;
                est.Qtd = estoque.Qtd;

                n.Estoques.AddObject(est);
                n.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na rotina BotControlaEstoque.AtualizarEstoque. {0} itemID: {1} {0}",Environment.NewLine, estoque.itemID), ex);
            }
        }
    }
}