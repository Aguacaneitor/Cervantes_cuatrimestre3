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
    public partial class GestionDeRol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fill_Combobox();
            List<Rol> roles = new List<Rol>();
            Rol rol1 = new Rol();
            Rol rol2 = new Rol();
            rol1.rol_id = 1;
            rol1.rol_descripcion = "admin";
            rol2.rol_id = 2;
            rol2.rol_descripcion = "operador";
            roles.Add(rol1);
            roles.Add(rol2);
            gv_roles.DataSource = roles;
            gv_roles.DataBind();
        }
        private void fill_Combobox()
        {
            lb_rol.Items.Clear();
            lb_rol.Items.Add("Seleccione Rol");
            List<string> listaRoles = RolesListaLN.getInstance().ObtenerListaRoles();
            listaRoles.ForEach(Console.WriteLine);
            foreach (string rol in listaRoles)
            {
                lb_rol.Items.Add(rol);
            }
        }
    }
}