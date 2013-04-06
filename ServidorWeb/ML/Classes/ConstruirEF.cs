﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;
using System.Configuration;
using System.Threading;


namespace ServidorWeb.ML.Classes
{

    public enum Entities
    {
        Bot,
        MercadoLivre

    }


    public class ConstruirEF
    {

        public Object RecuperaEntity(Entities en)
        {

            Object EF;

            if (en == Entities.MercadoLivre)
            {
                EF = new NSAADMEntities(stringdeconexao());
                return (NSAADMEntities)EF;
            }
            else if (en == Entities.Bot)
            {
                EF = new BotWoWEntities(stringdeconexao());
                return (BotWoWEntities)EF;
            }
            else
            {
                throw new Exception("Enumerador inválido");
            }

        }

        
        private string stringdeconexao()
        {
            try
            {
                string NomePC = System.Net.Dns.GetHostName();
                string strConexao = ConfigurationManager.ConnectionStrings[NomePC].ConnectionString;
                return strConexao;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na rotina stringdeconexao",ex);
            }

        }

    }
}