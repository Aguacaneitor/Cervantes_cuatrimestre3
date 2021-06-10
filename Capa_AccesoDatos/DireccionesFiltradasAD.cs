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
    public class DireccionesFiltradasAD
    {
        #region "PATRON SINGLETON"
        private static DireccionesFiltradasAD adDireccionesLista = null;
        private DireccionesFiltradasAD() { }
        public static DireccionesFiltradasAD getInstance()
        {
            if (adDireccionesLista == null)
            {
                adDireccionesLista = new DireccionesFiltradasAD();
            }
            return adDireccionesLista;
        }
        #endregion

        public List<Direccion> ObtenerListaDirecciones(String usuario)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Direccion> direcciones = new List<Direccion>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaDireccionesFiltradas", conexion);                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", usuario);
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    Direccion o_direccion_temp = new Direccion();
                    o_direccion_temp.dir_id = rd["dir_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["dir_id"]);
                    o_direccion_temp.dir_calle = rd["dir_calle"] == DBNull.Value ? "" : rd["dir_calle"].ToString();
                    o_direccion_temp.dir_altura = rd["dir_altura"] == DBNull.Value ? 0 : Convert.ToInt32(rd["dir_altura"]);
                    o_direccion_temp.dir_piso = rd["dir_piso"] == DBNull.Value ? "" : rd["dir_piso"].ToString();
                    o_direccion_temp.dir_dpto = rd["dir_dpto"] == DBNull.Value ? "" : rd["dir_dpto"].ToString();
                    o_direccion_temp.dir_torre = rd["dir_torre"] == DBNull.Value ? "" : rd["dir_torre"].ToString();
                    o_direccion_temp.dir_manzana = rd["dir_manzana"] == DBNull.Value ? "" : rd["dir_manzana"].ToString();
                    o_direccion_temp.usu_CP = rd["usu_CP"] == DBNull.Value ? "" : rd["usu_CP"].ToString();
                    //barrio
                    Barrio o_barrio_temp = new Barrio();
                    o_barrio_temp.barrio_id = rd["barrio_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["barrio_id"]);
                    o_barrio_temp.barrio_nombre = rd["barrio_nombre"] == DBNull.Value ? "" : rd["barrio_nombre"].ToString();
                    o_direccion_temp.dir_barrio = o_barrio_temp;
                    //localidad
                    Localidad o_localidad_temp = new Localidad();
                    o_localidad_temp.loc_id = rd["loc_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["loc_id"]);
                    o_localidad_temp.loc_nombre = rd["loc_nombre"] == DBNull.Value ? "" : rd["loc_nombre"].ToString();
                    //Provincia - va dentro de localidad
                    Provincia o_provincia_temp = new Provincia();
                    o_provincia_temp.provinc_id = rd["provinc_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["provinc_id"]);                    
                    o_provincia_temp.provincia_nombre = rd["provincia_nombre"] == DBNull.Value ? "" : rd["provincia_nombre"].ToString();
                    o_localidad_temp.loc_provincia = o_provincia_temp;

                    o_direccion_temp.dir_localidad = o_localidad_temp;
                    direcciones.Add(o_direccion_temp);
                }
                //}
            }
            catch (Exception ex)
            {
                direcciones = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return direcciones;

        }
    }
}
