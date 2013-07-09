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
        protected void Page_Load(object sender, EventArgs e)
        {

            BotWoWEntities n = new BotWoWEntities();

            var x = (from p in n.Estoques
                     join i in n.Items on p.idItem equals i.id
                     where p.NomePersonagem == "Goldaqui" && i.itemClass == 3
                     select new { Nome = i.Desc, Qtd = p.Qtd, Cor = i.itemSubClass, Valor = (i.ValorMinnaAH/10000) }
                     );
            
            GridView1.DataSource = x.OrderBy(z => z.Valor);
            GridView1.DataBind();


        }
    }
}