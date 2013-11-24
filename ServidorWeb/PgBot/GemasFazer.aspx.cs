using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public partial class GemasFazer : System.Web.UI.Page
    {
        BotWoWEntities n = EntityContextBot.GetContext;

        protected void Page_Load(object sender, EventArgs e)
        {

            

            if (!Page.IsPostBack)
            {
                BotWoWEntities n1 = EntityContextBot.GetContext;

                DropDownList1.Items.Clear();
                var nomes = n1.Estoques.Select(m => m.NomePersonagem).Distinct();

                foreach (string item in nomes)
                {
                    DropDownList1.Items.Add(new ListItem(item));
                }

            }


            



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var nome = DropDownList1.SelectedItem.ToString();
            var x = (from p in n.Estoques
                     join i in n.Items on p.idItem equals i.id
                     where p.NomePersonagem == nome && i.itemClass == 3
                     select new { Nome = i.Desc, Qtd = p.Qtd, Cor = i.itemSubClass, Valor = (i.ValorMinnaAH / 10000) }
                     );

            GridView1.DataSource = x.OrderBy(z => z.Valor);
            GridView1.DataBind();
        }
    }
}