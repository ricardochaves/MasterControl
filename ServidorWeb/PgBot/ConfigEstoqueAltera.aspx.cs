using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public partial class ConfigEstoqueAltera : System.Web.UI.Page
    {

        ConfiguracaoEstoque c;
        decimal codigo;
        BotWoWEntities n = new BotWoWEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            codigo = Convert.ToDecimal(Request.QueryString["code"]);


            c = (from p in n.ConfiguracaoEstoques where p.id == codigo select p).First();

            Label1.Text = c.NomePersonagem;
            Label2.Text = c.idItem.ToString();
            TextBox1.Text = c.Qtd.ToString();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                BotWoWEntities n1 = new BotWoWEntities();
                c = (from p in n1.ConfiguracaoEstoques where p.id == codigo select p).First();
                c.Qtd = Convert.ToDecimal(TextBox1.Text);
                n1.SaveChanges();

                Response.Redirect("ConfigEstoque.aspx");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }


        }
    }
}