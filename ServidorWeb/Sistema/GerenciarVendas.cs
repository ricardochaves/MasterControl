using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.Sistema
{
    public class GerenciarVendas
    {

        public void IncluirVenda(Venda v, NSAADMEntities e)
        {
            e.Vendas.AddObject(v);
        }
    }
}