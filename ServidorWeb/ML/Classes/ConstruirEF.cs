using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{

    public enum Entities
    {
        Bot,
        MercadoLivre

    }


    public class ConstruirEF
    {

        public Object RecuperaEntity(Entities en)
        {

            Object EF;

            if (en == Entities.MercadoLivre)
            {
                EF = new NSAADMEntities("");
                return (NSAADMEntities)EF;
            }
            else if (en == Entities.Bot)
            {
                EF = new BotWoWEntities("");
                return (BotWoWEntities)EF;
            }
            else
            {
                throw new Exception("Enumerador inválido");
            }

        }

        private string stringdeconexao()
        {

             System.Threading.Thread.CurrentThread.CurrentPrincipal.Identity.Name

        }

    }
}