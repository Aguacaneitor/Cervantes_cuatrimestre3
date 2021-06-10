using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidades;
using Capa_LogicaNegocio;


namespace Gestion_administrativa
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_logeo_Click(object sender, EventArgs e)
        {
            string user = txt_usuario.Text;
            string contrasena = txt_contrasena.Text;
            Usuario o_usuario = UsuarioLN.getInstance().AccesoSistema(user, contrasena);

            if (o_usuario != null)
            {
                Response.Write("<script>alert('Usuario Correcto')</script>");
                string nombre = o_usuario.usu_Ape +", "+ o_usuario.usu_Nom;
                Response.Redirect("PanelGeneral.aspx?nombre="+nombre+"&perfil="+ o_usuario.o_rol.rol_descripcion+"&editor="+ o_usuario.usuario);
            }
            else
            {
                Response.Write("<script>alert('Usuario Incorrecto')</script>");
            }
            //Response.Write("<script>alert('Usuario "+user+"; Contraseña: "+ contrasena + "')</script>");
            //string query = "SELECT US.usuario,US.usu_Nom,US.usu_Ape,US.usu_pass,RL.rol_descripcion FROM dbo.Usuarios AS US  LEFT JOIN dbo.roles AS RL ON  US.rol_id = RL.rol_id  WHERE US.usuario = 'admin' AND US.usu_pass = 'admin'";
        }
    }
}