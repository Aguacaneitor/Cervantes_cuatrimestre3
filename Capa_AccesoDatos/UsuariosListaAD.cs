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
    public class UsuariosListaAD
    {
        #region "PATRON SINGLETON"
        private static UsuariosListaAD adUsuariosLista = null;
        private UsuariosListaAD() { }
        public static UsuariosListaAD getInstance()
        {
            if (adUsuariosLista == null)
            {
                adUsuariosLista = new UsuariosListaAD();
            }
            return adUsuariosLista;
        }
        #endregion

        public List<Usuario> ObtenerListaUsuarios()
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<Usuario> o_usuario = new List<Usuario>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    Usuario o_usuario_temp;
                    o_usuario_temp = new Usuario();
                    o_usuario_temp.usu_id = rd["usu_id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["usu_id"]);
                    o_usuario_temp.usuario = rd["usuario"] == DBNull.Value ? "" : rd["usuario"].ToString();
                    o_usuario_temp.usu_Ape = rd["usu_Ape"] == DBNull.Value ? "" : rd["usu_Ape"].ToString();
                    o_usuario_temp.usu_Nom = rd["usu_Nom"] == DBNull.Value ? "" : rd["usu_Nom"].ToString();
                    o_usuario_temp.fec_nac = rd["fec_nac"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["fec_nac"].ToString());
                    o_usuario_temp.usu_tipodoc = rd["usu_tipodoc"] == DBNull.Value ? "" : rd["usu_tipodoc"].ToString();
                    o_usuario_temp.usu_nomdoc = rd["usu_nomdoc"] == DBNull.Value ? "" : rd["usu_nomdoc"].ToString();
                    o_usuario_temp.usu_email = rd["usu_email"] == DBNull.Value ? "" : rd["usu_email"].ToString();
                    o_usuario_temp.usu_alta = rd["usu_alta"] == DBNull.Value ? "" : rd["usu_alta"].ToString();
                    o_usuario_temp.usu_fecalta = rd["usu_fecalta"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["usu_fecalta"].ToString());
                    o_usuario_temp.usu_modi = rd["usu_modi"] == DBNull.Value ? "" : rd["usu_modi"].ToString();
                    o_usuario_temp.usu_fecmodi = rd["usu_fecmodi"] == DBNull.Value ? default(DateTime) : DateTime.Parse(rd["usu_fecmodi"].ToString());
                    o_usuario_temp.estado_usuario = rd["estado_usuario"] == DBNull.Value ? false : (bool)rd["estado_usuario"];

                    Rol o_rolTemp = new Rol();
                    o_rolTemp.rol_id = Convert.ToInt32(rd["rol_id"].ToString());
                    o_rolTemp.rol_descripcion = rd["rol_descripcion"].ToString();
                    o_usuario_temp.o_rol = o_rolTemp;

                    o_usuario.Add(o_usuario_temp);
                    // ,US.[dir_id]  ,US.[tel_id]

                }
                //}
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
