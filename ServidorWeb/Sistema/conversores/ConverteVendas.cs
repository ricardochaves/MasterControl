using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.Sistema.conversores
{
    public class ConverteVendas
    {

        public Venda ConverteMLVendaEmVenda(ML_Order o, NSAADM_HMLEntities e)
        {

            String id;
            id = o.id.ToString();

            ML_Shipping mS = o.ML_Shipping.FirstOrDefault();
            ML_FeedbackSeller Fb = o.ML_FeedbackSeller.FirstOrDefault();

            Venda v = new Venda();

            v.data_venda = Convert.ToDateTime(o.date_created);
            v.valor_venda = (decimal)o.total_amount;
            v.valor_desconto = 0;
            v.data_final = Convert.ToDateTime(o.date_closed);

            if (Fb != null)
            {
                v.status = ConvertStatus(o.status, Fb.rating);
            }
            else
            {
                v.status = ConvertStatus(o.status, "");
            }
            

            if (mS != null)
            {
                v.valor_frete = Convert.ToDecimal(mS.cost);
            }

            v.id_ML = id;

            Cliente c = (from p in e.Clientes where p.idML == id select p).FirstOrDefault();
            if (c == null)
            {
                c = new Cliente();
                c.email = o.ML_Usuario1.email;
                c.nome = o.ML_Usuario1.first_name;
                c.idML = o.id.ToString();
                c.nicknName = o.ML_Usuario1.nickname;
                c.ultimoNome = o.ML_Usuario1.last_name;
            }
            v.Cliente = c;

            ML_OrderItem mo = o.ML_OrderItem.FirstOrDefault();
            Produto pr = (from a in e.Produtoes where a.Descr == mo.ML_Item.title select a).FirstOrDefault();
            if (pr == null)
            {
                pr = new Produto();
                pr.Descr = mo.ML_Item.title;
                pr.qtd = (decimal)mo.quantity;
            }

            
            VendaProduto vp = new VendaProduto();
            vp.Produto = pr;
            vp.Venda = v;
            vp.qtd = (decimal)mo.quantity;

            v.VendaProdutoes.Add(vp);

            return v;
        }

        /// <summary>
        /// Convente o Status que vem do Mercado Livre utilizando o rating da qualificação e o status da venda
        /// </summary>
        /// <param name="status">Status do Objeto Ordem</param>
        /// <param name="rating">Rating do Objeto FeedbackBuyer</param>
        /// <returns>1 - Pago; 2 - Cancelada; 3 - Aguardando Pagamento; 99 - Erro</returns>
        public decimal ConvertStatus(string status, string rating)
        {

            if (status == "paid")
            {
                return 1; 
            }
            else if (status == "confirmed" && rating == "positive")
            {
                return 1;
            }
            else if (status == "confirmed" && rating == "")
            {
                return 3;
            }
            else if (status == "confirmed" && rating != "positive")
            {
                return 2; 
            }
            else
            {
                return 99;
            }

        }
    }
}