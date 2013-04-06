using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb
{
    public class EntityContextBot
    {
        public static BotWoWEntities _context;

        public static BotWoWEntities GetContext
        {
            get
            {
                if (_context == null)
                {
                    _context = new BotWoWEntities();
                }

                return _context;
            }

        }
    }
}