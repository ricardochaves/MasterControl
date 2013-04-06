using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ServidorWeb.BD;
using ServidorWeb.EntityContext;

namespace ServidorWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

            NSAADMEntities n = new NSAADMEntities();

            try
            {
                var u = (from p in n.UsuarioAdms where p.nome == Login1.UserName && p.senha == Login1.Password select p).First();

                e.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);

            }
            catch (Exception)
            {

                throw;
            }

            //if ((Login1.UserName == "admin") && (Login1.Password == "123"))
            //{
            //    e.Authenticated = true;
            //    FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
            //}
            //else
            //{
            //    e.Authenticated = false;
            //}
        }
    }
}