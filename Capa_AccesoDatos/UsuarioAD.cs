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
    public class UsuarioAD
    {
        #region "PATRON SINGLETON"
        private static UsuarioAD adUsuario = null;
        private UsuarioAD() { }
        public static UsuarioAD getInstance()
        {
            if (adUsuario == null)
            {
                adUsuario = new UsuarioAD();
            }
            return adUsuario;
        }
        #endregion

        public Usuario AccesoSistema(String usuario, String contrasena)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            Usuario o_usuario = null;
            SqlDataReader rd = null;
            
            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spAccesoSistema", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", usuario);
                cmd.Parameters.AddWithValue("@prmPass", contrasena);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    o_usuario = new Usuario();
                    o_usuario.usu_id = Convert.ToInt32(rd["usu_id"].ToString());
                    o_usuario.usuario = rd["usuario"].ToString();
                    o_usuario.usu_pass = rd["usu_pass"].ToString();
                    o_usuario.usu_Ape = rd["usu_Ape"].ToString();
                    o_usuario.usu_Nom = rd["usu_Nom"].ToString();
                    o_usuario.estado_usuario = Convert.ToBoolean(rd["estado_usuario"].ToString());
                    Rol o_rolTemp = new Rol();
                    o_rolTemp.rol_id = Convert.ToInt32(rd["rol_id"].ToString());
                    o_rolTemp.rol_descripcion = rd["rol_descripcion"].ToString();                     
                    o_usuario.o_rol = o_rolTemp;                    
                }
            }
            catch (Exception ex)
            {
                o_usuario = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }           
            return o_usuario;

        }

        public void RegistrarUsuarioAD(Usuario o_usuario)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spRegistroUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", o_usuario.usuario);
                cmd.Parameters.AddWithValue("@prmPass", o_usuario.usu_pass);
                cmd.Parameters.AddWithValue("@prmNombre", o_usuario.usu_Nom);
                cmd.Parameters.AddWithValue("@prmApellido", o_usuario.usu_Ape);
                cmd.Parameters.AddWithValue("@prmFechaNacimiento", o_usuario.fec_nac.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@prmTipoDocumento", o_usuario.usu_tipodoc);
                cmd.Parameters.AddWithValue("@prmDocumento", o_usuario.usu_nomdoc);
                cmd.Parameters.AddWithValue("@prmRol", o_usuario.o_rol.rol_descripcion);
                cmd.Parameters.AddWithValue("@prmEmail", o_usuario.usu_email);
                cmd.Parameters.AddWithValue("@prmUserAlta", o_usuario.usu_alta);
                string fecha_alta = o_usuario.usu_fecalta.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.AddWithValue("@prmFechaAlta", o_usuario.usu_fecalta.ToString("yyyy-MM-dd HH:mm:ss"));
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.Write(rd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public Usuario ObtenerUsuario(String usuario)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            Usuario o_usuario = null;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spObtenerUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", usuario);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    o_usuario = new Usuario();
                    o_usuario.usu_id = Convert.ToInt32(rd["usu_id"].ToString());
                    o_usuario.usuario = rd["usuario"].ToString();
                    o_usuario.usu_pass = rd["usu_pass"].ToString();
                    o_usuario.usu_Ape = rd["usu_Ape"].ToString();
                    o_usuario.usu_Nom = rd["usu_Nom"].ToString();
                    o_usuario.fec_nac = DateTime.Parse(rd["fec_nac"].ToString());
                    o_usuario.usu_tipodoc = rd["usu_tipodoc"].ToString();
                    o_usuario.usu_nomdoc = rd["usu_nomdoc"].ToString();
                    o_usuario.usu_email = rd["usu_email"].ToString();
                    o_usuario.estado_usuario = Convert.ToBoolean(rd["estado_usuario"].ToString());
                    Rol o_rolTemp = new Rol();
                    o_rolTemp.rol_id = Convert.ToInt32(rd["rol_id"].ToString());
                    o_rolTemp.rol_descripcion = rd["rol_descripcion"].ToString();
                    o_usuario.o_rol = o_rolTemp;
                }
            }
            catch (Exception ex)
            {
                o_usuario = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return o_usuario;

        }

        public bool CambiarEstadoUsuario(String editor, String usuario, int cambio)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            bool cambiado = true;
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spCambiarEstadoUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", usuario);
                cmd.Parameters.AddWithValue("@prmCambio", cambio);
                cmd.Parameters.AddWithValue("@prmEditor", editor);
                conexion.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.Write("Query brindo feedback");
                }
            }
            catch (Exception ex)
            {
                cambiado = false;
            }
            finally
            {
                conexion.Close();
            }
            return cambiado;

        }

        public bool EditarUsuario(Usuario o_usuario)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spEditarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmUser", o_usuario.usuario);
                cmd.Parameters.AddWithValue("@prmPass", o_usuario.usu_pass);
                cmd.Parameters.AddWithValue("@prmNombre", o_usuario.usu_Nom);
                cmd.Parameters.AddWithValue("@prmApellido", o_usuario.usu_Ape);
                cmd.Parameters.AddWithValue("@prmFechaNacimiento", o_usuario.fec_nac.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@prmRol", o_usuario.o_rol.rol_descripcion);
                cmd.Parameters.AddWithValue("@prmEmail", o_usuario.usu_email);
                cmd.Parameters.AddWithValue("@prmUserModi", o_usuario.usu_modi);
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

    }
}
