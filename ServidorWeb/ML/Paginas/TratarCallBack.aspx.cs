using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;
using RestSharp;
using ServidorWeb.BD;


namespace ServidorWeb.ML.Paginas
{
    public partial class TratarCallBack : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            ControlaMeli cm = new ControlaMeli();
            ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

            var x = (from p in cm.n.CallBackMLs where p.topic == "questions" select p);


            foreach (CallBackML c in x.ToList())
            {
                if (c.topic == "questions")
                {

                    Question q = cm.RetonarQuestion(c.resource);

                    ControlaPerguntas p = new ControlaPerguntas();
                    p.GravaPergunta(q, cm.n);
                    

                }
            }
        }


    }
}