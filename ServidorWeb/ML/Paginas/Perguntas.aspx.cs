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

        ControlaPerguntas cp = new ControlaPerguntas();
        ControlaMeli cm;

        protected void Page_Load(object sender, EventArgs e)
        {

            cm = new ControlaMeli();


            List<ML_Question> x = cp.RetornaPerguntas(TipoRetornaPerguntas.NaoRespondidas, cm.n);

            var a = (from p in x select new { p.id, p.text });

            GridView1.DataSource = a;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<ML_Question> x = cp.RetornaPerguntas(TipoRetornaPerguntas.NaoRespondidas, cm.n);

            foreach (ML_Question item in x)
            {

                cp.GravaPergunta(item, cm.n); 

            }

        }

    }
}