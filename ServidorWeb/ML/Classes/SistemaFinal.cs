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
            WSNerdMoney.cliente c = new WSNerdMoney.cliente();


            c.nome = o.ML_Usuario.nickname;

            w.realizaVenda(1, WSNerdMoney.tipoCaixa.NerdSa, 1, c);


        }

    }
}