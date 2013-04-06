using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financeiro
{
    public class movimentacaoCaixa
    {
        public tipoCaixa _caixa;
        public double _valor;
        public categoria _categoria;
        public Dictionary<categoria, double> _split;

        public movimentacaoCaixa()
        {

        }

        public enum tipoCaixa
        {
            Tiago = (int)3,
            Ricardo = (int)2,
            NerdSa = (int)1
        }

        public enum categoria
        {
            Correios = (int)1,
            Papelaria = (int)2,
            Transporte = (int)3,
            Prejuizo = (int)4,
            VendaProduto = (int)5
        }

        public enum tipoAcao
        {
            deposito,
            saque
        }

        public void deposito(tipoCaixa c, double v, categoria cc)
        {
            Caixa C = new Caixa();

            using (NerdEntities N = new NerdEntities())
            {
                C.Cod_Caixa = (int)c;
                C.Cod_Categ = (int)cc;
                C.valor = (decimal)v;

                N.Caixa.AddObject(C);
                N.SaveChanges();
            }
        }

        public void saque(tipoCaixa c, double v, categoria cc)
        {

            Caixa C = new Caixa();

            using (NerdEntities N = new NerdEntities())
            {
                C.Cod_Caixa = (int)c;
                C.Cod_Categ = (int)cc;
                C.valor = -(decimal)v;

                N.Caixa.AddObject(C);
                N.SaveChanges();
            }
        }

        public void deposito(tipoCaixa c, Dictionary<categoria, double> cc)
        {
            Caixa C = new Caixa();

            using (NerdEntities N = new NerdEntities())
                foreach (KeyValuePair<categoria, double> pair in cc)
                {
                    C.Cod_Caixa = (int)c;
                    C.Cod_Categ = (decimal)pair.Key;
                    C.valor = (decimal)pair.Value;

                    N.Caixa.AddObject(C);
                    N.SaveChanges();
                }
        }

        public void saque(tipoCaixa c, Dictionary<categoria, double> cc)
        {

            Caixa C = new Caixa();

            using (NerdEntities N = new NerdEntities())
                foreach (KeyValuePair<categoria, double> pair in cc)
                {
                    C.Cod_Caixa = (int)c;
                    _categoria = pair.Key;
                    _valor = -(double)pair.Value;

                    N.Caixa.AddObject(C);
                    N.SaveChanges();
                }
        }

    }
}