using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_AccesoDatos
{
    public class Conexion
    {
        #region "PATRON SINGLETON"
        private static Conexion conexion = null;
        private Conexion() { }
        public static Conexion getInstance()
        {
            if (conexion == null)
            {
                conexion = new Conexion();
            }
            return conexion;
        }
        #endregion

        public SqlConnection ConexionBD()
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "Data Source=(LocalDb)\\localdbCervantes; Initial Catalog=Emp_Seguridad; Integrated Security = True";
            //conexion.ConnectionString = "Server = (LocalDb)\\localdbCervantes,Authentication = Windows Authentication, Database = Emp_Seguridad";
            return conexion;

        }

    }
}
