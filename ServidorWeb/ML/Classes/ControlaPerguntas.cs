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

        public void GravaPergunta(ML_Question q, NSAADMEntities n)
        {

            try
            {
                var i = (from p in n.ML_Question where p.id_question == q.id select p).First();

                i.Answer_date_created = q.Answer_date_created;
                i.Answer_status = q.Answer_status;
                i.Answer_text = q.Answer_text;
                i.answered_questions = q.answered_questions;

                n.SaveChanges();


            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina GravaPergunta(ML_Question q, NSAADMEntities n)", ex);
            }


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

        public ML_Question RetornaPergunta(decimal id, NSAADMEntities n)
        {

            ML_Question q;

            try
            {
                q = (from p in n.ML_Question where p.id == id select p).First();

                return q;

            }
            catch (InvalidOperationException iex)
            {
                throw new Exception("Pergunta não encontrada.", iex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na rotina RetornaPergunta. {0} id: {1} {0}", Environment.NewLine, id), ex);
            }
        }

        public List<ML_Question> RetonaPerguntas(TipoRetonaPerguntas t, NSAADMEntities n)
        {

            if (t == TipoRetonaPerguntas.NAORESPONDIDA)
            {
                List<ML_Question> q = (from p in n.ML_Question select p).ToList();

                return q;
            }
            else
            {
                throw new Exception("Tipo de Retona Pergunta não implementado ou inválido.");
            }

        }






    }

    public enum TipoRetonaPerguntas
    {
        NAORESPONDIDA = 0,
        RESPONDIDAS = 1,
        TODAS = 2
    }

}