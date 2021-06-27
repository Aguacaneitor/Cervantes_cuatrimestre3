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
    public class TelefonosAD
    {
        #region "PATRON SINGLETON"
        private static TelefonosAD adTelefonos = null;
        private TelefonosAD() { }
        public static TelefonosAD getInstance()
        {
            if (adTelefonos == null)
            {
                adTelefonos = new TelefonosAD();
            }
            return adTelefonos;
        }
        #endregion

        public List<Telefono> ObtenerListaTelefonos(String usuario)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Telefono> telefonos = new List<Telefono>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaTelefonosFiltrados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", usuario);
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    Telefono o_telefono_temp = new Telefono();
                    o_telefono_temp.tel_id = rd["tel_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["tel_id"]);
                    o_telefono_temp.tel_tipo = rd["tel_tipo"] == DBNull.Value ? "" : rd["tel_tipo"].ToString();
                    o_telefono_temp.tel_nro = rd["tel_nro"] == DBNull.Value ? "" : rd["tel_nro"].ToString();
                    o_telefono_temp.tel_prioridad = rd["tel_prioridad"] == DBNull.Value ? 0 : Convert.ToInt32(rd["tel_prioridad"].ToString());
                    Usuario o_usuario_temp = new Usuario();
                    o_usuario_temp.usu_id = rd["usu_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["usu_id"]);
                    o_usuario_temp.usuario = rd["usuario"] == DBNull.Value ? "" : rd["usuario"].ToString();
                    o_telefono_temp.tel_usuario = o_usuario_temp;
                    telefonos.Add(o_telefono_temp);
                }
                telefonos = telefonos.OrderBy(x => x.tel_prioridad).ToList();
                //}
            }
            catch (Exception ex)
            {
                telefonos = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return telefonos;

        }

        public bool RegistrarTelefono(Telefono o_telefono)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                List<Telefono> telefonos_temp = ObtenerListaTelefonos(o_telefono.tel_usuario.usuario).Where(x => x.tel_prioridad >= o_telefono.tel_prioridad).ToList();
                if (telefonos_temp.Count >= 1)
                {
                    foreach(Telefono telefono_temp in telefonos_temp)
                    {
                        telefono_temp.tel_prioridad = telefono_temp.tel_prioridad + 1;
                        EditarTelefono(telefono_temp);
                    }                    
                }
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spRegistroTelefono", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmTel_Tipo", o_telefono.tel_tipo);
                cmd.Parameters.AddWithValue("@prmTel_nro", o_telefono.tel_nro);
                cmd.Parameters.AddWithValue("@prmTel_prioridad", o_telefono.tel_prioridad);
                cmd.Parameters.AddWithValue("@prmUser", o_telefono.tel_usuario.usuario == null ? "" : o_telefono.tel_usuario.usuario);
                cmd.Parameters.AddWithValue("@prmUserID", o_telefono.tel_usuario.usu_id == null ? -1 : o_telefono.tel_usuario.usu_id);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Consulta brindo devolucion.");   
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

        public bool EditarVerificandoTelegono(Telefono o_telefono)
        {
            try
            {
                List<Telefono> telefonos_temp = ObtenerListaTelefonos(o_telefono.tel_usuario.usuario).Where(x => (x.tel_prioridad >= o_telefono.tel_prioridad) && (x.tel_id != o_telefono.tel_id)).ToList();
                if (telefonos_temp.Count >= 1)
                {
                    foreach (Telefono telefono_temp in telefonos_temp)
                    {
                        telefono_temp.tel_prioridad = telefono_temp.tel_prioridad + 1;
                        EditarTelefono(telefono_temp);
                    }
                }
                EditarTelefono(o_telefono);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool EditarTelefono(Telefono o_telefono)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spEditarTelefono", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmTel_Tipo", o_telefono.tel_tipo);
                cmd.Parameters.AddWithValue("@prmTel_nro", o_telefono.tel_nro);
                cmd.Parameters.AddWithValue("@prmTel_prioridad", o_telefono.tel_prioridad);
                cmd.Parameters.AddWithValue("@prmTelID", o_telefono.tel_id);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Consulta brindo devolucion.");
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

        public bool EliminarTelefono(Telefono o_telefono)
        {
            try
            {                
                if (SP_EliminarTelefono(o_telefono))
                {
                    List<Telefono> telefonos_temp = ObtenerListaTelefonos(o_telefono.tel_usuario.usuario).Where(x => (x.tel_prioridad >= o_telefono.tel_prioridad)).ToList();
                    if (telefonos_temp.Count >= 1)
                    {
                        foreach (Telefono telefono_temp in telefonos_temp)
                        {
                            telefono_temp.tel_prioridad = telefono_temp.tel_prioridad - 1;
                            EditarTelefono(telefono_temp);
                        }
                    }
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool SP_EliminarTelefono(Telefono o_telefono)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spEliminarTelefono", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmTelID", o_telefono.tel_id);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine("Consulta brindo devolucion.");
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
