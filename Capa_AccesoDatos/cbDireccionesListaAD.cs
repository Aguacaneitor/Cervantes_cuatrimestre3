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

        public List<string> ObtenerListaLocalidades()
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            List<string> localidades = new List<string>();
            SqlDataReader rd = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListaLocalidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                rd = cmd.ExecuteReader();
                //if (rd.Read())
                //{
                while (rd.Read())
                {
                    var myString = rd.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                                                    // Do somthing with this rows string, for example to put them in to a list
                    localidades.Add(myString);
                }
                //}
            }
            catch (Exception ex)
            {
                localidades = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return localidades;

        }
    }
}
