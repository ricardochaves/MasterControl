using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaUsuario
    {

        /// <summary>
        /// Recebe um ID e Retona um ML_Order cadastrado no EF
        /// </summary>
        /// <param name="u">ID do usuário usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_Usuario casdastrado no EF</returns>
        public ML_Usuario RetonarUsuario(decimal ID, NSAADMEntities n)
        {
            try
            {
                ML_Usuario m;
                m = (from p in n.ML_Usuario where p.id == ID select p).FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarUsuario.", ex);
            }
        }

    }
}
