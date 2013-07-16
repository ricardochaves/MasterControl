using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaEndereco
    {
        /// <summary>
        /// Recebe um ID e Retona um ML_State cadastrado no EF
        /// </summary>
        /// <param name="u">ID do Estado usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_State casdastrado no EF</returns>
        public ML_State RetonarStado(decimal ID, NSAADMEntities n)
        {
            try
            {
                ML_State m;
                m = (from p in n.ML_State where p.id == ID select p).FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarStado.", ex);
            }
        }

        /// <summary>
        /// Recebe um ID e Retona um ML_City cadastrado no EF
        /// </summary>
        /// <param name="u">ID da Cidade usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_City casdastrado no EF</returns>
        public ML_City RetonarCidade(decimal ID, NSAADMEntities n)
        {
            try
            {
                ML_City m;
                m = (from p in n.ML_City where p.id == ID select p).FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarCidade.", ex);
            }
        }

        /// <summary>
        /// Recebe um ID e Retona um ML_Country cadastrado no EF
        /// </summary>
        /// <param name="u">ID da País usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_Country casdastrado no EF</returns>
        public ML_Country RetonarPais(decimal ID, NSAADMEntities n)
        {
            try
            {
                ML_Country m;
                m = (from p in n.ML_Country where p.id == ID select p).FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarPais.", ex);
            }
        }
    }
}