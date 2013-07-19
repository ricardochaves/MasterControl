using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaOrdens
    {
        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

        /// <summary>
        /// Recebe uma Order e Retona um ML_Order cadastrado no EF
        /// </summary>
        /// <param name="o">Objeto Order usado na pesquisa</param>
        /// <param name="n">Objeto NSAADMEntities onde vai ser pesquisado</param>
        /// <returns>Retorna o objeto ML_Order casdastrado no EF</returns>
        public ML_Order RetonarOrder(Order o, NSAADMEntities n)
        {
            try
            {

                ML_Order mo = (from p in n.ML_Order where p.id == o.id select p).First();

                return mo;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetonarOrder.", ex);
            }
        }

        /// <summary>
        /// Retorna todas as ordens baseadas no filtro informado.
        /// </summary>
        /// <param name="to">TipoRetornaOrdens, informa quais as ordens devem ser retornadas</param>
        /// <param name="n">Objeto NSAADMEntities usado para selecionar as ordens</param>
        /// <returns>Retorna um List<ML_Order> com todas as ordens selecionadas</returns>
        public List<ML_Order> RetornaOrdens(TipoRetonaOrdens to, NSAADMEntities n)
        {
            try
            {
                List<ML_Order> mo = new List<ML_Order>();

                if (to == TipoRetonaOrdens.ABERTASPAGAS)
                {
                    mo = (from p in n.ML_Order where p.status == "paid" select p).ToList();
                }
                else
                {
                    throw new Exception("Tipo de Retorna Order não implementado ou inválido.");
                }
                return mo;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina RetornaOrdens.", ex);
            }
        }

        /// <summary>
        /// Inclui ou Altera uma ML_Order no banco de dados.
        /// </summary>
        /// <param name="o">Objeto ML_Order que vai ser inserido ou alterado</param>
        /// <param name="n">Objeto NSAADMEntities que vai ser usado para fazer a inclusão ou alteração.</param>
        public void GravaOrdem(ML_Order o, NSAADMEntities n)
        {

        }

        /// <summary>
        /// NÃO IMPLEMENTADO - COLOCAR ELE COMO PUBLICO DEPOIS.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        public void GravaOrdem(Order o, NSAADMEntities n)
        {
            try
            {
                ML_Order Ordem;
                ConverterObjetoMLparaEF cf = new ConverterObjetoMLparaEF();

                Ordem = (from p in n.ML_Order where p.id == o.id select p).FirstOrDefault();
                if (Ordem == null)
                {
                    Ordem = cf.ConverteOrdem(o, n);
                    n.ML_Order.AddObject(Ordem);
                }
                else
                {
                    cf.AtualizaOrdem(Ordem, o, n);
                }

                n.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina GravaOrdem.", ex);
            }
        }
    }

    public enum TipoRetonaOrdens
    {
        ABERTAS = 0,
        FECHADAS = 1,
        ABERTASPAGAS = 2,
        ABERTASNAOPAGAS = 3,
    }


}
