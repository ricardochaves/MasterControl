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
    public partial class ResumodePerda : System.Web.UI.Page
    {

        ControlaMeli cm;

        protected void Page_Load(object sender, EventArgs e)
        {
            cm = new ControlaMeli();


            var a = (from p in cm.n.ML_Order where !p.ML_FeedbackSeller.Any(x => x.rating != "positive") select p);

            GridView1.DataSource = a;
            GridView1.DataBind();



            Label1.Text = (a.Sum(x => x.total_amount) * 0.01).ToString();



        }
    }
}