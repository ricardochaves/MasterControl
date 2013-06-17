using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public partial class ConfigEstoque : System.Web.UI.Page
    {

        BotWoWEntities n = new BotWoWEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.Items.Clear();
                var nomes = n.ConfiguracaoEstoques.Select(m => m.NomePersonagem).Distinct();

                foreach (string item in nomes)
                {
                    DropDownList1.Items.Add(new ListItem(item));
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var nome = DropDownList1.SelectedItem.ToString();
            var x = (from p in n.ConfiguracaoEstoques
                     join i in n.Items on p.idItem equals i.id
                     where p.NomePersonagem == nome
                     select new { ItemID =i.id, Item = i.Desc, Qtd = p.Qtd, Chave = p.id });

            GridView1.DataSource = x;
            GridView1.DataBind();
        
        }



    }
}