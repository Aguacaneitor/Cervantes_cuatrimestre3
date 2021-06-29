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
                fill_gridview(txt_usuario_buscado.Text);
            }
            else
            {
                Response.Write("<script>alert('Ingrese un usuario para buscar.')</script>");
            }
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
                foreach (Comprobante o_comprobante in comprobantes_almacenados.Where(x => x.com_Pagado == 0))
                {
                    String cuota = "Fecha Cierre: " + o_comprobante.comp_fecha_formateado + ", monto: " + o_comprobante.comp_total_formateado;
                    dpl_cuotaPagar.Items.Add(cuota);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ocurrio un error intente nuevamente.')</script>");
            }
        }
    }  
}