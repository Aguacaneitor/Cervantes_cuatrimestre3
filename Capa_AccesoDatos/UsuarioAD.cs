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

    }
}
