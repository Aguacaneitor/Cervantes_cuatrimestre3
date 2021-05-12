using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gestion_administrativa
{
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["nombre"] != null)
            {
                Lbl_nombreBienvenida.Text = Request.QueryString["nombre"];
            }
            else
            {
                Lbl_nombreBienvenida.Text = "Usuario";
            }                
        }
    }
}