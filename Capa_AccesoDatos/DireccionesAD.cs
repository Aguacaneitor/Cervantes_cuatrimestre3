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
    public class DireccionesAD
    {
        #region "PATRON SINGLETON"
        private static DireccionesAD adDireccionesLista = null;
        private DireccionesAD() { }
        public static DireccionesAD getInstance()
        {
            if (adDireccionesLista == null)
            {
                adDireccionesLista = new DireccionesAD();
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
                    o_barrio_temp.barrio_localidad = o_localidad_temp;
                    o_direccion_temp.dir_barrio = o_barrio_temp;
                    direcciones.Add(o_direccion_temp);
                }
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

        public bool RegistrarDireccion(Direccion o_direccion)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spRegistroDomicilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", o_direccion.dir_usuario.usuario);
                cmd.Parameters.AddWithValue("@prmCalle", o_direccion.dir_calle);
                cmd.Parameters.AddWithValue("@prmAltura", o_direccion.dir_altura);
                cmd.Parameters.AddWithValue("@prmPiso", o_direccion.dir_piso);
                cmd.Parameters.AddWithValue("@prmDpto", o_direccion.dir_dpto);
                cmd.Parameters.AddWithValue("@prmTorre", o_direccion.dir_torre);
                cmd.Parameters.AddWithValue("@prmManzana", o_direccion.dir_manzana);
                cmd.Parameters.AddWithValue("@prmBarrio_id", o_direccion.dir_barrio.barrio_id);
                cmd.Parameters.AddWithValue("@prmUsu_CP", o_direccion.usu_CP);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Insert devolvio record.");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;                
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool ActualizarDireccion(Direccion o_direccion)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spActualizarDomicilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmDir_ID", o_direccion.dir_id);
                cmd.Parameters.AddWithValue("@prmUser", o_direccion.dir_usuario.usuario);
                cmd.Parameters.AddWithValue("@prmCalle", o_direccion.dir_calle);
                cmd.Parameters.AddWithValue("@prmAltura", o_direccion.dir_altura);
                cmd.Parameters.AddWithValue("@prmPiso", o_direccion.dir_piso);
                cmd.Parameters.AddWithValue("@prmDpto", o_direccion.dir_dpto);
                cmd.Parameters.AddWithValue("@prmTorre", o_direccion.dir_torre);
                cmd.Parameters.AddWithValue("@prmManzana", o_direccion.dir_manzana);
                cmd.Parameters.AddWithValue("@prmBarrio_id", o_direccion.dir_barrio.barrio_id);
                cmd.Parameters.AddWithValue("@prmUsu_CP", o_direccion.usu_CP);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Insert devolvio record.");
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

        public bool EliminarDireccion(int dir_id)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spEliminarDomicilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmDir_ID", dir_id);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Insert devolvio record.");
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
    }
}
