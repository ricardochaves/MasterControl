using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;

namespace Financeiro
{
    public class Produto
    {
        public string nome;
        public double preco;
        public tipoLoja[] loja;

        public enum tipoLoja
        {
            Mercadolivre = (int)1,
            BomNegocio = (int)2,
            OLX = (int)3,
            TodOferta = (int)4,
            Miguel = (int)5
        }

        public double consultaPrecoProduto(int codProduto, tipoLoja codLoja)
        {

            NSAADMEntities N;
            ConstruirEF cf = new ConstruirEF();
            N = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);


            LojaProduto lp = new LojaProduto();

            lp = N.LojaProdutoes.First(u => u.Cod_Produto == codProduto && u.Cod_Loja == (int)codLoja);
            
            return (double)lp.Preco;
           
        }

        public double consultaPrecoProdutoTarifado(int codProduto, tipoLoja codLoja)
        {

            NSAADMEntities N;
            ConstruirEF cf = new ConstruirEF();
            N = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

            
            LojaProduto lp = new LojaProduto();

            lp = N.LojaProdutoes.First(u => u.Cod_Produto * u.Tarifa == codProduto && u.Cod_Loja == (int)codLoja);

            return (double)lp.Preco;
           
        }
    }
}