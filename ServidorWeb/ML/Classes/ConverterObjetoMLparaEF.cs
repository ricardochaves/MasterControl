using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;
using ServidorWeb.EntityContext;

namespace ServidorWeb.ML.Classes
{
    public class ConverterObjetoMLparaEF
    {

        NSAADMEntities n = EntityContextML.GetContext;

        public ML_Usuario ConverteUsuario(Usuario us)
        {


            //DADOS DA BASE DA CLASSE
            ML_Usuario u = new ML_Usuario();


            try
            {
                u = (from p in n.ML_Usuario where p.id == us.id select p).First();
                return u;
            }
            catch (InvalidOperationException e)
            {

                u.country_id = us.country_id;
                u.email = us.email;
                u.first_name = us.first_name;
                u.id = us.id;
                u.last_name = us.last_name;
                u.logo = us.logo;
                u.nickname = us.nickname;
                u.permalink = us.permalink;
                u.points = us.points;
                //u.registration_date = us.registration_date;
                u.seller_experience = us.seller_experience;
                u.site_id = us.site_id;
                u.user_type = us.user_type;


                //IDENTIFICAÇÃO USUARIO
                if (us.identification != null)
                {
                    ML_Identification ident = new ML_Identification();
                    ident.number = us.identification.number;
                    ident.type = us.identification.type;
                    u.ML_Identification.Add(ident);
                    n.AddObject("ML_Identification", ident);
                }

                if (us.billing_info != null)
                {
                    ML_Identification ident = new ML_Identification();
                    ident.type = us.billing_info.doc_type;
                    ident.number = us.billing_info.doc_number;
                    u.ML_Identification.Add(ident);
                    n.AddObject("ML_Identification", ident);
                }


                if (us.phone != null)
                {
                    //TELEFONES
                    ML_Phone pho = new ML_Phone();
                    pho.area_code = us.phone.area_code;
                    pho.extension = us.phone.extension;
                    pho.number = us.phone.number;
                    pho.varified = us.phone.verified.ToString();
                    u.ML_Phone.Add(pho);
                    n.AddObject("ML_Phone", pho);

                    if (us.alternative_phone != null)
                    {
                        pho = new ML_Phone();
                        pho.area_code = us.alternative_phone.area_code;
                        pho.extension = us.alternative_phone.extension;
                        pho.number = us.alternative_phone.number;
                        u.ML_Phone.Add(pho);
                        n.AddObject("ML_Phone", pho);
                    }
                }

                //TRATANDO REPUTAÇÃO DE VENDEDOR
                if (us.seller_reputation != null)
                {
                    ML_SellerReputation sr = new ML_SellerReputation();
                    sr.power_seller_status = us.seller_reputation.power_seller_status;
                    sr.level_id = us.seller_reputation.level_id;
                    u.ML_SellerReputation.Add(sr);
                    n.AddObject("ML_SellerReputation", sr);

                    ML_TransactionsSeller ts = new ML_TransactionsSeller();
                    ts.canceled = us.seller_reputation.transactions.canceled;
                    ts.completed = us.seller_reputation.transactions.completed;
                    ts.period = us.seller_reputation.transactions.period;
                    ts.total = us.seller_reputation.transactions.total;
                    ts.ML_SellerReputation = sr;
                    sr.ML_TransactionsSeller.Add(ts);
                    n.AddObject("ML_TransactionsSeller", ts);

                    ML_Ratings rt = new ML_Ratings();
                    rt.ML_TransactionsSeller = ts;
                    rt.negative = us.seller_reputation.transactions.ratings.negative;
                    rt.neutral = us.seller_reputation.transactions.ratings.neutral;
                    rt.positive = us.seller_reputation.transactions.ratings.positive;
                    ts.ML_Ratings.Add(rt);
                    n.AddObject("ML_Ratings", rt);
                }


                //TRATANDO REPUTAÇÃO COMPRADOR
                if (us.buyer_reputation != null)
                {
                    ML_BuyerReputation mb = new ML_BuyerReputation();
                    mb.canceled_transactions = us.buyer_reputation.canceled_transactions;
                    u.ML_BuyerReputation.Add(mb);
                    n.AddObject("ML_BuyerReputation", mb);

                    ML_TransactionsBuyer tb = new ML_TransactionsBuyer();
                    tb.completed = us.buyer_reputation.transactions.completed;
                    tb.period = us.buyer_reputation.transactions.period;
                    tb.total = us.buyer_reputation.transactions.total;
                    tb.ML_BuyerReputation = mb;
                    n.AddObject("ML_TransactionsBuyer", tb);

                    ML_ResumoTransBuyer canceled = new ML_ResumoTransBuyer();
                    canceled.paid = us.buyer_reputation.transactions.canceled.paid;
                    canceled.total = us.buyer_reputation.transactions.canceled.total;
                    canceled.units = us.buyer_reputation.transactions.canceled.units;
                    tb.ML_ResumoTransBuyer2 = canceled;
                    n.AddObject("ML_ResumoTransBuyer", canceled);

                    ML_ResumoTransBuyer unrated = new ML_ResumoTransBuyer();
                    unrated.paid = us.buyer_reputation.transactions.unrated.paid;
                    unrated.total = us.buyer_reputation.transactions.unrated.total;
                    unrated.units = us.buyer_reputation.transactions.unrated.units;
                    tb.ML_ResumoTransBuyer1 = unrated;
                    n.AddObject("ML_ResumoTransBuyer", unrated);

                    ML_ResumoTransBuyer not_yet_rated = new ML_ResumoTransBuyer();
                    not_yet_rated.paid = us.buyer_reputation.transactions.not_yet_rated.paid;
                    not_yet_rated.total = us.buyer_reputation.transactions.not_yet_rated.total;
                    not_yet_rated.units = us.buyer_reputation.transactions.not_yet_rated.units;
                    tb.ML_ResumoTransBuyer = not_yet_rated;
                    n.AddObject("ML_ResumoTransBuyer", not_yet_rated);
                }

                n.AddObject("ML_Usuario", u);

                n.SaveChanges();

                return u;
            }

        }

