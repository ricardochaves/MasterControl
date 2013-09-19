using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.Sistema
{
    public class GerenciarVendas
    {

        public void IncluirVenda(Venda v, NSAADM_HMLEntities e)
        {
            e.Vendas.AddObject(v);
            e.SaveChanges();
        }
    }
}