using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;

namespace ServidorWeb.ML.Paginas
{
    public partial class ResumoPagamentoItem : System.Web.UI.Page
    {

        NSAADMEntities n;
        double COMISSAO_ML = Convert.ToDouble("0,925");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ConstruirEF cf = new ConstruirEF();
                n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

                var um = (from ito in n.ML_ItemOrganizacao
                          join it in n.ML_Item on ito.id equals it.id_org
                          join ordit in n.ML_OrderItem on it.id equals ordit.id_item
                          join ord in n.ML_Order on ordit.id_order equals ord.id
                          join fd in n.ML_FeedbackSeller on ord.id equals fd.id_order
                         where fd.id_order == ord.id
                            && fd.rating == "positive"
                        select new { DescrItem = ito.Descricao, ValorOrdem = ord.total_amount });


                var dois = (from p in um group p by p.DescrItem into umg select new { ItemDesc = umg.Key, Qtd = umg.Count(), TotalBruto = umg.Sum(x => x.ValorOrdem), TotalLiquido = (umg.Sum(x1 => x1.ValorOrdem) * COMISSAO_ML) });

                GridView1.DataSource = dois;
                GridView1.DataBind();

                Label1.Text = String.Format("Total de valor recebido: {0}",dois.Sum(x => x.TotalLiquido));

            }
        }
    }
}