using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.EntityContext;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Paginas
{
    public partial class VendasProduto : System.Web.UI.Page
    {

        NSAADMEntities n = EntityContextML.GetContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var x = (from p in n.ML_ItemOrganizacao select p);

                DropDownList1.Items.Clear();
                Label1.Text = "";
                Label2.Text = "";
                Label3.Text = "";

                DropDownList1.DataSource = x;
                DropDownList1.DataTextField = "descricao";
                DropDownList1.DataValueField = "id";

                DropDownList1.DataBind();



            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            var idSelecionado = Convert.ToDecimal(DropDownList1.SelectedValue);

            //var x = (from p in n.ML_Order join a in n.ML_OrderItem on p.id equals a.id_order   where a.id_item == DropDownList1.SelectedValue && p.status == "paid" select p);

            //var b = (from p in n.ML_Order from a in n.ML_OrderItem from y in n.MP_Payments where p.id == a.id_order && p.id == y.external_reference && a.id_item == DropDownList1.SelectedValue && y.status != "approved" select p);
            var b = (from p in n.ML_Order from a in n.ML_OrderItem from y in n.ML_FeedbackSeller from i in n.ML_Item where p.id == a.id_order && p.id == y.id_order && a.id_item == i.id  && i.id_org == idSelecionado && y.rating == "positive" select p);

            GridView1.DataSource = b;
            GridView1.DataBind();
            
            Label1.Text = String.Format("Total de itens com qualificação Positiva: {0}", GridView1.Rows.Count);



            var x1 = (from p in n.ML_Order from a in n.ML_OrderItem from y in n.ML_FeedbackSeller from i in n.ML_Item where p.id == a.id_order && p.id == y.id_order && a.id_item == i.id && i.id_org == idSelecionado && y.rating != "positive" select p);

            GridView2.DataSource = x1;
            GridView2.DataBind();

            Label2.Text = String.Format("Total de itens com qualificação diferente de Positiva: {0}", GridView2.Rows.Count);



            var teste = (from p in n.ML_Order from a in n.ML_OrderItem from y in n.ML_FeedbackSeller from i in n.ML_Item from mp in n.MP_Payments where p.id == a.id_order && p.id == y.id_order && a.id_item == i.id && i.id_org == idSelecionado && mp.external_reference == p.id && y.rating == "positive" && mp.status == "approved" select new { p.id, mp.transaction_amount, mp.mercadopago_fee, mp.net_received_amount, mp.total_paid_amount, mp.shipping_cost, mp.status, PagamentoID = mp.id });

            GridView3.DataSource = teste;
            GridView3.DataBind();
            Label3.Text = String.Format("Total de linhas: {0}. Valor total de frete recebido: {1}. Valor total liquido recebido: {2}", GridView3.Rows.Count, teste.Sum(a => a.shipping_cost).ToString(), (teste.Sum(a => a.net_received_amount) - teste.Sum(a => a.shipping_cost)).ToString() );
            

            

        }

    }
}