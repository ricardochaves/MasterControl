using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaItem
    {

        /// <summary>
        /// Recebe um ID e Retona um ML_Item cadastrado no EF
        /// </summary>
        /// <param name="u">ID do item usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_Item casdastrado no EF</returns>
        public ML_Item RetonarItem(string ID, NSAADMEntities n)
        {
            try
            {
                ML_Item m;
                m = (from p in n.ML_Item where p.id == ID select p).FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarItem.", ex);
            }
        }


    }
}
