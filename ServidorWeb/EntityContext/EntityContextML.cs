using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWeb.BD;

namespace ServidorWeb.EntityContext
{
    public static class EntityContextML
    {
        public static NSAADMEntities _context;

        public static NSAADMEntities GetContext
        {
            get
            {
                if (_context == null)
                {
                    _context = new NSAADMEntities();
                }

                return _context;
            }

        }
    }
}