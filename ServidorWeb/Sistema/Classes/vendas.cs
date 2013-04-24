using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financeiro
{
    public class vendas
    {        
        public void realizaVenda(Produto p)
        {                                                                               
            movimentacaoCaixa caixa = new movimentacaoCaixa();            
                      
            caixa.deposito(movimentacaoCaixa.tipoCaixa.Tiago, p.preco, movimentacaoCaixa.categoria.VendaProduto);            
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