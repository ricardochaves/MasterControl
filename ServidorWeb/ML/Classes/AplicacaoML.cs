using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWeb.ML.Classes
{
    public static class AplicacaoML
    {
        public static long RetornaIDAplicacaoML()
        {
            try
            {
                long ID;
                string NomePC = System.Net.Dns.GetHostName();

                if (NomePC == "Ricardo-PC")
                {
                    ID = Convert.ToInt64(ServidorWeb.Properties.Settings.Default.IDAplicacaoML_HML);
                }
                else if (NomePC == "PC_O1")
                {
                    ID = Convert.ToInt64(ServidorWeb.Properties.Settings.Default.IDAplicacaoML_HML);
                }
                else if (NomePC == "DGTEC-DESIS-RBC")
                {
                    ID = Convert.ToInt64(ServidorWeb.Properties.Settings.Default.IDAplicacaoML_HML);
                }
                else
                {
                    ID = Convert.ToInt64(ServidorWeb.Properties.Settings.Default.IDAplicacaoML_PRD);
                }

                return ID;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina RetornaIDAplicacaoML", ex);
            }
        }
        public static string RetornaKeyAplicacaoML()
        {
            try
            {
                string Key;
                string NomePC = System.Net.Dns.GetHostName();

                if (NomePC == "Ricardo-PC")
                {
                    Key = ServidorWeb.Properties.Settings.Default.SecretKeyML_HML;
                }
                else if (NomePC == "PC_O1")
                {
                    Key = ServidorWeb.Properties.Settings.Default.SecretKeyML_HML;
                }
                else if (NomePC == "DGTEC-DESIS-RBC")
                {
                    Key = ServidorWeb.Properties.Settings.Default.SecretKeyML_HML;
                }
                else
                {
                    Key = ServidorWeb.Properties.Settings.Default.SecretKeyML_PRD;
                }

                return Key;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina RetornaKeyAplicacaoML", ex);
            }
        }
        public static string RetornaURLLoginAplicacaoML()
        {
            try
            {
                string URL;
                string NomePC = System.Net.Dns.GetHostName();

                if (NomePC == "Ricardo-PC")
                {
                    URL = ServidorWeb.Properties.Settings.Default.URL_Login_HML;
                }
                else if (NomePC == "PC_O1")
                {
                    URL = ServidorWeb.Properties.Settings.Default.URL_Login_HML;
                }
                else if (NomePC == "DGTEC-DESIS-RBC")
                {
                    URL = ServidorWeb.Properties.Settings.Default.URL_Login_HML;
                }
                else
                {
                    URL = ServidorWeb.Properties.Settings.Default.URL_Login;
                }

                return URL;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina RetornaURLLoginAplicacaoML", ex);
            }
        }
    }
}
