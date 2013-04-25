using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Financeiro
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Produto p = new Produto();

            p.nome = this.TextBox1.Text;
            p.qtd = int.Parse(this.TextBox2.Text);
            p.incluiProduto();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Produto p = new Produto();

            this.GridView1.DataSource = p.consultaTodosProdutos();
            this.GridView1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto p = new Produto();
                       
            DropDownList2.DataSource = p.consultaLojaProdutos(int.Parse(DropDownList1.SelectedValue));            
            DropDownList2.DataTextField = "Preco";
            DropDownList2.DataValueField = "Cod_Produto";            
            DropDownList2.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            movimentacaoCaixa mc = new movimentacaoCaixa();
            Produto p = new Produto();

            mc.deposito(movimentacaoCaixa.tipoCaixa.NerdSa, p.consultaPrecoProdutoTarifado(int.Parse(DropDownList2.SelectedValue), int.Parse(DropDownList1.SelectedValue)), movimentacaoCaixa.categoria.VendaProduto);
            p.reduzEstoque();
        }
        

        



    }
}
