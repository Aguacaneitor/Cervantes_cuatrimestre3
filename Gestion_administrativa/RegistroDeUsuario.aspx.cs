using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidades;
using Capa_LogicaNegocio;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Gestion_administrativa
{
    public partial class RegistroDeUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill_Combobox();
            } 

        }

        private void fill_Combobox()
        {
            /*var dia = new List<int>();
             var mes = new List<int>();
             var agno = new List<int>();
             for (int i = 1; i <= 31; i++)
             {
                 dia.Add(i);
             }
             for (int i = 1; i <= 12; i++)
             {
                 mes.Add(i);
             }
             for (int i = 1900; i <= DateTime.Now.Year; i++)
             {
                 agno.Add(i);
             }

             drp_dia.DataSource = dia;
             drp_mes.DataSource = mes;
             drp_agno.DataSource = agno;
             drp_dia.DataBind();
             drp_mes.DataBind();
             drp_agno.DataBind();*/
            lb_rol.Items.Add("Seleccione Rol");
            List<string> listaRoles = RolesListaLN.getInstance().ObtenerListaRoles();
            //listaRoles.ForEach(Console.WriteLine);
            foreach (string rol in listaRoles)
            {
                lb_rol.Items.Add(rol);
            }
            drp_tipodoc.Items.Add("Tipo");
            drp_tipodoc.Items.Add("D.N.I");
            drp_tipodoc.Items.Add("L.C");
            drp_tipodoc.Items.Add("L.E");
            drp_tipodoc.Items.Add("PAS");
            drp_tipodoc.Items.Add("C.I");
        }

        protected void img_calendario_Click(object sender, ImageClickEventArgs e)
        {
            /*if (cal_fec_nac.Visible)
            {
                cal_fec_nac.Visible = false;
            } else
            {
                cal_fec_nac.Visible = true;
            }*/
        }

        protected void cal_fec_nac_SelectionChanged(object sender, EventArgs e)
        {
            /*txt_cal_fec_nac.Text = cal_fec_nac.SelectedDate.ToShortDateString();
            cal_fec_nac.Visible = false;*/
        }

        protected void btn_fecha_Click(object sender, EventArgs e)
        {
           /* int dia = Convert.ToInt32(drp_dia.SelectedValue);
            int mes = Convert.ToInt32(drp_mes.SelectedValue);
            int agno = Convert.ToInt32(drp_agno.SelectedValue);
            string dia_f = (dia < 10) ? "0" + dia : "" + dia;
            string mes_f = (mes < 10) ? "0" + mes : "" + mes;
            try
            {
                string fecha = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f).ToString("yyyy-MM-dd");
                txt_fecha_nacimiento.Text = fecha;
            } catch(Exception ex)
            {
                Response.Write("<script>alert('Fecha ingresada es invalida')</script>");
            } */        
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            string txt_salida = "";
            try
            {
                int dia = Convert.ToInt32(drp_dia.Text);
                int mes = Convert.ToInt32(drp_mes.Text);
                int agno = Convert.ToInt32(drp_agno.Text);
                string dia_f = (dia < 10) ? "0" + dia : "" + dia;
                string mes_f = (mes < 10) ? "0" + mes : "" + mes;
                
            
                string fecha = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f).ToString("yyyy-MM-dd");
                int agnos = (DateTime.Now.Year - DateTime.Parse(agno + "-" + mes_f + "-" + dia_f).Year);
                if (agnos < 18)
                {
                    txt_salida += "Fecha: Menor de edad";
                }
                //txt_fecha_nacimiento.Text = fecha;
            }
            catch (Exception ex)
            {
                txt_salida += txt_salida == "" ? "Fecha " : ", Fecha ";
            }
           
            if (!(Regex.Match(txt_usu_nomdoc.Text, @"[0-9]*").Value == txt_usu_nomdoc.Text) | "" == txt_usu_nomdoc.Text)
            {
                txt_salida += txt_salida == "" ? "Documento" : ", Documento";
            }
            if (!(Regex.Match(txt_usu_Nom.Text, @"[a-zA-Z]*\s*[a-zA-Z]*").Value == txt_usu_Nom.Text) | "" == txt_usu_Nom.Text)
            {
                txt_salida += txt_salida == "" ? "Nombre" : ", Nombre";
            }
            if (!(Regex.Match(txt_usu_Ape.Text, @"[a-zA-Z]*\s*[a-zA-Z]*").Value == txt_usu_Ape.Text) | "" == txt_usu_Ape.Text)
            {
                txt_salida += txt_salida == "" ? "Apellido" : ", Apellido";
            }
            if (!(Regex.Match(txt_usuario.Text, @"\S*").Value == txt_usuario.Text) | "" == txt_usuario.Text)
            {
                txt_salida += txt_salida == "" ? "Usuario" : ", Usuario";
            }
            if (!(Regex.Match(txt_usu_pass.Text, @"\S*").Value == txt_usu_pass.Text) | "" == txt_usu_pass.Text)
            {
                txt_salida += txt_salida == "" ? "Contraseña" : ", Contraseña";
            }
            if (lb_rol.SelectedValue == "Seleccione Rol")
            {
                txt_salida += txt_salida == "" ? "Rol" : ", Rol";
            }
            if (drp_tipodoc.SelectedValue == "Tipo")
            {
                txt_salida += txt_salida == "" ? "Tipo de Documento" : ", Tipo de Documento";
            }
            if (!mailValido(txt_usu_email.Text))
            {
                txt_salida += txt_salida == "" ? "Email " : ", Email";
            }
            if (txt_salida == "")
            {
                if (btn_registrar.Text == "REGISTRAR")
                {
                    List<Usuario> listaUsuarios = usuariosListaLN.getInstance().ObtenerListaUsuarios();
                    foreach (Usuario o_usuario_temp in listaUsuarios)
                    {
                        if (drp_tipodoc.SelectedValue.ToUpper() == o_usuario_temp.usu_tipodoc.ToUpper() & txt_usu_nomdoc.Text.ToUpper() == o_usuario_temp.usu_nomdoc.ToUpper())
                        {
                            txt_salida += "El cliente con el documento ingresado ya se encuentra registrado.";
                        }
                        else if (txt_usu_email.Text.ToUpper() == o_usuario_temp.usu_email.ToUpper())
                        {
                            txt_salida += "El email ingresado ya se encuentra registrado.";
                        }
                        else if (txt_usuario.Text.ToUpper() == o_usuario_temp.usuario.ToUpper())
                        {
                            txt_salida += "El usuario ingresado no esta disponible.";
                        }
                    }
                    if (txt_salida != "")
                    {
                        Response.Write("<script>alert('" + txt_salida + "')</script>");
                    }
                    else
                    {
                        //Response.Write("<script>alert('Debe terminar el registro o el usuario quedara inactivo.')</script>");
                        Usuario o_usuario = new Usuario();
                        Rol o_usu_rol = new Rol();
                        int dia = Convert.ToInt32(drp_dia.Text);
                        int mes = Convert.ToInt32(drp_mes.Text);
                        int agno = Convert.ToInt32(drp_agno.Text);
                        string dia_f = (dia < 10) ? "0" + dia : "" + dia;
                        string mes_f = (mes < 10) ? "0" + mes : "" + mes;
                        o_usuario.usuario = txt_usuario.Text;
                        o_usuario.usu_pass = txt_usu_pass.Text;
                        o_usuario.usu_nomdoc = txt_usu_nomdoc.Text;
                        o_usuario.usu_Ape = txt_usu_Ape.Text;
                        o_usuario.usu_Nom = txt_usu_Nom.Text;
                        o_usu_rol.rol_descripcion = lb_rol.SelectedValue;
                        o_usuario.o_rol = o_usu_rol;
                        o_usuario.usu_email = txt_usu_email.Text;
                        o_usuario.usu_tipodoc = drp_tipodoc.SelectedValue;
                        o_usuario.fec_nac = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f);
                        o_usuario.usu_alta = Request.QueryString["editor"] != null ? Request.QueryString["editor"] : "default";
                        o_usuario.usu_fecalta = DateTime.Now;
                        registrar_usuario(o_usuario);
                        string parametros;
                        if (Request.QueryString["nombre"] != null)
                        {
                            //usuario_conectado = Request.QueryString["usuario"];
                            parametros = "?nombre=" + Request.QueryString["nombre"] + "&perfil=" + Request.QueryString["perfil"] + "&editor=" + Request.QueryString["usuario"];
                        }
                        else
                        {
                            parametros = "Usuario";
                        }
                        Response.Write("<script>alert('Se registro exitosamente el usuario, finalice la carga de su domicilio y contacto para que quede activo el mismo.')</script>");
                        Response.Redirect("RegistroDeDomicilio.aspx" + parametros + "&usuario_r=" + txt_usuario.Text + "&nombre_r=" + txt_usu_Nom.Text);
                    }
                } else
                {
                    if (btn_eliminar.Text == "DESACTIVAR")
                    {
                        Usuario o_usuario = new Usuario();
                        Rol o_usu_rol = new Rol();
                        int dia = Convert.ToInt32(drp_dia.Text);
                        int mes = Convert.ToInt32(drp_mes.Text);
                        int agno = Convert.ToInt32(drp_agno.Text);
                        string dia_f = (dia < 10) ? "0" + dia : "" + dia;
                        string mes_f = (mes < 10) ? "0" + mes : "" + mes;
                        o_usuario.usuario = txt_usuario.Text;
                        o_usuario.usu_pass = txt_usu_pass.Text;
                        o_usuario.usu_nomdoc = txt_usu_nomdoc.Text;
                        o_usuario.usu_Ape = txt_usu_Ape.Text;
                        o_usuario.usu_Nom = txt_usu_Nom.Text;
                        o_usu_rol.rol_descripcion = lb_rol.SelectedValue;
                        o_usuario.o_rol = o_usu_rol;
                        o_usuario.usu_email = txt_usu_email.Text;
                        o_usuario.usu_tipodoc = drp_tipodoc.SelectedValue;
                        o_usuario.fec_nac = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f);
                        o_usuario.usu_modi = Request.QueryString["editor"] != null ? Request.QueryString["editor"] : "default";
                        if (UsuarioLN.getInstance().EditarUsuario(o_usuario))
                        {
                            Response.Write("<script>alert('Usuario modificado correctamente.')</script>");
                            limpiar();
                        }
                        else
                        {
                            Response.Write("<script>alert('ha ocurrido un error intente nuevamente.')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Debe estar activo el usuario para poder editar.')</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Campos erroneamente cargados: "+ txt_salida + "')</script>");
            }
            
        }

        private void registrar_usuario(Usuario o_usuario)
        {
            UsuarioLN.getInstance().RegistroUsuario(o_usuario);
        }

        public bool mailValido(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            btn_registrar.Text = "REGISTRAR";
            txt_usuario_buscado.Text = "";
            txt_usu_nomdoc.Text = "";
            drp_tipodoc.SelectedValue = "Tipo";
            txt_usu_Nom.Text = "";
            txt_usu_Ape.Text = "";
            drp_dia.Text = "";
            drp_mes.Text = "";
            drp_agno.Text = "";
            txt_usu_email.Text = "";
            txt_usuario.Text = "";
            txt_usu_pass.Text = "";
            lb_rol.SelectedValue = "Seleccione Rol";
            btn_eliminar.Text = "DESACTIVAR";
            btn_eliminar.Visible = false;
            txt_usu_nomdoc.ReadOnly = false;
            txt_usuario.ReadOnly = false;
            drp_tipodoc.Enabled = true;

        }

        protected void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario_buscado.Text != "")
            {
                Usuario o_usuario = UsuarioLN.getInstance().ObtenerUsuario(txt_usuario_buscado.Text);
                if (o_usuario != null)
                {                    
                    if (!o_usuario.estado_usuario)
                    {
                        Response.Write("<script>alert('Usuario inactivo, activelo antes de editar.')</script>");
                        btn_eliminar.Text = "ACTIVAR";
                    } else
                    {
                        btn_eliminar.Text = "DESACTIVAR";
                    }
                    btn_registrar.Text = "  EDITAR ";
                    txt_usuario.Text = o_usuario.usuario;
                    txt_usu_Nom.Text = o_usuario.usu_Nom;
                    txt_usu_Ape.Text = o_usuario.usu_Ape;
                    drp_tipodoc.SelectedValue = o_usuario.usu_tipodoc;
                    txt_usu_nomdoc.Text = ""+o_usuario.usu_nomdoc;
                    txt_usu_email.Text = o_usuario.usu_email;
                    txt_usu_pass.Text = o_usuario.usu_pass;
                    //txt_usu_email.Text = o_usuario.usu_email;
                    lb_rol.SelectedValue = o_usuario.o_rol.rol_descripcion;
                    drp_dia.Text = ""+o_usuario.fec_nac.Day;
                    drp_mes.Text = "" + o_usuario.fec_nac.Month;
                    drp_agno.Text = "" + o_usuario.fec_nac.Year;
                    btn_eliminar.Visible = true;
                    txt_usu_nomdoc.ReadOnly = true;
                    txt_usuario.ReadOnly = true;
                    drp_tipodoc.Enabled = false;

                } else
                {
                    Response.Write("<script>alert('Usuario no encontrado.')</script>");
                }
                
            }
            
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (btn_eliminar.Text == "ACTIVAR")
            {
                //Activa
                String editor = Request.QueryString["editor"] != null ? Request.QueryString["editor"] : "default";
                if(UsuarioLN.getInstance().CambiarEstadoUsuario(editor, txt_usuario.Text, 1))
                {
                    Response.Write("<script>alert('Usuario activado.')</script>");
                    btn_eliminar.Text = "DESACTIVAR";
                } else
                {
                    Response.Write("<script>alert('Error en la activación intente nuevamente.')</script>");
                }
            }
            else
            {
                //desactiva
                String editor = Request.QueryString["editor"] != null ? Request.QueryString["editor"] : "default";                
                if (UsuarioLN.getInstance().CambiarEstadoUsuario(editor, txt_usuario.Text, 0))
                {
                    Response.Write("<script>alert('Usuario desactivado.')</script>");
                    btn_eliminar.Text = "ACTIVAR";
                }
                else
                {
                    Response.Write("<script>alert('Error en la desactivación intente nuevamente.')</script>");
                }
            }
        }
    } 
}