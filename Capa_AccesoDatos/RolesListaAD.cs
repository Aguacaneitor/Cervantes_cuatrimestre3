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
    public class RolesListaAD
    {
        #region "PATRON SINGLETON"
        private static RolesListaAD adRolesLista = null;
        private RolesListaAD() { }
        public static RolesListaAD getInstance()
        {
            if (adRolesLista == null)
            {
                adRolesLista = new RolesListaAD();
            }
            return adRolesLista;
        }
        #endregion

        public List<string> ObtenerListaRoles()
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<string> roles = new List<string>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaRoles", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    var myString = rd.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                                                    // Do somthing with this rows string, for example to put them in to a list
                    roles.Add(myString);
                }
                //}
            }
            catch (Exception ex)
            {
                roles = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return roles;

        }
    }
}
