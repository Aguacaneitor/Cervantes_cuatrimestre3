using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class DireccionesLN
    {
        #region "PATRON SINGLETON"
        private static DireccionesLN lnDireccionesLista = null;
        private DireccionesLN() { }
        public static DireccionesLN getInstance()
        {
            if (lnDireccionesLista == null)
            {
                lnDireccionesLista = new DireccionesLN();
            }
            return lnDireccionesLista;
        }
        #endregion

        public List<Direccion> ObtenerListaDirecciones(string usuario)
        {
            try
            {
                return DireccionesAD.getInstance().ObtenerListaDirecciones(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrarDireccion(Direccion o_direccion)
        {
            try
            {
                return DireccionesAD.getInstance().RegistrarDireccion(o_direccion);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ActualizarDireccion(Direccion o_direccion)
        {
            try
            {
                return DireccionesAD.getInstance().ActualizarDireccion(o_direccion);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarDireccion(int dir_id)
        {
            try
            {
                return DireccionesAD.getInstance().EliminarDireccion(dir_id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }        
    }
}
