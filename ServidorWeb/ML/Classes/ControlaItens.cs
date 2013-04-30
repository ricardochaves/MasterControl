using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaItens
    {
        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

        public void GravaItem(ML_Item item, NSAADMEntities n)
        {

            ML_Item it = new ML_Item();


            try
            {
                it = (from p in n.ML_Item where p.id == item.id select p).First();

            }
            catch (InvalidOperationException)
            {

                it.id_org = item.id_org;
                it.id = item.id;
                it.title = item.title;
                it.variation_id = item.variation_id;

                n.ML_Item.AddObject(it);

                n.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina GravaItem", ex);
            }

        }

    }
}