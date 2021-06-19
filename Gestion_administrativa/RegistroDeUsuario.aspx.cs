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
            /*if (!IsPostBack)
            {
                cal_fec_nac.Visible = false;
            }*/
            if (!IsPostBack)
            {
                fill_Combobox();
            } 

        }

        private void fill_Combobox()
        {
            var dia = new List<int>();
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
            drp_agno.DataBind();
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
            int dia = Convert.ToInt32(drp_dia.SelectedValue);
            int mes = Convert.ToInt32(drp_mes.SelectedValue);
            int agno = Convert.ToInt32(drp_agno.SelectedValue);
            string dia_f = (dia < 10) ? "0" + dia : "" + dia;
            string mes_f = (mes < 10) ? "0" + mes : "" + mes;
            string txt_salida = "";
            try
            {
                string fecha = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f).ToString("yyyy-MM-dd");
                //txt_fecha_nacimiento.Text = fecha;
            }
            catch (Exception ex)
            {
                txt_salida += "Fecha ";
            }
           
            if (!(Regex.Match(txt_usu_nomdoc.Text, @"[0-9]*").Value == txt_usu_nomdoc.Text) | "" == txt_usu_nomdoc.Text)
            {
                txt_salida += "Documento ";
            }
            if (!(Regex.Match(txt_usu_Nom.Text, @"[a-zA-Z]*\s*[a-zA-Z]*").Value == txt_usu_Nom.Text) | "" == txt_usu_Nom.Text)
            {
                txt_salida += "Nombre ";
            }
            if (!(Regex.Match(txt_usu_Ape.Text, @"[a-zA-Z]*\s*[a-zA-Z]*").Value == txt_usu_Ape.Text) | "" == txt_usu_Ape.Text)
            {
                txt_salida += "Apellido ";
            }
            if (!(Regex.Match(txt_usuario.Text, @"\S*").Value == txt_usuario.Text) | "" == txt_usuario.Text)
            {
                txt_salida += "Usuario ";
            }
            if (!(Regex.Match(txt_usu_pass.Text, @"\S*").Value == txt_usu_pass.Text) | "" == txt_usu_pass.Text)
            {
                txt_salida += "Contraseña ";
            }
            if (lb_rol.SelectedValue == "Seleccione Rol")
            {
                txt_salida += "Rol ";
            }
            if (drp_tipodoc.SelectedValue == "Tipo")
            {
                txt_salida += "Tipo de Documento ";
            }
            if (!mailValido(txt_usu_email.Text))
            {
                txt_salida += "Email ";
            }
            if (txt_salida == "")
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
                    else
                    {
                        //Response.Write("<script>alert('Debe terminar el registro o el usuario quedara inactivo.')</script>");
                        /* Usuario o_usuario = new Usuario();
                         o_usuario.usuario = txt_usuario.Text;
                         o_usuario.usu_pass = txt_usu_pass.Text;
                         o_usuario.usu_nomdoc = txt_usu_nomdoc.Text;
                         o_usuario.usu_Ape = txt_usu_Ape.Text;
                         o_usuario.usu_Nom = txt_usu_Nom.Text;
                         o_usuario.o_rol.rol_descripcion = drp_tipodoc.SelectedValue;
                         o_usuario.usu_email = txt_usu_email.Text;
                         o_usuario.usu_tipodoc = drp_tipodoc.SelectedValue;
                         o_usuario.fec_nac = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f);
                         registrar_usuario(o_usuario);*/
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
                        Response.Redirect("RegistroDeDomicilio.aspx"+ parametros + "&usuario_r=" + txt_usuario.Text+"&nombre_r="+ txt_usu_Nom.Text);
                    }
                    if (txt_salida != "")
                    {
                        Response.Write("<script>alert('" + txt_salida + "')</script>");
                    }                        
                }
            } else
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

        }
    } 
}