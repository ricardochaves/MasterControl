﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.Sistema.conversores
{
    public class ConverteVendas
    {

        NSAADMEntities e = new NSAADMEntities();

        public Venda ConverteMLVendaEmVenda(ML_Order o)
        {

            String id;
            id = o.id.ToString();

            Venda v = new Venda();

            v.data_venda = Convert.ToDateTime(o.date_created);
            v.valor_venda = (decimal)o.total_amount;
            v.valor_desconto = 0;
            v.valor_frete = 0;


            Cliente c = (from p in e.Clientes where p.idML == id select p).FirstOrDefault();
            if (c == null)
            {
                c = new Cliente();
                c.email = o.ML_Usuario.email;
                c.nome = o.ML_Usuario.first_name;
                c.idML = o.id.ToString();
                
            }
            v.Cliente = c;

            ML_OrderItem mo = o.ML_OrderItem.FirstOrDefault();
            Produto pr = (from a in e.Produtoes where a.Descr == mo.ML_Item.title select a).FirstOrDefault();
            if (pr == null)
            {
                pr = new Produto();
                pr.Descr = mo.ML_Item.title;
                pr.qtd = (decimal)mo.quantity;
            }

            VendaProduto vp = new VendaProduto();
            vp.Produto = pr;
            vp.Venda = v;

            v.VendaProdutoes.Add(vp);

            return v;
        }
    }
}