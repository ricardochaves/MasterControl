using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ControlaPerguntas
    {

        private ConverterObjetoMLparaEF conv = new ConverterObjetoMLparaEF();

        public void GravaPergunta(Question q, NSAADMEntities n)
        {
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


            }
            catch (Exception)
            {

                ML_Question mlQ = conv.ConverteQuestion(q);

                n.ML_Question.AddObject(mlQ);

            }

            n.SaveChanges();
            

        }

        public void AtualizaPergunta(long codigo, NSAADMEntities n)
        {
            Question q = new Question();
            q.id = codigo;

        }

        private void AtualizaDados(ML_Question q, NSAADMEntities n)
        {

            try
            {
                var i = (from p in n.ML_Question where p.id_question == q.id_question select p).First();

                i.status = q.status;

                i.Answer_date_created = q.Answer_date_created;
                i.Answer_status = q.Answer_status;
                i.Answer_text = q.Answer_text;
                i.answered_questions = q.answered_questions;
                i.date_created = q.date_created;
                i.id_from = q.id_from;

                n.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina AtualizaDados", ex);
            }


        }
    }
}