using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financeiro
{
    public class vendas
    {        
        public void realizaVendaML(Produto p)
        {                                                                               
            movimentacaoCaixa caixa = new movimentacaoCaixa();            
                      
            caixa.deposito(movimentacaoCaixa.tipoCaixa.NerdSa, p.consultaPrecoProdutoTarifado(p.id,1),movimentacaoCaixa.categoria.VendaProduto);                     
        }

        public void realizaVendaMaos(Produto p, Produto.tipoLoja l, movimentacaoCaixa.tipoCaixa c)
        {
            movimentacaoCaixa caixa = new movimentacaoCaixa();

            caixa.deposito(movimentacaoCaixa.tipoCaixa.NerdSa, p.consultaPrecoProdutoTarifado(p.id, 1), movimentacaoCaixa.categoria.VendaProduto);
        }

        public void realizaVendaMiguel(Produto p, movimentacaoCaixa.tipoCaixa c)
        {
            movimentacaoCaixa caixa = new movimentacaoCaixa();

            realizaVendaMaos(p, Produto.tipoLoja.Miguel, c);
        }

        public void realizaVendaML(Produto p, movimentacaoCaixa.tipoCaixa c)
        {
            movimentacaoCaixa caixa = new movimentacaoCaixa();

            realizaVendaMaos(p, Produto.tipoLoja.Mercadolivre, c);
        }      
    }

    public class entrega
    {
        public enum tipoEnvio
        {
            SEDEX,
            PAC,
            emMãos
        }
       

        public void realizaEnvio(string data, tipoEnvio te, string cep)
        {
            //_data = data;
            //movimentacaoCaixa c = new movimentacaoCaixa(movimentacaoCaixa.tipoCaixa.Tiago);
            //c.saque(frete(te,cep), movimentacaoCaixa.categoria.Correios);
        }

        public double frete(tipoEnvio te, string cep)
        {
            double v;
            v = 0; //calculo do valorfrete

            return v;
        }
    }
    
    public class comprador
    {
        public struct end
        {
            public string logradouro;
            public string cep;
            public string cidade;
        }

        public end endereco; 
    }
}