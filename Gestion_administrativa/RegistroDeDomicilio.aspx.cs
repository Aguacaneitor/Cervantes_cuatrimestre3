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
    public partial class RegistroDeDomicilio : System.Web.UI.Page
    {
        static private List<int> ID_domicilio = new List<int>();
        static List<Direccion> direcciones_almacenadas = new List<Direccion>();
        static List<Barrio> barrios_almacenados = new List<Barrio>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario_r"] != null)
            {                
                txt_usuario_buscado.Text = Request.QueryString["usuario_r"];
                llenar_gridview(DireccionesLN.getInstance().ObtenerListaDirecciones(txt_usuario_buscado.Text));
                string nombre = Request.QueryString["nombre_r"] != null ? Request.QueryString["nombre_r"] : Request.QueryString["usuario_r"];
                dpl_domicilio.Items.Clear();
                dpl_domicilio.Items.Add("Registre el primer domicilio de "+ nombre + ".");
            } else if (dpl_domicilio.Items.Count == 0)
            {
                dpl_domicilio.Items.Add("Ingrese y busque un usuario para editar.");
            }
            if (!IsPostBack)
            {
                llenar_cb_proloba();
            }
        }

        private void limpiar_campos()
        {
            llenar_cb_proloba();
            gv_direcciones.DataSource = null;
            gv_direcciones.DataBind();
            dpl_domicilio.Items.Clear();
            dpl_domicilio.Items.Add("Ingrese y busque un usuario para editar.");
            btn_registrar.Text = "REGISTRAR";
            dpl_provincia.SelectedValue = "Seleccione una provincia.";
            txt_usuario_buscado.Text = "";
            txt_usu_calle.Text = "";
            txt_usu_altura.Text = "";
            txt_usu_piso.Text = "";
            txt_usu_dpto.Text = "";
            txt_usu_CP.Text = "";
            btn_eliminar.Visible = false;
        }

        protected void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            llenar_gridview(DireccionesLN.getInstance().ObtenerListaDirecciones(txt_usuario_buscado.Text));
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
                if (direcciones.Count > 0)
                {
                    dpl_domicilio.Items.Add("Seleccione domicilio a editar.");
                    foreach (Direccion direccion in direcciones)
                    {
                        //string direccion_anterior1 = "";
                        //string direccion_anterior2 = "";
                        string atributo_formateado = "";
                        ID_domicilio.Add(direccion.dir_id);
                        string domicilio_formateado = "";
                        domicilio_formateado += direccion.dir_calle;
                        domicilio_formateado += " " + direccion.dir_altura;
                        //direccion_anterior1 = domicilio_formateado;
                        atributo_formateado = direccion.dir_manzana == null ? "" : " MZ: " + direccion.dir_manzana;
                        domicilio_formateado += atributo_formateado;
                        //direccion_anterior2 = domicilio_formateado;
                        atributo_formateado = direccion.dir_piso == null ? "" : " PISO: " + direccion.dir_piso;
                        domicilio_formateado += atributo_formateado;
                        atributo_formateado = direccion.dir_dpto == null ? "" : " DPTO: " + direccion.dir_dpto;
                        domicilio_formateado += atributo_formateado;
                        domicilio_formateado += ", " + direccion.dir_barrio.barrio_nombre;
                        domicilio_formateado += ", LOCALIDAD: " + direccion.dir_barrio.barrio_localidad.loc_nombre;
                        domicilio_formateado += ", PROVINCIA: " + direccion.dir_barrio.barrio_localidad.loc_provincia.provincia_nombre;
                        domicilio_formateado += ", CD: " + direccion.usu_CP;
                        dpl_domicilio.Items.Add(domicilio_formateado);
                    }
                }
                else
                {
                    dpl_domicilio.Items.Add("Registre el primer domicilio de " + txt_usuario_buscado.Text + ".");
                }                
            }
        }

        private List<Barrio> llenar_barrios_almacenados()
        {
            if (barrios_almacenados.Count == 0)
            {
                barrios_almacenados = cbDireccionesListaLN.getInstance().ObtenerListaBarrios();
            }
            return barrios_almacenados;
        }

        private void llenar_cb_proloba()
        {
            llenar_cbo_provincias();
            dpl_localidad.Items.Clear();
            dpl_barrio.Items.Clear();
            dpl_localidad.Items.Add("Seleccione primero provincia.");
            dpl_barrio.Items.Add("Seleccione primero localidad.");
        }
        private void llenar_cbo_provincias()
        {
            List<Barrio> o_barrios_temp = llenar_barrios_almacenados();
            List<string> provincias_temp = new List<string>();

            provincias_temp = o_barrios_temp.Select(x => x.barrio_localidad.loc_provincia.provincia_nombre).Distinct().ToList();
            provincias_temp.Insert(0, "Seleccione una provincia.");
            dpl_provincia.DataSource = null;
            dpl_provincia.DataSource = provincias_temp;
            dpl_provincia.DataBind();
        }

        protected void dpl_domicilio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 dir_id = ID_domicilio[dpl_domicilio.SelectedIndex - 1];
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
                    llenar_cbo_provincias();
                    dpl_provincia.SelectedValue = o_direccion.dir_barrio.barrio_localidad.loc_provincia.provincia_nombre;
                    llenar_cbo_localidades();
                    dpl_localidad.SelectedValue = o_direccion.dir_barrio.barrio_localidad.loc_nombre;
                    llenar_cbo_barrios();
                    dpl_barrio.SelectedValue = o_direccion.dir_barrio.barrio_nombre;
                    txt_usu_calle.Text = o_direccion.dir_calle;
                    txt_usu_altura.Text = "" + o_direccion.dir_altura;
                    txt_usu_piso.Text = o_direccion.dir_piso;
                    txt_usu_dpto.Text = o_direccion.dir_dpto;
                    txt_usu_CP.Text = o_direccion.usu_CP;
                    btn_registrar.Text = " EDITAR ";
                    btn_eliminar.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                btn_registrar.Text = "REGISTRAR";
                llenar_cb_proloba();
                dpl_provincia.SelectedValue = "Seleccione una provincia.";
                txt_usu_calle.Text = "";
                txt_usu_altura.Text = "";
                txt_usu_piso.Text = "";
                txt_usu_dpto.Text = "";
                txt_usu_CP.Text = "";
                btn_eliminar.Visible = false;
            }
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            if (btn_registrar.Text == "REGISTRAR")
            {
                string txt_salida = "";
                if ("" == txt_usuario_buscado.Text)
                {
                    txt_salida = "Debe ingresar el nombre de un usuario en el buscador.";
                }
                if (!(Regex.Match(txt_usu_CP.Text, @"[0-9]*").Value == txt_usu_CP.Text) | "" == txt_usu_CP.Text)
                {
                    txt_salida += "CP ";
                }
                if ("" == txt_usu_calle.Text)
                {
                    txt_salida += "Calle ";
                }
                if (!(Regex.Match(txt_usu_altura.Text, @"[0-9]*").Value == txt_usu_altura.Text) | "" == txt_usu_altura.Text)
                {
                    txt_salida += "Altura ";
                }
                if (Regex.Match(dpl_provincia.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Provincia ";
                }
                if (Regex.Match(dpl_localidad.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Localidad ";
                }
                if (Regex.Match(dpl_barrio.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Barrio ";
                }
                if (txt_salida == "")
                {
                    string parametros = "";
                    try
                    {
                        Direccion o_direccion = new Direccion();
                        Barrio o_barrio_temp = new Barrio();
                        Usuario o_usu_temp = new Usuario();
                        o_direccion.dir_calle = txt_usu_calle.Text;
                        o_direccion.dir_altura = Convert.ToInt32(txt_usu_altura.Text);
                        o_direccion.dir_piso = txt_usu_piso.Text;
                        o_direccion.dir_dpto = txt_usu_dpto.Text;
                        o_direccion.dir_torre = txt_usu_torre.Text;
                        o_direccion.dir_manzana = txt_usu_manzana.Text;
                        o_usu_temp.usuario = txt_usuario_buscado.Text;
                        o_direccion.dir_usuario = o_usu_temp;
                        o_direccion.usu_CP = txt_usu_CP.Text;
                        List<Barrio> o_barrios_temp = llenar_barrios_almacenados();
                        string provincia = dpl_provincia.SelectedValue;
                        string localidad = dpl_localidad.SelectedValue;
                        string barrio = dpl_barrio.SelectedValue;
                        var barrio_id_temp = o_barrios_temp.Where(x => (x.barrio_localidad.loc_provincia.provincia_nombre == provincia) && (x.barrio_localidad.loc_nombre == localidad) && (x.barrio_nombre == barrio)).Select(x => x.barrio_id).ToList();
                        
                        //int barrio_id_temp = Convert.ToInt32(o_barrios_temp.Where(x => x.barrio_localidad.loc_provincia.provincia_nombre == provincia).Select(x => x.barrio_localidad.loc_nombre).Distinct().OrderBy(o => o).ToList());
                        o_barrio_temp.barrio_id = Convert.ToInt32(barrio_id_temp[0]);
                        o_direccion.dir_barrio = o_barrio_temp;
                        bool registrado = DireccionesLN.getInstance().RegistrarDireccion(o_direccion);
                        if (registrado)
                        {
                            if (Request.QueryString["usuario_r"] != null)
                            {
                                //Response.Write("<script>alert('Se registro correctamente el domicilio, finalice la activación del usuario registrando un contacto.')</script>");
                                if (Request.QueryString["nombre"] != null)
                                {
                                    //usuario_conectado = Request.QueryString["usuario"];
                                    parametros = "?nombre=" + Request.QueryString["nombre"] + "&perfil=" + Request.QueryString["perfil"] + "&editor=" + Request.QueryString["editor"] + "&usuario_r=" + Request.QueryString["usuario_r"] + "&nombre_r=" + Request.QueryString["nombre_r"];
                                }
                                else
                                {
                                    parametros = "";
                                }                                
                            }
                            else
                            {
                                Response.Write("<script>alert('Se registro correctamente el domicilio.')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No se pudo registrar el domicilio intente nuevamente.')</script>");
                        }                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (parametros != "")
                        {
                            Response.Redirect("GestionDeContacto.aspx" + parametros);
                        }                        
                    }
                } else
                {
                    Response.Write("<script>alert('Los siguientes campos estan incompletos o con inconvenientes: "+txt_salida+".')</script>");
                }
                
            } else
            {
                string txt_salida = "";
                if ("" == txt_usuario_buscado.Text)
                {
                    txt_salida = "Debe ingresar el nombre de un usuario en el buscador.";
                }
                if (!(Regex.Match(txt_usu_CP.Text, @"[0-9]*").Value == txt_usu_CP.Text) | "" == txt_usu_CP.Text)
                {
                    txt_salida += "CP ";
                }
                if ("" == txt_usu_calle.Text)
                {
                    txt_salida += "Calle ";
                }
                if (!(Regex.Match(txt_usu_altura.Text, @"[0-9]*").Value == txt_usu_altura.Text) | "" == txt_usu_altura.Text)
                {
                    txt_salida += "Altura ";
                }
                if (Regex.Match(dpl_provincia.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Provincia ";
                }
                if (Regex.Match(dpl_localidad.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Localidad ";
                }
                if (Regex.Match(dpl_barrio.SelectedValue, @"Seleccione.*").Value != "")
                {
                    txt_salida += "Barrio ";
                }
                if (txt_salida == "")
                {
                    try
                    {
                        Direccion o_direccion = new Direccion();
                        Barrio o_barrio_temp = new Barrio();
                        Usuario o_usu_temp = new Usuario();
                        o_direccion.dir_calle = txt_usu_calle.Text;
                        o_direccion.dir_altura = Convert.ToInt32(txt_usu_altura.Text);
                        o_direccion.dir_piso = txt_usu_piso.Text;
                        o_direccion.dir_dpto = txt_usu_dpto.Text;
                        o_direccion.dir_torre = txt_usu_torre.Text;
                        o_direccion.dir_manzana = txt_usu_manzana.Text;
                        o_usu_temp.usuario = txt_usuario_buscado.Text;
                        o_direccion.dir_usuario = o_usu_temp;
                        o_direccion.usu_CP = txt_usu_CP.Text;
                        List<Barrio> o_barrios_temp = llenar_barrios_almacenados();
                        string provincia = dpl_provincia.SelectedValue;
                        string localidad = dpl_localidad.SelectedValue;
                        string barrio = dpl_barrio.SelectedValue;
                        var barrio_id_temp = o_barrios_temp.Where(x => (x.barrio_localidad.loc_provincia.provincia_nombre == provincia) && (x.barrio_localidad.loc_nombre == localidad) && (x.barrio_nombre == barrio)).Select(x => x.barrio_id).ToList();
                        Int32 dir_id = ID_domicilio[dpl_domicilio.SelectedIndex - 1];
                        o_direccion.dir_id = dir_id;
                        //int barrio_id_temp = Convert.ToInt32(o_barrios_temp.Where(x => x.barrio_localidad.loc_provincia.provincia_nombre == provincia).Select(x => x.barrio_localidad.loc_nombre).Distinct().OrderBy(o => o).ToList());
                        o_barrio_temp.barrio_id = Convert.ToInt32(barrio_id_temp[0]);
                        o_direccion.dir_barrio = o_barrio_temp;
                        bool registrado = DireccionesLN.getInstance().ActualizarDireccion(o_direccion);
                        if (registrado)
                        {
                            Response.Write("<script>alert('Se actualizo correctamente el domicilio.')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('No se pudo actualizar el domicilio intente nuevamente.')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Los siguientes campos estan incompletos o con inconvenientes: " + txt_salida + ".')</script>");
                }
                //DireccionesLN.getInstance().EditarDireccion();
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }

        protected void dpl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenar_cbo_localidades();
        }

        protected void dpl_barrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void dpl_localidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenar_cbo_barrios();
        }

        private void llenar_cbo_barrios()        
        {

            List<Barrio> o_barrios_temp = llenar_barrios_almacenados();
            List<string> barrios_temp = new List<string>();

            barrios_temp = o_barrios_temp.Where(x => (x.barrio_localidad.loc_provincia.provincia_nombre == dpl_provincia.SelectedValue) && (x.barrio_localidad.loc_nombre == dpl_localidad.SelectedValue)).Select(x => x.barrio_nombre).Distinct().OrderBy(o => o).ToList();
            barrios_temp.Insert(0, "Seleccione un barrio.");
            dpl_barrio.DataSource = null;
            dpl_barrio.DataSource = barrios_temp;
            dpl_barrio.DataBind();
        }

        private void llenar_cbo_localidades()
        {
            List<Barrio> o_barrios_temp = llenar_barrios_almacenados();
            List<string> localidades_temp = new List<string>();
            string provincia = dpl_provincia.SelectedValue;
            localidades_temp = o_barrios_temp.Where(x => x.barrio_localidad.loc_provincia.provincia_nombre == provincia).Select(x => x.barrio_localidad.loc_nombre).Distinct().OrderBy(o => o).ToList();
            localidades_temp.Insert(0, "Seleccione una localidad.");
            dpl_localidad.DataSource = null;
            dpl_localidad.DataSource = localidades_temp;
            dpl_localidad.DataBind();            
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 dir_id = ID_domicilio[dpl_domicilio.SelectedIndex - 1];
                if (DireccionesLN.getInstance().EliminarDireccion(dir_id))
                {
                    Response.Write("<script>alert('Se elimino correctamente el domicilio.')</script>");
                    limpiar_campos();
                }
                else
                {
                    Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                }
            }
            catch (Exception ex)
            {
                limpiar_campos();
            }

        }
    }
}