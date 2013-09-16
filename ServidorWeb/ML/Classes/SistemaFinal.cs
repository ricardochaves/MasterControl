using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class SistemaFinal
    {

        public void FazerVenda(ML_Order o)
        {
            WSNerdMoney.NerdMoney w = new WSNerdMoney.NerdMoney();
            WSNerdMoney.Cliente c = new WSNerdMoney.Cliente();
            WSNerdMoney.vendas v = new WSNerdMoney.vendas();
            WSNerdMoney.Produto p = new WSNerdMoney.Produto();

            ML_OrderItem oi = o.ML_OrderItem.FirstOrDefault();
            ML_Item i = oi.ML_Item;

            //Criando cliente
            c._nome = o.ML_Usuario1.first_name;
            c._cod_cliente_ML = o.ML_Usuario1.id.ToString();

            //Criando produto
            p.id_ML = i.id;
            p.nome = i.title;
            

            //Criano venda
            v.cli = c;
            v.data = (DateTime)o.date_created;
            v.qtd = (int)oi.quantity;
            v.prod = p;
            

            w.realizaVenda(v);


        }

    }
}