        public void ConverteOrdem(Order o)
        {



            //CONVERTENDO COMPRADOR E VENDEDOR
            ML_Usuario buy = ConverteUsuario(o.buyer);
            ML_Usuario sel = ConverteUsuario(o.seller);


            ML_Order ord = new ML_Order();
            //ITENS DA ORDEM
            ML_OrderItem oi;
            foreach (OrderItem item in o.order_items)
            {
                oi = new ML_OrderItem();
                oi.currency_id = item.currency_id; 
                oi.quantity = item.quantity;
                oi.unit_price = item.unit_price;

                oi.ML_Item = ConverteItem(o.order_items[0].item);

                ord.ML_OrderItem.Add(oi);
            }


            ML_Payment pa;
            foreach (Payment p in o.payments)
            {
                pa = new ML_Payment();
                pa.currency_id = p.currency_id;
                pa.date_created = p.date_created;
                pa.date_last_updated = p.date_last_updated;
                pa.status = p.status;
                pa.transaction_amount = p.transaction_amount;

                pa.id = p.id;

                n.ML_Payment.AddObject(pa);

                ord.ML_Payment.Add(pa);


            }


            if (o.feedback != null)
            {
                if (o.feedback.purchase != null)
                {
                    ML_FeedbackBuyer feeb = new ML_FeedbackBuyer();
                    feeb.date_created = o.feedback.purchase.date_created;
                    feeb.fulfilled = o.feedback.purchase.fulfilled.ToString();
                    feeb.rating = o.feedback.purchase.rating;
                    feeb.id_order = o.id;

                    n.ML_FeedbackBuyer.AddObject(feeb);

                    ord.ML_FeedbackBuyer.Add(feeb);

                }

                if (o.feedback != null)
                {
                    if (o.feedback.sale != null)
                    {
                        ML_FeedbackSeller fees = new ML_FeedbackSeller();
                        fees.date_created = o.feedback.sale.date_created;
                        fees.fulfilled = o.feedback.sale.fulfilled.ToString();
                        fees.rating = o.feedback.sale.rating;
                        fees.id_order = o.id;

                        n.ML_FeedbackSeller.AddObject(fees);

                        ord.ML_FeedbackSeller.Add(fees);
                    }
                }


            }



            ord.ML_Usuario = sel;
            ord.ML_Usuario1 = buy;

            //DADOS DA ORDEM
            ord.id = o.id;
            ord.currency_id = o.currency_id;
            ord.date_closed = o.date_closed;
            ord.date_created = o.date_created;
            ord.status = o.status;
            //ord.status_detail = o.status_detail.description;
            ord.total_amount = o.total_amount;

            n.ML_Order.AddObject(ord);
            n.SaveChanges();

        }

