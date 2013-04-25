using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financeiro
{
    public class Produto
    {
        public int id;
        public string nome;
        public double preco;
        public tipoLoja[] loja;
        public int qtd;

        public enum tipoLoja 
        {
            Mercadolivre = (int)1,
            BomNegocio = (int)2,
            OLX = (int)3,
            TodOferta = (int)4,
            Miguel = (int)5
        }

        public double consultaPrecoProduto(int codProduto, int codLoja)
        {            
            using (NerdEntities N = new NerdEntities())
            {
                LojaProduto lp = new LojaProduto();                
                
                lp = N.LojaProduto.First(u => u.Cod_Produto == codProduto && u.Cod_Loja == (int)codLoja);

                return (double)lp.Preco;                
            }
        }

        public double consultaTarifaLoja(int codLoja)
        {
            using (NerdEntities N = new NerdEntities())
            {
                LojaProduto lp = new LojaProduto();

                lp = N.LojaProduto.First(u => u.Cod_Loja == (int)codLoja);

                return (double)lp.Preco;
            }
        }

        public double consultaPrecoProdutoTarifado(int codProduto, int codLoja)
        {
            Double preco;

            preco = consultaPrecoProduto(codProduto, codLoja);

            return preco - (preco * consultaTarifaLoja(codLoja));
        }


        public List<produto> consultaTodosProdutos()
        {
            using (NerdEntities N = new NerdEntities())
            {
                produto p = new produto();

                return  N.produto.ToList();                
            }
            
        }

        public List<LojaProduto> consultaLojaProdutos(int pCodLoja)
        {
            using (NerdEntities N = new NerdEntities())
            {      
                return N.LojaProduto.Where (l => l.Cod_Loja == pCodLoja).ToList();
            }

        }

        public void incluiProduto()
        {
            using (NerdEntities N = new NerdEntities())
            {
                produto p = new produto();

                p.nome = this.nome;
                p.quantidade = this.qtd;

                N.produto.AddObject(p);

                N.SaveChanges();
            }
        }

        public void reduzEstoque()
        {            
            using (NerdEntities N = new NerdEntities())
            {
                produto p = new produto();  
                
                      
            }

        }
       
    } 

  }