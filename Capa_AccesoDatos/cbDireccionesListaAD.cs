using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidades;

namespace Capa_AccesoDatos
{
    public class cbDireccionesListaAD
    {
        #region "PATRON SINGLETON"
        private static cbDireccionesListaAD adProvinciasLista = null;
        private cbDireccionesListaAD() { }
        public static cbDireccionesListaAD getInstance()
        {
            if (adProvinciasLista == null)
            {
                adProvinciasLista = new cbDireccionesListaAD();
            }
            return adProvinciasLista;
        }
        #endregion

        public List<Barrio> ObtenerListaBarrios()
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Barrio> barrios = new List<Barrio>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaBarrios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Barrio o_barrio_temp = new Barrio();
                    o_barrio_temp.barrio_id = rd["barrio_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["barrio_id"]);
                    o_barrio_temp.barrio_nombre = rd["barrio_nombre"] == DBNull.Value ? "" : rd["barrio_nombre"].ToString();
                    Provincia o_provincia_temp = new Provincia();
                    o_provincia_temp.provinc_id = rd["provinc_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["provinc_id"]);
                    o_provincia_temp.provincia_nombre = rd["provincia_nombre"] == DBNull.Value ? "" : rd["provincia_nombre"].ToString();
                    Localidad o_localidad_temp = new Localidad();
                    o_localidad_temp.loc_id = rd["loc_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["loc_id"]);
                    o_localidad_temp.loc_nombre = rd["loc_nombre"] == DBNull.Value ? "" : rd["loc_nombre"].ToString();
                    o_localidad_temp.loc_provincia = o_provincia_temp;
                    o_barrio_temp.barrio_localidad = o_localidad_temp;
                    barrios.Add(o_barrio_temp);
                }
            }
            catch (Exception ex)
            {
                barrios = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return barrios;

        }
    }
}