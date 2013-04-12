using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;

namespace ServidorWeb.ML.Paginas
{
    public partial class Perguntas : System.Web.UI.Page
    {

        ControlaMeli cm;
        ControlaPerguntas cp = new ControlaPerguntas();

        protected void Page_Load(object sender, EventArgs e)
        {

            cm = new ControlaMeli();

            var x = cp.RetonaPerguntas(TipoRetonaPerguntas.NAORESPONDIDA, cm.n);

            var a = (from p in x select new { p.id, p.text });

            GridView1.DataSource = a;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            var x = cp.RetonaPerguntas(TipoRetonaPerguntas.NAORESPONDIDA, cm.n);

            foreach (ML_Question item in x)
            {

                cp.GravaPergunta(item, cm.n);

            }

        }
    }
}
