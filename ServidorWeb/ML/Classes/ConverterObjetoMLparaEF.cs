using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Classes
{
    public class ConverterObjetoMLparaEF
    {

        
   
        public void AtualizaOrdem(ML_Order o, Order oML, NSAADMEntities n1)
        {
            try
            {

                if (o.ML_FeedbackBuyer != null)
                {
                    o.ML_FeedbackBuyer.Load();
                }

                if (o.ML_FeedbackSeller != null)
                {
                    o.ML_FeedbackSeller.Load();
                }

                if (o.ML_Payment != null)
                {
                    o.ML_Payment.Load();
                }

                if (oML.status != null)
                {
                    o.status = oML.status;
                }

                if (oML.status_detail != null)
                {
                    o.status_detail = oML.status_detail.description;
                }
                
                AtualizaFeedBackBuyer(o, oML, n1);
                AtualizaFeedBackSeller(o, oML, n1);
                AtualizaShipping(o, oML, n1);
                ML_Payment p = o.ML_Payment.FirstOrDefault();
                if (p != null) AtualizaPagamento(p, oML.payments.First(), n1);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina AtualizaOrdem(ML_Order o, Order oML)", ex);
            }
        }

        private void AtualizaShipping(ML_Order o, Order oML, NSAADMEntities n)
        {
            if (oML.shipping.id != null)
            {
                o.ML_Shipping.Load();

                ML_Shipping ship;
                
                ship = o.ML_Shipping.FirstOrDefault();

                if (ship == null)
                {

                    ship = ConverteShipping(oML, n);
                    o.ML_Shipping.Add(ship);

                }
                else
                {
                    ship.status = oML.shipping.status;
                    ship.shipment_type = oML.shipping.shipment_type;
                    ship.currency_id = oML.shipping.currency_id;
                    ship.cost = oML.shipping.cost;

                }


            }
        }

        private void AtualizaFeedBackBuyer(ML_Order o, Order oML, NSAADMEntities n)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina AtualizaFeedBackBuyer.", ex);
            }

        }

        private void AtualizaFeedBackSeller(ML_Order o, Order oML, NSAADMEntities n)
        {

            try
            {


                if (o.id == 777585100)
                {
                    string a;
                }

                if (o.ML_FeedbackSeller.Count > 0)
                {
                    if (RetornaFeedBackSeller(oML) != null)
                    {
                        ML_FeedbackSeller MLs = o.ML_FeedbackSeller.First();
                        Sale s = RetornaFeedBackSeller(oML);

                        MLs.date_created = s.date_created;
                        MLs.fulfilled = s.fulfilled.ToString();
                        MLs.rating = s.rating;
                        o.ML_FeedbackSeller.Add(MLs);
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

                        
                        o.ML_FeedbackSeller.Add(MLs1);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina AtualizaFeedBackSeller", ex);
            }

        }

        private void AtualizaPagamento(ML_Payment p, Payment py, NSAADMEntities n)
        {
            try
            {
                p.currency_id = py.currency_id;
                p.date_created = py.date_created;
                p.date_last_updated = py.date_last_updated;
                p.status = py.status;
                p.transaction_amount = py.transaction_amount;
                if (p.id == 0) p.id = py.id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina AtualziaPagamento.",ex);
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
        public ML_Item ConverteItemParaML_Item(Item i)
        {
            ML_Item it = new ML_Item();

            try
            {
                it.id = i.id;
                it.title = i.title;
                it.variation_id = i.variation_id;

                return it;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro na rotina ConverteItemParaML_Item", ex);

            }

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
        public ML_Order ConverteOrdem(Order o, NSAADMEntities n)
        {

            try
            {

                ControlaUsuario ControlaUsu = new ControlaUsuario();
                ControlaItem ControlaIt = new ControlaItem();
                ControlaEndereco ControlEnd = new ControlaEndereco();
                ConstruirEF cf = new ConstruirEF();
                
                ML_Order ord = new ML_Order();



                //CONVERTENDO COMPRADOR E VENDEDOR
                ML_Usuario buy = ControlaUsu.RetonarUsuario(o.buyer.id, n);
                if (buy == null)
                {
                    buy = ConverteUsuario(o.buyer);
                }

                ML_Usuario sel = ControlaUsu.RetonarUsuario(o.seller.id, n);
                if (sel == null)
                {
                    sel = ConverteUsuario(o.seller);
                }

                ord.ML_Usuario = sel;
                ord.ML_Usuario1 = buy;



                
                //ITENS DA ORDEM
                ML_OrderItem oi;
                foreach (OrderItem item in o.order_items)
                {
                    oi = new ML_OrderItem();
                    oi.currency_id = item.currency_id;
                    oi.quantity = item.quantity;
                    oi.unit_price = item.unit_price;

                    ML_Item mitem = ControlaIt.RetonarItem(o.order_items[0].item.id, n);
                    if (mitem == null)
                    {
                        mitem = ConverteItem(o.order_items[0].item);
                    }
                    
                    oi.ML_Item = mitem;
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


                            ord.ML_FeedbackSeller.Add(fees);
                        }
                    }
                }

                ML_Shipping s = new ML_Shipping();
                s = ConverteShipping(o, n);
                if (s != null) ord.ML_Shipping.Add(s);

                //DADOS DA ORDEM
                ord.id = o.id;
                ord.currency_id = o.currency_id;
                ord.date_closed = o.date_closed;
                ord.date_created = o.date_created;
                ord.status = o.status;
                //ord.status_detail = o.status_detail.description;
                ord.total_amount = o.total_amount;

                return ord;

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro na rotina de ConverteOrdem. OrdemID: {0}",o.id), ex);
            }
        }
        public ML_Usuario ConverteUsuario(Usuario us)
        {


            //DADOS DA BASE DA CLASSE
            ML_Usuario u = new ML_Usuario();


            try
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
                }

                //if (us.billing_info != null)
                //{
                //    ML_Identification ident = new ML_Identification();
                //    ident.type = us.billing_info.doc_type;
                //    ident.number = us.billing_info.doc_number;
                //    u.ML_Identification.Add(ident);
                //}


                if (us.phone != null)
                {
                    //TELEFONES
                    ML_Phone pho = new ML_Phone();
                    pho.area_code = us.phone.area_code;
                    pho.extension = us.phone.extension;
                    pho.number = us.phone.number;
                    pho.varified = us.phone.verified.ToString();
                    u.ML_Phone.Add(pho);

                    if (us.alternative_phone != null)
                    {
                        pho = new ML_Phone();
                        pho.area_code = us.alternative_phone.area_code;
                        pho.extension = us.alternative_phone.extension;
                        pho.number = us.alternative_phone.number;
                        u.ML_Phone.Add(pho);
                    }
                }

                //TRATANDO REPUTAÇÃO DE VENDEDOR
                //if (us.seller_reputation != null)
                //{
                //    ML_SellerReputation sr = new ML_SellerReputation();
                //    sr.power_seller_status = us.seller_reputation.power_seller_status;
                //    sr.level_id = us.seller_reputation.level_id;
                //    u.ML_SellerReputation.Add(sr);

                //    ML_TransactionsSeller ts = new ML_TransactionsSeller();
                //    ts.canceled = us.seller_reputation.transactions.canceled;
                //    ts.completed = us.seller_reputation.transactions.completed;
                //    ts.period = us.seller_reputation.transactions.period;
                //    ts.total = us.seller_reputation.transactions.total;
                //    ts.ML_SellerReputation = sr;
                //    sr.ML_TransactionsSeller.Add(ts);

                //    ML_Ratings rt = new ML_Ratings();
                //    rt.ML_TransactionsSeller = ts;
                //    rt.negative = us.seller_reputation.transactions.ratings.negative;
                //    rt.neutral = us.seller_reputation.transactions.ratings.neutral;
                //    rt.positive = us.seller_reputation.transactions.ratings.positive;
                //    ts.ML_Ratings.Add(rt);
                //}


                //TRATANDO REPUTAÇÃO COMPRADOR
                //if (us.buyer_reputation != null)
                //{
                //    ML_BuyerReputation mb = new ML_BuyerReputation();
                //    mb.canceled_transactions = us.buyer_reputation.canceled_transactions;
                //    u.ML_BuyerReputation.Add(mb);

                //    ML_TransactionsBuyer tb = new ML_TransactionsBuyer();
                //    tb.completed = us.buyer_reputation.transactions.completed;
                //    tb.period = us.buyer_reputation.transactions.period;
                //    tb.total = us.buyer_reputation.transactions.total;
                //    tb.ML_BuyerReputation = mb;

                //    ML_ResumoTransBuyer canceled = new ML_ResumoTransBuyer();
                //    canceled.paid = us.buyer_reputation.transactions.canceled.paid;
                //    canceled.total = us.buyer_reputation.transactions.canceled.total;
                //    canceled.units = us.buyer_reputation.transactions.canceled.units;
                //    tb.ML_ResumoTransBuyer2 = canceled;

                //    ML_ResumoTransBuyer unrated = new ML_ResumoTransBuyer();
                //    unrated.paid = us.buyer_reputation.transactions.unrated.paid;
                //    unrated.total = us.buyer_reputation.transactions.unrated.total;
                //    unrated.units = us.buyer_reputation.transactions.unrated.units;
                //    tb.ML_ResumoTransBuyer1 = unrated;

                //    ML_ResumoTransBuyer not_yet_rated = new ML_ResumoTransBuyer();
                //    not_yet_rated.paid = us.buyer_reputation.transactions.not_yet_rated.paid;
                //    not_yet_rated.total = us.buyer_reputation.transactions.not_yet_rated.total;
                //    not_yet_rated.units = us.buyer_reputation.transactions.not_yet_rated.units;
                //    tb.ML_ResumoTransBuyer = not_yet_rated;
                //}


                return u;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina ConverteUsuario2.", ex);
            }


        }
        public ML_Item ConverteItem(Item i)
        {
            ML_Item it = new ML_Item();


            try
            {

                it.id = i.id;
                it.title = i.title;
                it.variation_id = i.variation_id;

                return it;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina ConverteItem2.", ex);

            }



        }
        public ML_State ConverteState(State s)
        {
            ML_State st = new ML_State();


            try
            {

                st.id = s.id;
                st.name = s.name;

                return st;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina ConverteState.", ex);

            }
        }
        public ML_City ConverteCity(City s)
        {
            ML_City st = new ML_City();


            try
            {

                st.id = s.id;
                st.name = s.name;

                return st;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina ConverteCity.", ex);

            }
        }
        public ML_Country ConverteCountry(Country s)
        {
            ML_Country st = new ML_Country();


            try
            {

                st.id = s.id;
                st.name = s.name;

                return st;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina ConverteCountry.", ex);

            }
        }
        public ML_Shipping ConverteShipping(Order o, NSAADMEntities n)
        {
            
            ControlaEndereco ControlEnd = new ControlaEndereco();

            if (o.shipping.id != null)
            {
                //IMPLEMENTAR ENDERECO
                ML_Shipping s = new ML_Shipping();
                s.cost = o.shipping.cost;
                s.currency_id = o.shipping.currency_id;
                if (o.shipping.date_created != null)
                {
                    s.date_created = Convert.ToDateTime(o.shipping.date_created);
                }
                s.id = Convert.ToDecimal(o.shipping.id);
                s.shipment_type = o.shipping.shipment_type;
                s.status = o.shipping.status;
                
                if (o.shipping.receiver_address != null && o.shipping.receiver_address.id != null)
                {

                    ML_ReceiverAddress rc;
                    decimal d = Convert.ToDecimal(o.shipping.receiver_address.id);
                    rc = (from p in n.ML_ReceiverAddress where p.ID == d select p).FirstOrDefault();
                    if (rc == null)
                    {
                        rc = new ML_ReceiverAddress();
                        rc.address_line = o.shipping.receiver_address.address_line;
                        rc.comment = o.shipping.receiver_address.comment;
                        rc.ID = Convert.ToDecimal(o.shipping.receiver_address.id.ToString());
                        //rc.latitude = o.shipping.receiver_address.latitude.ToString(); Todos vem Null
                        //rc.longitude = o.shipping.receiver_address.longitude.ToString();
                        rc.zip_code = o.shipping.receiver_address.zip_code;


                        rc.ML_State = ControlEnd.RetonarStado(o.shipping.receiver_address.state.id, n);
                        if (rc.ML_State == null && o.shipping.receiver_address.state.id != null)
                        {
                            rc.ML_State = ConverteState(o.shipping.receiver_address.state);
                        }

                        rc.ML_City = ControlEnd.RetonarCidade(o.shipping.receiver_address.city.id, n);
                        if (rc.ML_City == null && o.shipping.receiver_address.city.id != null)
                        {
                            rc.ML_City = ConverteCity(o.shipping.receiver_address.city);
                        }

                        rc.ML_Country = ControlEnd.RetonarPais(o.shipping.receiver_address.country.id, n);
                        if (rc.ML_Country == null && o.shipping.receiver_address.country.id != null)
                        {
                            rc.ML_Country = ConverteCountry(o.shipping.receiver_address.country);
                        }
                    }

                    s.ML_ReceiverAddress = rc;
                }
                return s;
            }
            else
            {
                return null;
            }
        }
    }
}
