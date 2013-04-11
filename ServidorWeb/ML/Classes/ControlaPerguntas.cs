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

        const string NÃO_RESPONDIDAS = "UNANSWERED";


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
                var i = (from p in n.ML_Question where p.id_question == q.id_question select p).First();

                
                i.Answer_date_created = q.Answer_date_created;
                i.Answer_status = q.Answer_status;
                i.Answer_text = q.Answer_text;
                i.answered_questions = q.answered_questions;
                i.date_created = q.date_created;
                i.id_from = q.id_from;
                i.status = q.status;

                n.SaveChanges();


            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina GravaPergunta.", ex);

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

        public List<ML_Question> RetornaPerguntas(TipoRetornaPerguntas t, NSAADMEntities n)
        {

            try
            {

                if (t == TipoRetornaPerguntas.NaoRespondidas)
                {

                    List<ML_Question> q = (from p in n.ML_Question where p.status == NÃO_RESPONDIDAS select p).ToList();
                    return q;
                }
                else
                {
                    throw new Exception("Tipo de Retorno inválido ou implementado.");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina RetornaPerguntas.",ex);
            }
        }

        public ML_Question RetornaQuestion(decimal codigo, NSAADMEntities n)
        {
            try
            {
                return (from p in n.ML_Question where p.id == codigo select p).First();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro na rotina RetornaQuestion.",ex);
            }
        }
    }

    public enum TipoRetornaPerguntas
    {
        NaoRespondidas = 0,
        Respondidas = 1,
        Todas = 2
    }

}