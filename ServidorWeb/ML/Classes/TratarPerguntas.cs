using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MercadoLibre.SDK;
using RestSharp;
using ServidorWeb.BD;
using ServidorWeb.EntityContext;

namespace ServidorWeb.ML.Classes
{
    public class TratarPerguntas
    {

        private NSAADMEntities n;
        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();
        private Meli m;
        private CallBackML c;
        private string resource;

        public TratarPerguntas(Meli meli, NSAADMEntities nEF, string resou)
        {

            ValidaMeli(meli);

            m = meli;
            n = nEF;
            resource = resou;


        }

        public TratarPerguntas(Meli meli, NSAADMEntities nEF, CallBackML call)
        {

            ValidaMeli(meli);


            m = meli;
            n = nEF;
            c = call;
            resource = c.resource;

        }

        public TratarPerguntas(Meli meli, NSAADMEntities nEF, decimal idPerguntaML)
        {
            ValidaMeli(meli);

            m = meli;
            n = nEF;
            resource = "/questions/" + idPerguntaML;
        }

        private void AtualizaPerguntaBD()
        {


            LerML ler = new LerML(m);
            Question q = ler.RetonarQuestion(resource);

            try
            {
                var i = (from p in n.ML_Question where p.id_question == q.id select p).First();

                i.status = q.status;

                if (q.from != null)
                {
                    i.answered_questions = q.from.answered_questions;
                }
                if (q.answer != null)
                {
                    i.Answer_date_created = q.answer.date_created;
                    i.Answer_status = q.answer.status;
                    i.Answer_text = q.answer.text;
                }

                VerificaCallBack();

            }
            catch (Exception)
            {

                ML_Question mlQ = conv.ConverteQuestion(q);

                VerificaCallBack();

                n.ML_Question.AddObject(mlQ);

            }

            n.SaveChanges();
            

        }

        private void ValidaMeli(Meli meli)
        {
            if (meli.AccessToken != null)
            {
                throw new Exception("AtualizaPerguntaBD. AccessToken nulo.");
            }
        }

        private void VerificaCallBack()
        {
            if (c != null)
            {
                n.CallBackMLs.DeleteObject(c);
            }
        }

        public void executa()
        {
            AtualizaPerguntaBD();
        }

    }
}