        public void AtualizaOrdem(ML_Order o, Order oML)
        {

            o.ML_FeedbackBuyer.Load();
            o.ML_FeedbackSeller.Load();
            o.ML_Payment.Load();

            o.status = oML.status;

            if (oML.status_detail != null)
            {
                o.status_detail = oML.status_detail.description;
            }

            AtualizaFeedBackBuyer(o, oML);
            AtualizaFeedBackSeller(o, oML);

            n.SaveChanges();
            
        }

        private Purchase RetornaFeedBackPurchase(Order o)
        {
            if (o.feedback != null)
            {
                if (o.feedback.purchase != null)
                {
                    return o.feedback.purchase;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private Sale RetornaFeedBackSeller(Order o)
        {
            if (o.feedback != null)
            {
                if (o.feedback.sale != null)
                {
                    return o.feedback.sale;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private void AtualizaFeedBackBuyer(ML_Order o, Order oML)
        {
            if (o.ML_FeedbackBuyer.Count > 0)
            {
                if (RetornaFeedBackPurchase(oML) != null)
                {
                    ML_FeedbackBuyer b = o.ML_FeedbackBuyer.First();
                    Purchase p = RetornaFeedBackPurchase(oML);

                    b.date_created = p.date_created;
                    b.fulfilled = p.fulfilled.ToString();
                    b.rating = p.rating;

                }

            }
            else
            {
                if (RetornaFeedBackPurchase(oML) != null)
                {

                    ML_FeedbackBuyer b1 = new ML_FeedbackBuyer();
                    Purchase p1 = RetornaFeedBackPurchase(oML);

                    b1.date_created = p1.date_created;
                    b1.fulfilled = p1.fulfilled.ToString();
                    b1.rating = p1.rating;
                    b1.id_order = o.id;

                    n.ML_FeedbackBuyer.AddObject(b1);
                    o.ML_FeedbackBuyer.Add(b1);

                }


            }
        }
        private void AtualizaFeedBackSeller(ML_Order o, Order oML)
        {
            if (o.ML_FeedbackSeller.Count > 0)
            {
                if (RetornaFeedBackSeller(oML) != null)
                {
                    ML_FeedbackSeller MLs = o.ML_FeedbackSeller.First();
                    Sale s = RetornaFeedBackSeller(oML);

                    MLs.date_created = s.date_created;
                    MLs.fulfilled = s.fulfilled.ToString();
                    MLs.rating = s.rating;

                }

            }
            else
            {
                if (RetornaFeedBackSeller(oML) != null)
                {

                    ML_FeedbackSeller MLs1 = new ML_FeedbackSeller();
                    Sale s1 = RetornaFeedBackSeller(oML);

                    MLs1.date_created = s1.date_created;
                    MLs1.fulfilled = s1.fulfilled.ToString();
                    MLs1.rating = s1.rating;
                    MLs1.id_order = o.id;

                    n.ML_FeedbackSeller.AddObject(MLs1);
                    o.ML_FeedbackSeller.Add(MLs1);

                }


            }
        }

        public ML_Item ConverteItem(Item i)
        {
            ML_Item it = new ML_Item();


            try
            {
                it = (from p in n.ML_Item where p.id == i.id select p).First();
                return it;

            }
            catch (InvalidOperationException)
            {

                it.id = i.id;
                it.title = i.title;
                it.variation_id = i.variation_id;

                n.ML_Item.AddObject(it);
                n.SaveChanges();

                return it;

            }



        }

        public ML_Question ConverteQuestion(Question q)
        {

            ML_Question mlq = new ML_Question();

            if (q.answer != null)
            {
                mlq.Answer_date_created = q.answer.date_created;
                mlq.Answer_status = q.answer.status;
                mlq.Answer_text = q.answer.text;
            }

            if (q.from != null)
            {
                mlq.id_from = q.from.id;
                mlq.answered_questions = q.from.answered_questions;
            }
            mlq.date_created = q.date_created;
            mlq.id = q.id;
            if (q.item_id != null)
            {
                mlq.item_id = q.item_id;
            }
            mlq.seller_id = q.seller_id;
            mlq.status = q.status;
            mlq.text = q.text;
            mlq.id_question = q.id;

            return mlq;

        }

    }
}