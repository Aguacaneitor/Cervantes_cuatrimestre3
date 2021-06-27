using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidades;
using Capa_LogicaNegocio;
using System.Text.RegularExpressions;

namespace Gestion_administrativa
{
    public partial class GestionDeContacto : System.Web.UI.Page
    {
        private static List<Telefono> telefonos_almacenados = new List<Telefono>();
        static private List<int> ID_contacto = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario_r"] != null && !IsPostBack)
            {
                txt_usuario_buscado.Text = Request.QueryString["usuario_r"];
                //llenar_gridview(DireccionesLN.getInstance().ObtenerListaDirecciones(txt_usuario_buscado.Text));
                string nombre = Request.QueryString["nombre_r"] != null ? Request.QueryString["nombre_r"] : Request.QueryString["usuario_r"];
                dpl_contacto.Items.Clear();
                dpl_contacto.Items.Add("Registre el primer domicilio de " + nombre + ".");
            }
            else if (dpl_contacto.Items.Count == 0 && dpl_contacto.Items.Count > 0)
            {
                dpl_contacto.Items.Add("Ingrese y busque un usuario para editar.");
            }
            if (!IsPostBack)
            {            
                dpl_tipo.Items.Clear();
                dpl_tipo.Items.Add("Seleccione tipo de contacto.");
                dpl_tipo.Items.Add("FIJO");
                dpl_tipo.Items.Add("MOVIL");
                dpl_prioridad.Items.Clear();
                dpl_prioridad.Items.Add("Seleccione la prioridad del contacto.");
                dpl_prioridad.Items.Add("1");
                dpl_codigoarea.Items.Clear();
                dpl_codigoarea.Items.Add("Seleccione");
                dpl_codigoarea.Items.Add("011");
                dpl_codigoarea.Items.Add("0351");
                dpl_codigoarea.Items.Add("0379");
                dpl_codigoarea.Items.Add("0370");
                dpl_codigoarea.Items.Add("0221");
                dpl_codigoarea.Items.Add("0380");
                dpl_codigoarea.Items.Add("0261");
                dpl_codigoarea.Items.Add("0299");
                dpl_codigoarea.Items.Add("0343");
                dpl_codigoarea.Items.Add("0376");
                dpl_codigoarea.Items.Add("0280");
                dpl_codigoarea.Items.Add("0362");
                dpl_codigoarea.Items.Add("2966");
                dpl_codigoarea.Items.Add("0387");
                dpl_codigoarea.Items.Add("0383");
                dpl_codigoarea.Items.Add("0264");
                dpl_codigoarea.Items.Add("0266");
                dpl_codigoarea.Items.Add("0381");
                dpl_codigoarea.Items.Add("0388");
                dpl_codigoarea.Items.Add("0342");
                dpl_codigoarea.Items.Add("2954");
                dpl_codigoarea.Items.Add("0385");
                dpl_codigoarea.Items.Add("2920");
                dpl_codigoarea.Items.Add("2901");
            }

            /*if (!IsPostBack)
            {
                llenar_cb_proloba();
            }*/
        }

        private void limpiar()
        {
            btn_registrar.Text = "REGISTRAR";
            dpl_codigoarea.SelectedValue = "Seleccione";
            dpl_tipo.SelectedValue = "Seleccione tipo de contacto.";
            dpl_prioridad.SelectedValue = "Seleccione la prioridad del contacto.";
            txt_nro.Text = "";
            txt_usuario_buscado.Text = "";
            llenar_gridview(null);
            btn_eliminar.Visible = false;
        }

