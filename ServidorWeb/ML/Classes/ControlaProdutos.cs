using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaProdutos
    {

        public ML_Item RetornaProduto(string id, NSAADMEntities n)
        {
            try
            {
                return (from p in n.ML_Item where p.id == id select p).First();
            }
            catch (Exception)
            {

                ML_Item ml = new ML_Item();
                ml.title = "Sem venda";
                return ml;

            }
            
        }
        
    }
}