using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.BD;
using ServidorWeb.EntityContext;
using Newtonsoft.Json;
using ServidorWeb.ML.Classes;
using MercadoLibre.SDK;
using RestSharp;

namespace ServidorWeb.ML.Paginas
{
    public partial class CallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            

            NSAADMEntities n = new NSAADMEntities();


            //string teste = "{\"user_id\":\"66129937\",\"resource\":\"/questions/2681174350\",\"topic\":\"questions\",\"received\":\"2013-04-01T17:15:47.823Z\",\"application_id\":5971480328026573,\"sent\":\"2013-04-01T17:15:48.282Z\",\"attempts\":0}";


            
            //*****************************************
            Request.InputStream.Position = 0;
            System.IO.StreamReader str = new System.IO.StreamReader(Request.InputStream);
                        
            CallBackTemp c = new CallBackTemp();
            c = JsonConvert.DeserializeObject<CallBackTemp>(str.ReadToEnd());
            //c = JsonConvert.DeserializeObject<CallBackTemp>(teste);
            //*****************************************


            try
            {
                CallBackML teste = (from p in n.CallBackMLs where p.resource == c.resource select p).First();

            }
            catch (Exception)
            {
                
                CallBackML call = new CallBackML();
                call.received = c.received;
                call.resource = c.resource;
                call.sent = c.sent;
                call.topic = c.topic;
                call.userID = c.user_id;
            
                n.CallBackMLs.AddObject(call);
            
                n.SaveChanges();

            }


        }
    }
}