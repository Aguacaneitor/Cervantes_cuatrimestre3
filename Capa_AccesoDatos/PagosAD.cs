using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Capa_AccesoDatos
{
    public class PagosAD
    {
        #region "PATRON SINGLETON"
        private static PagosAD aDPagos = null;
        private PagosAD() { }
        public static PagosAD getInstance()
        {
            if (aDPagos == null)
            {
                aDPagos = new PagosAD();
            }
            return aDPagos;
        }
        #endregion

        public bool GenerarCuponesCobro()
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spNominaPagos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    String usuario = rd["usuario"].ToString();
                    DateTime fecha_alta = rd["usu_fecalta"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["usu_fecalta"].ToString());
                    int meses_Activo = Convert.ToInt32(rd["Meses_Activo"]);
                    int dia_Vencimiento = Convert.ToInt32(rd["Dia_Vencimiento"]);
                    int usu_id = Convert.ToInt32(rd["usu_id"]);
                    List<Comprobante> l_Comprobantes = ObtenerListaComprobantes(usu_id, "Cupon Cobro");
                    for (int i = 1; i <= meses_Activo; i++)
                    {
                        //if (l_Comprobantes.Count > 0)
                        //{
                        List<Comprobante> l_comprobante_temp = l_Comprobantes.Where(x => x.comp_fecha.Month == (fecha_alta.Month + i)).ToList();
                        if (l_comprobante_temp.Count == 0)
                        {
                            Comprobante o_comprobante_temp = new Comprobante();
                            TipoComprobante o_tipoComprobante_temp = new TipoComprobante();
                            o_tipoComprobante_temp.TC_nombre = "Cupon Cobro";
                            o_comprobante_temp.com_TipoComprobante = o_tipoComprobante_temp;
                            o_comprobante_temp.comp_fecha = fecha_alta.AddMonths(i);
                            o_comprobante_temp.comp_letra = "X";
                            o_comprobante_temp.comp_suc = 1;
                            Usuario o_usuario_temp = new Usuario();
                            o_usuario_temp.usu_id = usu_id;
                            o_comprobante_temp.com_Usuario = o_usuario_temp;
                            Producto o_producto_temp = new Producto();
                            o_producto_temp.prod_id = 1;
                            o_comprobante_temp.com_Producto = o_producto_temp;
                            RegistrarCupon(o_comprobante_temp);
                        }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }
        //Registro de comprobante
        public bool RegistrarCupon(Comprobante o_comprobante)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spRegistroComprobante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmTCNombre", o_comprobante.com_TipoComprobante.TC_nombre);
                cmd.Parameters.AddWithValue("@prmFechaComprobante", o_comprobante.comp_fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@prmLetra", o_comprobante.comp_letra);
                cmd.Parameters.AddWithValue("@prmSuc", o_comprobante.comp_suc);
                cmd.Parameters.AddWithValue("@prmUserID", o_comprobante.com_Usuario.usu_id);
                cmd.Parameters.AddWithValue("@prmProductoID", o_comprobante.com_Producto.prod_id);

                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.Write(rd);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }
        //spListaComprobantesFiltrados        
       public List<Comprobante> ObtenerListaComprobantesPagos(int usuario_ID, String tipo_Comprobante)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Comprobante> o_comprobante = new List<Comprobante>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaComprobantesFiltradosPagos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUserID", usuario_ID);
                cmd.Parameters.AddWithValue("@prmTC", tipo_Comprobante);
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    Comprobante o_comprobante_temp;
                    o_comprobante_temp = new Comprobante();
                    o_comprobante_temp.comp_id = rd["comp_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["comp_id"]);
                    o_comprobante_temp.comp_fecha = rd["comp_fecha"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["comp_fecha"].ToString());
                    o_comprobante_temp.comp_letra = rd["comp_letra"] == DBNull.Value ? "" : rd["comp_letra"].ToString();
                    o_comprobante_temp.comp_suc = rd["comp_suc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["comp_suc"]);
                    o_comprobante_temp.comp_neto = rd["comp_neto"] == DBNull.Value ? (float)0 : (float)Convert.ToDouble(rd["comp_neto"]);
                    o_comprobante_temp.comp_iva = rd["comp_iva"] == DBNull.Value ? (float)0 : (float)Convert.ToDouble(rd["comp_iva"]);
                    o_comprobante_temp.comp_total = rd["comp_total"] == DBNull.Value ? (float)0 : (float)Convert.ToDouble(rd["comp_total"]);
                    Usuario o_usuario_temp = new Usuario();
                    o_usuario_temp.usu_id = rd["usu_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["usu_id"]);
                    o_comprobante_temp.com_Usuario = o_usuario_temp;
                    TipoComprobante o_tipoComprobanteTemp = new TipoComprobante();
                    o_tipoComprobanteTemp.TC_id = rd["TC_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TC_id"]);
                    o_tipoComprobanteTemp.TC_nombre = rd["TC_nombre"] == DBNull.Value ? "" : rd["TC_nombre"].ToString();
                    o_tipoComprobanteTemp.TC_signo = rd["TC_signo"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TC_signo"]);
                    o_comprobante_temp.com_TipoComprobante = o_tipoComprobanteTemp;
                    o_comprobante_temp.comp_id = rd["prod_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["prod_id"]);
                    o_comprobante_temp.com_Pagado = rd["Pagado"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Pagado"]);
                    o_comprobante_temp.comp_total_formateado = rd["comp_total_formateado"] == DBNull.Value ? "" : rd["comp_total_formateado"].ToString();
                    o_comprobante_temp.comp_fecha_formateado = rd["comp_fecha_formateado"] == DBNull.Value ? "" : rd["comp_fecha_formateado"].ToString();                    
                    o_comprobante.Add(o_comprobante_temp);
                }
            }
            catch (Exception ex)
            {
                o_comprobante = null;
            }
            finally
            {
                conexion.Close();
            }
            return o_comprobante;
        }

        public List<Comprobante> ObtenerListaComprobantes(int usuario_ID, String tipo_Comprobante)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Comprobante> o_comprobante = new List<Comprobante>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaComprobantesFiltrados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUserID", usuario_ID);
                cmd.Parameters.AddWithValue("@prmTC", tipo_Comprobante);
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    Comprobante o_comprobante_temp;
                    o_comprobante_temp = new Comprobante();
                    o_comprobante_temp.comp_id = rd["comp_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["comp_id"]);
                    o_comprobante_temp.comp_fecha = rd["comp_fecha"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["comp_fecha"].ToString());
                    o_comprobante_temp.comp_letra = rd["comp_letra"] == DBNull.Value ? "" : rd["comp_letra"].ToString();
                    o_comprobante_temp.comp_suc = rd["comp_suc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["comp_suc"]);
                    o_comprobante_temp.comp_neto = rd["comp_neto"] == DBNull.Value ? (float) 0 : (float) Convert.ToDouble(rd["comp_neto"]);
                    o_comprobante_temp.comp_iva = rd["comp_iva"] == DBNull.Value ? (float)0 : (float)Convert.ToDouble(rd["comp_iva"]);
                    o_comprobante_temp.comp_total = rd["comp_total"] == DBNull.Value ? (float)0 : (float)Convert.ToDouble(rd["comp_total"]);
                    Usuario o_usuario_temp = new Usuario();
                    o_usuario_temp.usu_id = rd["usu_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["usu_id"]);
                    o_comprobante_temp.com_Usuario = o_usuario_temp;
                    TipoComprobante o_tipoComprobanteTemp = new TipoComprobante();
                    o_tipoComprobanteTemp.TC_id = rd["TC_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TC_id"]);
                    o_tipoComprobanteTemp.TC_nombre = rd["TC_nombre"] == DBNull.Value ? "" : rd["TC_nombre"].ToString();
                    o_tipoComprobanteTemp.TC_signo = rd["TC_signo"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TC_signo"]);
                    o_comprobante_temp.com_TipoComprobante = o_tipoComprobanteTemp;
                    o_comprobante_temp.comp_id = rd["prod_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["prod_id"]);
                    o_comprobante.Add(o_comprobante_temp);
                }
            }
            catch (Exception ex)
            {
                o_comprobante = null;
            }
            finally
            {
                conexion.Close();
            }
            return o_comprobante;
        }

    }
}
