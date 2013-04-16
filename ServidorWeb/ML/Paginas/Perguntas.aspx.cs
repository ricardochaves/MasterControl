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
        ControlaCallBack cb = new ControlaCallBack();

        protected void Page_Load(object sender, EventArgs e)
        {

            cm = new ControlaMeli();

            TrataPerguntasDoCallBack();
            cb.LimpaCallBack();

            var x = cp.RetonaPerguntas(TipoRetonaPerguntas.NAORESPONDIDA, cm.n);

            var a = (from p in x select new { p.id_question, p.text });

            GridView1.DataSource = a;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                AtualizaPerguntas();

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao atualizar perguntas.",ex);
            }

        }

        private void AtualizaPerguntas()
        {
            try
            {
                var x = cp.RetonaPerguntas(TipoRetonaPerguntas.NAORESPONDIDA, cm.n);

                foreach (ML_Question item in x)
                {

                    cp.GravaPergunta(cm.RetonarQuestion("/questions/" + item.id_question), cm.n);

                }

            }
            catch (InvalidOperationException iex)
            {
                throw new Exception("Nada para atualizar.", iex);

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar perguntas.", ex);
            }
        }

        private void TrataPerguntasDoCallBack()
        {

            try
            {
                
                List<CallBackML> a = cb.RetornaCallBacks(ControlaCallBack.TipoRetonaCallBacks.PERGUNTAS, cm.n);

                foreach (CallBackML item in a)
                {

                    cp.GravaPergunta(cm.RetonarQuestion(item.resource),cm.n);

                }

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina TrataPerguntasDoCallBack.",ex);
            }

        }
        
    }
}
