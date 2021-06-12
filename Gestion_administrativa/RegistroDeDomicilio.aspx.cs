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
    public partial class RegistroDeDomicilio : System.Web.UI.Page
    {
        static List<Direccion> direcciones_almacenadas = new List<Direccion>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario_r"] != null)
            {                
                txt_usuario_buscado.Text = Request.QueryString["usuario_r"];
                llenar_gridview(DireccionesFiltradasLN.getInstance().ObtenerListaDirecciones(txt_usuario_buscado.Text));
                string nombre = Request.QueryString["nombre_r"] != null ? Request.QueryString["nombre_r"] : Request.QueryString["usuario_r"];
                dpl_domicilio.Items.Clear();
                dpl_domicilio.Items.Add("Registre el primer domicilio de "+ nombre + ".");
            } else if (dpl_domicilio.Items.Count == 0)
            {
                dpl_domicilio.Items.Add("Ingrese y busque un usuario para editar.");
            }
            dpl_provincia.Items.Clear();
            dpl_localidad.Items.Clear();
            dpl_provincia.Items.Add("Seleccione provincia.");
            dpl_localidad.Items.Add("Seleccione localidad.");
        }

        protected void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            llenar_gridview(DireccionesFiltradasLN.getInstance().ObtenerListaDirecciones(txt_usuario_buscado.Text));
        }

        private void llenar_gridview(List<Direccion> direcciones)
        {
            gv_direcciones.DataSource = null;
            gv_direcciones.DataSource = direcciones;
            gv_direcciones.DataBind();
            dpl_domicilio.Items.Clear();

            if (direcciones == null)
            {
                dpl_domicilio.Items.Add("Ingrese y busque un usuario para editar.");
            }
            else
            {
                direcciones_almacenadas = direcciones;
                dpl_domicilio.Items.Add("Seleccione domicilio a editar.");
                foreach (Direccion direccion in direcciones)
                {
                    string domicilio_formateado = direccion.dir_id+": ";
                    domicilio_formateado += direccion.usu_CP;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_localidad.loc_provincia.provincia_nombre : " " + direccion.dir_localidad.loc_provincia.provincia_nombre;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_localidad.loc_nombre : ", " + direccion.dir_localidad.loc_nombre;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_barrio.barrio_nombre : " " + direccion.dir_barrio.barrio_nombre;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_calle : ", " + direccion.dir_calle;
                    domicilio_formateado += domicilio_formateado == "" ? "" + direccion.dir_altura : " " + direccion.dir_altura;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_piso : " " + direccion.dir_piso;
                    domicilio_formateado += domicilio_formateado == "" ? direccion.dir_dpto : " " + direccion.dir_dpto;
                    dpl_domicilio.Items.Add(domicilio_formateado);
                }
            }
        }

        protected void dpl_domicilio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 dir_id = Convert.ToInt32(dpl_domicilio.SelectedValue.Substring(0, dpl_domicilio.SelectedValue.IndexOf(":")));
                Direccion o_direccion = new Direccion();
                foreach (Direccion direccion in direcciones_almacenadas)
                {
                    if (direccion.dir_id == dir_id)
                    {
                        o_direccion = direccion;
                        break;
                    }
                }
                if (o_direccion != null)
                {
                    dpl_provincia.SelectedValue = o_direccion.dir_localidad.loc_provincia.provincia_nombre;
                    dpl_localidad.SelectedValue = o_direccion.dir_localidad.loc_nombre;
                    txt_barrio.Text = o_direccion.dir_barrio.barrio_nombre;
                    txt_usu_calle.Text = o_direccion.dir_calle;
                    txt_usu_altura.Text = "" + o_direccion.dir_altura;
                    txt_usu_piso.Text = o_direccion.dir_piso;
                    txt_usu_dpto.Text = o_direccion.dir_dpto;
                    txt_usu_CP.Text = o_direccion.usu_CP;
                    btn_registrar.Text = " Editar ";
                }
                
            }
            catch (Exception ex)
            {
                btn_registrar.Text = "Registrar";
                dpl_provincia.SelectedValue = "Seleccione provincia.";
                dpl_localidad.SelectedValue = "Seleccione localidad.";
                txt_barrio.Text = "";
                txt_usu_calle.Text = "";
                txt_usu_altura.Text = "";
                txt_usu_piso.Text = "";
                txt_usu_dpto.Text = "";
                txt_usu_CP.Text = "";
            }
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }
    }
}