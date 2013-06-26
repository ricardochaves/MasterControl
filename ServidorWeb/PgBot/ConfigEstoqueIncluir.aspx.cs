using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public partial class ConfigEstoqueIncluir : System.Web.UI.Page
    {
        BotWoWEntities n = new BotWoWEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConfiguracaoEstoque c = new ConfiguracaoEstoque();
                c.idItem = Convert.ToDecimal(txtIdItem.Text);
                c.Qtd = Convert.ToDecimal(txtQtd.Text);
                c.NomePersonagem = txtNome.Text;
                n.ConfiguracaoEstoques.AddObject(c);
                n.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina Button1_Click.", ex);
            }

        }
    }
}