        protected void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario_buscado.Text != "")
            {
                llenar_gridview(TelefonosLN.getInstance().ObtenerListaTelefonos(txt_usuario_buscado.Text));
            }
            else
            {
                Response.Write("<script>alert('Ingrese un usuario para buscar.')</script>");
            }            
        }

        private void llenar_gridview(List<Telefono> telefonos)
        {
            gv_telefonos.DataSource = null;
            gv_telefonos.DataSource = telefonos;
            gv_telefonos.DataBind();
            dpl_contacto.Items.Clear();

            if (telefonos == null)
            {
                dpl_contacto.Items.Add("Ingrese y busque un usuario para editar.");
            }
            else
            {
                telefonos_almacenados = telefonos;
                if (telefonos.Count > 0)
                {
                    dpl_prioridad.Items.Clear();
                    dpl_prioridad.Items.Add("Seleccione la prioridad del contacto.");
                    dpl_contacto.Items.Add("Seleccione un contacto para editar.");
                    int minima_prioridad = 0;
                    foreach (Telefono telefono in telefonos.OrderBy(x => x.tel_prioridad))
                    {
                        ID_contacto.Add(telefono.tel_id);
                        string telefono_formateado = "Tipo: ";
                        telefono_formateado += telefono.tel_tipo;
                        telefono_formateado += ", Numero: " + telefono.tel_nro;
                        telefono_formateado += ", Prioridad: " + telefono.tel_prioridad;
                        dpl_contacto.Items.Add(telefono_formateado);
                        dpl_prioridad.Items.Add("" + telefono.tel_prioridad);
                        minima_prioridad = telefono.tel_prioridad > minima_prioridad ? telefono.tel_prioridad : minima_prioridad;
                    }
                    dpl_prioridad.Items.Add("" + (minima_prioridad+1));
                }
                else
                {
                    dpl_contacto.Items.Add("Registre el primer contacto de " + txt_usuario_buscado.Text + ".");
                    dpl_prioridad.Items.Clear();
                    dpl_prioridad.Items.Add("Seleccione la prioridad del contacto.");
                    dpl_prioridad.Items.Add("1");
                }
            }
        }

        private void llenar_prioridades(bool editar)
        {
            dpl_prioridad.Items.Clear();
            dpl_prioridad.Items.Add("Seleccione la prioridad del contacto.");
            int minima_prioridad = 0;
            foreach (Telefono telefono in telefonos_almacenados.OrderBy(x => x.tel_prioridad))
            {
                dpl_prioridad.Items.Add("" + telefono.tel_prioridad);
                minima_prioridad = telefono.tel_prioridad > minima_prioridad ? telefono.tel_prioridad : minima_prioridad;
            }            
            if (!editar)
            {
                dpl_prioridad.Items.Add("" + (minima_prioridad + 1));
            }
        }

        protected void dpl_contacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 tel_id = ID_contacto[dpl_contacto.SelectedIndex - 1];
                Telefono o_telefono = new Telefono();
                foreach (Telefono telefono in telefonos_almacenados)
                {
                    if (telefono.tel_id == tel_id)
                    {
                        o_telefono = telefono;
                        break;
                    }
                }
                if (o_telefono != null)
                {
                    llenar_prioridades(true);
                    dpl_codigoarea.SelectedValue = extrae_codigo_area(o_telefono.tel_nro);
                    dpl_tipo.SelectedValue = o_telefono.tel_tipo;
                    dpl_prioridad.SelectedValue = "" + o_telefono.tel_prioridad;
                    txt_nro.Text = (""+o_telefono.tel_nro).Substring(4);
                    btn_registrar.Text = " EDITAR ";
                    btn_eliminar.Visible = true;
                }

            }
            catch (Exception ex)
            {
                //limpiar();
                llenar_prioridades(false);
                btn_registrar.Text = "REGISTRAR";
                dpl_codigoarea.SelectedValue = "Seleccione";
                dpl_tipo.SelectedValue = "Seleccione tipo de contacto.";
                dpl_prioridad.SelectedValue = "Seleccione la prioridad del contacto.";
                txt_nro.Text = "";
                btn_eliminar.Visible = false;
             }
        }

        private string extrae_codigo_area(string numero)
        {
            string salida = "";
            for (int indice = 0; indice < dpl_codigoarea.Items.Count; indice++)
            {
                bool encontrado = numero.StartsWith(dpl_codigoarea.Items[indice].Value);
                if (encontrado)
                {
                    salida = dpl_codigoarea.Items[indice].Value;
                    break;
                }
            }
            return salida;
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            string txt_salida = "";
            if (dpl_codigoarea.SelectedValue == "Seleccione")
            {
                txt_salida = "Codigo de Area ";
            }
            if (dpl_prioridad.SelectedValue == "Seleccione la prioridad del contacto.")
            {
                txt_salida = "Prioridad ";
            }
            if (dpl_tipo.SelectedValue == "Seleccione tipo de contacto.")
            {
                txt_salida = "Tipo de Contacto ";
            }
            if (!(Regex.Match(txt_nro.Text, @"[0-9]*").Value == txt_nro.Text) | "" == txt_nro.Text)
            {
                txt_salida = "Número de Telefono ";
            }
            if (txt_salida != "")
            {
                Response.Write("<script>alert('Campos erroneamente cargados: " + txt_salida + "')</script>");
            }
            else
            {
                Telefono o_telefono = new Telefono();
                o_telefono.tel_nro = dpl_codigoarea.SelectedValue + txt_nro.Text;
                o_telefono.tel_prioridad = Convert.ToInt32(dpl_prioridad.SelectedValue);
                o_telefono.tel_tipo = dpl_tipo.SelectedValue;
                Usuario o_usuario_temp = new Usuario();
                if (btn_registrar.Text == "REGISTRAR")
                {
                    o_usuario_temp.usuario = txt_usuario_buscado.Text;
                    o_telefono.tel_usuario = o_usuario_temp;
                    if (TelefonosLN.getInstance().RegistrarTelefono(o_telefono))
                    {
                        Response.Write("<script>alert('Se registro con exito.')</script>");
                        limpiar();
                    }
                    else
                    {
                        Response.Write("<script>alert('Ha habido un error intente nuevamente.')</script>");
                    }
                }
                else
                {
                    try
                    {
                        Int32 tel_id = ID_contacto[dpl_contacto.SelectedIndex - 1];
                        o_telefono.tel_id = tel_id;
                        Telefono o_telefono_temp = new Telefono();
                        foreach (Telefono telefono in telefonos_almacenados)
                        {
                            if (telefono.tel_id == tel_id)
                            {
                                o_telefono_temp = telefono;
                                break;
                            }
                        }
                        if (o_telefono != null)
                        {
                            o_usuario_temp = o_telefono_temp.tel_usuario;
                        }
                        else
                        {
                            o_usuario_temp.usuario = txt_usuario_buscado.Text;
                            o_telefono_temp = null;
                        }
                        o_telefono.tel_usuario = o_usuario_temp;
                        //EdiTAR
                        if (TelefonosLN.getInstance().EditarTelefono(o_telefono))
                        {
                            Response.Write("<script>alert('Se edito con exito el contacto.')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Ha habido un error intente nuevamente.')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Ha habido un error intente nuevamente.')</script>");
                    }
                    finally
                    {
                        limpiar();
                    }
                }
            }
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 tel_id = ID_contacto[dpl_contacto.SelectedIndex - 1];
                Telefono o_telefono = new Telefono();
                foreach (Telefono telefono in telefonos_almacenados)
                {
                    if (telefono.tel_id == tel_id)
                    {
                        o_telefono = telefono;
                        break;
                    }
                }
                if (TelefonosLN.getInstance().EliminarTelefono(o_telefono))
                {
                    Response.Write("<script>alert('Se elimino con exito el contacto.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Ha habido un error intente nuevamente.')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ha habido un error intente nuevamente.')</script>");
            }
            finally
            {
                limpiar();
            }
        }
    }
}