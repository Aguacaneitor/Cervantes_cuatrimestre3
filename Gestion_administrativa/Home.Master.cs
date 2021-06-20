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
        public static string usuario_conectado {set;get;}

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["nombre"] != null)
            {
                Lbl_nombreBienvenida.Text = Request.QueryString["nombre"];
                //usuario_conectado = Request.QueryString["usuario"];
                usuario_conectado = "?nombre=" + Request.QueryString["nombre"] + "&perfil=" + Request.QueryString["perfil"] + "&editor=" + Request.QueryString["editor"];
            }
            else
            {
                Lbl_nombreBienvenida.Text = "Usuario";
            }                
        }
    }
}