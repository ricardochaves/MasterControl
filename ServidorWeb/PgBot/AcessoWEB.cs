using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWeb.PgBot
{
    public class AcessoWEB
    {
        public string RecuperaJSON(string url)
        {
            try
            {
                System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
                System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse;
                System.IO.StreamReader _readResponse = new System.IO.StreamReader(response.GetResponseStream());
                return _readResponse.ReadToEnd();
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(string.Format("Erro na rotina RecuperaJSON. {0} url: {1} {0}", Environment.NewLine, url), e);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro na rotina RecuperaJSON. {0} url: {1} {0}", Environment.NewLine, url), ex);
            }

        }


    }
}