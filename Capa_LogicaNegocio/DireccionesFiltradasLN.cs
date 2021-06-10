using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class DireccionesFiltradasLN
    {
        #region "PATRON SINGLETON"
        private static DireccionesFiltradasLN lnDireccionesLista = null;
        private DireccionesFiltradasLN() { }
        public static DireccionesFiltradasLN getInstance()
        {
            if (lnDireccionesLista == null)
            {
                lnDireccionesLista = new DireccionesFiltradasLN();
            }
            return lnDireccionesLista;
        }
        #endregion

        public List<Direccion> ObtenerListaDirecciones(string usuario)
        {
            try
            {
                return DireccionesFiltradasAD.getInstance().ObtenerListaDirecciones(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
