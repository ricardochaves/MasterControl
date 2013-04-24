using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using ServidorWeb.ML.Classes;
using ServidorWeb.BD;

namespace ServidorWeb.ML.Paginas
{
    public partial class ImportaMovimentacaoMP : System.Web.UI.Page
    {

        


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private JObject RetornaMovimentos(MP m, Int32 offset)
        {
            // Sets the filters you want
            Dictionary<String, String> filters = new Dictionary<String, String>();
            filters.Add("site_id", "MLB"); // Argentina: MLA; Brasil: MLB

            return m.searchPayment(filters, offset, 0);
        }
        private void InserirMOvimentos(JObject t)
        {
            NSAADMEntities n;
            
            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);

            decimal temp;
            Nullable<DateTime> tempd;

            foreach (var item in t)
            {

                string a = item.ToString();

                JToken j = t["response"];

                JToken s = j["results"];

                foreach (var i in s)
                {
                    JToken b = i["collection"];

                    if (VerificaPay(b) == false)
                    {

                        MP_Payments p = new MP_Payments();

                        p.id = (decimal)b["id"];
                        p.last_modified = (DateTime)b["last_modified"];
                        p.marketplace = (string)b["marketplace"];

                        p.installments = b["installments"].ToString();

                        Decimal.TryParse(b["marketplace_fee"].ToString(), out temp);
                        p.marketplace_fee = temp;

                        Decimal.TryParse(b["mercadopago_fee"].ToString(), out temp);
                        p.mercadopago_fee = temp;

                        Decimal.TryParse(b["net_received_amount"].ToString(), out temp);
                        p.net_received_amount = temp;

                        Decimal.TryParse(b["collector_id"].ToString(), out temp);
                        p.collector_id = temp;

                        Decimal.TryParse(b["currency_id"].ToString(), out temp);
                        p.currency_id = temp;

                        Decimal.TryParse(b["external_reference"].ToString(), out temp);
                        p.external_reference = temp;

                        Decimal.TryParse(b["total_paid_amount"].ToString(), out temp);
                        p.total_paid_amount = temp;

                        Decimal.TryParse(b["transaction_amount"].ToString(), out temp);
                        p.transaction_amount = temp;

                        Decimal.TryParse(b["account_money_amount"].ToString(), out temp);
                        p.account_money_amount = temp;

                        Decimal.TryParse(b["shipping_cost"].ToString(), out temp);
                        p.shipping_cost = temp;

                        
                        
                        
                        
                        p.operation_type = (string)b["operation_type"];
                        p.payment_type = (string)b["payment_type"];
                        p.reason = (string)b["reason"];
                        p.released = (string)b["released"];

                        p.site_id = (string)b["site_id"];
                        p.sponsor_id = (string)b["sponsor_id"];
                        p.status = (string)b["status"];
                        p.status_code = (string)b["status_code"];
                        p.status_detail = (string)b["status_detail"];


                        tempd = (Nullable<DateTime>)b["date_approved"];
                        p.date_approved = tempd;
                        p.date_created = (DateTime)b["date_created"];


                        tempd = (Nullable<DateTime>)b["money_release_date"];
                        p.money_release_date = tempd;
                        //p.money_release_date = (DateTime)b["money_release_date"];

                        //USUARIO
                        JToken user = b["payer"];
                        if (VerificaUser(user) == false)
                        {
                            ML_Usuario u = new ML_Usuario();
                            Decimal.TryParse(user["id"].ToString(), out temp);
                            u.id = temp;
                            u.nickname = (string)user["nickname"];
                            u.first_name = (string)user["first_name"];
                            u.last_name = (string)user["last_name"];
                            u.email = (string)user["email"];

                            JToken phone = user["phone"];
                            ML_Phone phon = new ML_Phone();
                            phon.area_code = (string)phone["area_code"];
                            phon.number = (string)phone["number"];
                            phon.extension = (string)phone["extension"];

                            u.ML_Phone.Add(phon);

                            n.ML_Usuario.AddObject(u);
                        }

                        n.AddToMP_Payments(p);

                        n.SaveChanges();
                    }
                }


            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            MP m = new MP("5963566471355778", "iw3YTvnu7oTT5pQFzuww5SBgvfP9or3X");

            for (int i = 0; i < 100; i++)
            {

                JObject teste = RetornaMovimentos(m, (i*10));
                InserirMOvimentos(teste);

            }

        }




        public bool VerificaPay(JToken t)
        {

            NSAADMEntities n;

            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);


            try
            {
                decimal d = (decimal)t["id"];
                MP_Payments pa = (from p in n.MP_Payments where p.id == d select p).First();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;

            }
        }
        public bool VerificaUser(JToken t)
        {

            NSAADMEntities n;

            ConstruirEF cf = new ConstruirEF();
            n = (NSAADMEntities)cf.RecuperaEntity(Entities.MercadoLivre);


            try
            {
                decimal d = (decimal)t["id"];
                ML_Usuario pa = (from p in n.ML_Usuario where p.id == d select p).First();

                return true;

            }
            catch (InvalidOperationException)
            {
                if (t["nickname"].ToString() == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}