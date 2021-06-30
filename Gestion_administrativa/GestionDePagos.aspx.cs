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
    public partial class GestionDePagos : System.Web.UI.Page
    {
        private static List<Comprobante> comprobantes_almacenados = new List<Comprobante>();
        private static List<Usuario> usuarios_almacenados = new List<Usuario>();

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios_almacenados = usuariosListaLN.getInstance().ObtenerListaUsuarios();
        }

        protected void btn_generarCuponesPago_Click(object sender, EventArgs e)
        {
            PagosLN.getInstance().GenerarCuponesCobro();
        }

        protected void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario_buscado.Text != "")
            {
                Usuario o_usuario_temp = UsuarioLN.getInstance().ObtenerUsuario(txt_usuario_buscado.Text);
                txt_usuario_buscado.Text = o_usuario_temp.usuario;
                fill_gridview(o_usuario_temp.usuario);
                btn_eliminar.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('Ingrese un usuario para buscar.')</script>");
            }
        }

        private void limpiar()
        {
            btn_eliminar.Visible = false;
            btn_eliminarCuota.Visible = false;
            dpl_cuotaHistorica.Visible = false;
            gv_cupones_totales.DataSource = null;
            gv_cupones_pendientes.DataSource = null;
            gv_cupones_totales.DataBind();
            gv_cupones_pendientes.DataBind();
            txt_usuario_buscado.Text = "";
        }

        
        private void fill_gridview(String usuario)
        {
            try
            {
                gv_cupones_totales.DataSource = null;
                gv_cupones_pendientes.DataSource = null;                            
                int usu_id = usuarios_almacenados.Where(x => x.usuario.ToUpper() == usuario.ToUpper()).Select(x => x.usu_id).ToList()[0];
                comprobantes_almacenados = PagosLN.getInstance().ObtenerListaComprobantesPagos(usu_id, "Cupon Cobro");
                gv_cupones_totales.DataSource = comprobantes_almacenados;
                gv_cupones_totales.DataBind();
                gv_cupones_pendientes.DataSource = comprobantes_almacenados.Where(x => x.com_Pagado == 0);
                gv_cupones_pendientes.DataBind();
                dpl_cuotaPagar.Items.Clear();
                dpl_cuotaPagar.Items.Add("Seleccione cuota.");
                dpl_cuotaHistorica.Items.Clear();
                dpl_cuotaHistorica.Items.Add("Seleccione cuota.");
                foreach (Comprobante o_comprobante in comprobantes_almacenados)
                {
                    
                    if (o_comprobante.com_Pagado == 0) {
                        String cuota_pagar = "Fecha Cierre: " + o_comprobante.comp_fecha_formateado + ", monto: " + o_comprobante.comp_total_formateado + ", Tipo: " + o_comprobante.com_TipoComprobante.TC_nombre;
                        dpl_cuotaPagar.Items.Add(cuota_pagar);                        
                    }
                    else
                    {
                        String cuota = "Fecha Cierre: " + o_comprobante.comp_fecha_formateado + ", monto: " + o_comprobante.comp_total_formateado + ", Tipo: " + o_comprobante.com_TipoComprobante.TC_nombre;
                        dpl_cuotaHistorica.Items.Add(cuota);
                    }
                }
                if (dpl_cuotaHistorica.Items.Count > 1)
                {
                    dpl_cuotaHistorica.Visible = true;
                }
                else
                {
                    dpl_cuotaHistorica.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
            }
        }

        protected void btn_registrarPago_Click(object sender, EventArgs e)
        {
            if (dpl_cuotaPagar.SelectedValue == "Seleccione cuota.")
            {
                Response.Write("<script>alert('Seleccione cuota para continuar.')</script>");
            }
            else
            {
                try
                {
                    String fecha = Regex.Match(dpl_cuotaPagar.SelectedValue, @"[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;
                    String monto = Regex.Match(dpl_cuotaPagar.SelectedValue, @"[$][0-9]*").Value;
                    Comprobante o_comprobante = comprobantes_almacenados.Where(x => (x.comp_fecha_formateado == fecha && x.comp_total_formateado == monto)).ToList()[0];
                    o_comprobante.com_TipoComprobante.TC_nombre = "Recibo Pago";
                    if (PagosLN.getInstance().RegistrarCupon(o_comprobante))
                    {
                        Response.Write("<script>alert('Se registro el pago de la cuota seleccionada.')</script>");
                        if (txt_usuario_buscado.Text != "")
                        {
                            fill_gridview(txt_usuario_buscado.Text);
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                }                
            }
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dpl_cuotaPagar.SelectedValue == "Seleccione cuota.")
            {
                Response.Write("<script>alert('Seleccione cuota para continuar.')</script>");
            }
            else
            {
                try
                {
                    String fecha = Regex.Match(dpl_cuotaPagar.SelectedValue, @"[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;
                    String monto = Regex.Match(dpl_cuotaPagar.SelectedValue, @"[$][0-9]*").Value;
                    Comprobante o_comprobante = comprobantes_almacenados.Where(x => (x.comp_fecha_formateado == fecha && x.comp_total_formateado == monto)).ToList()[0];
                    if (PagosLN.getInstance().EliminarCupon(o_comprobante.comp_id))
                    {
                        Response.Write("<script>alert('Se elimino la cuota seleccionada.')</script>");
                        if (txt_usuario_buscado.Text != "")
                        {
                            fill_gridview(txt_usuario_buscado.Text);
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                }
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void dpl_cuotaHistorica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpl_cuotaHistorica.SelectedValue != "Seleccione cuota.")
            {
                btn_eliminarCuota.Visible = true;
            }
            else
            {
                btn_eliminarCuota.Visible = false;
            }         
        }

        protected void btn_eliminarCuota_Click(object sender, EventArgs e)
        {
            if (dpl_cuotaHistorica.SelectedValue == "Seleccione cuota.")
            {
                Response.Write("<script>alert('Seleccione cuota para continuar.')</script>");
            }
            else
            {
                try
                {
                    String fecha = Regex.Match(dpl_cuotaHistorica.SelectedValue, @"[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;
                    String monto = Regex.Match(dpl_cuotaHistorica.SelectedValue, @"[$][0-9]*").Value;
                    Comprobante o_comprobante = comprobantes_almacenados.Where(x => (x.comp_fecha_formateado == fecha && x.comp_total_formateado == monto)).ToList()[0];
                    if (PagosLN.getInstance().EliminarCupon(o_comprobante.comp_id_pago))
                    {
                        Response.Write("<script>alert('Se elimino el pago de la cuota seleccionada.')</script>");
                        if (txt_usuario_buscado.Text != "")
                        {
                            fill_gridview(txt_usuario_buscado.Text);
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
                }
            }
        }
    }  
}