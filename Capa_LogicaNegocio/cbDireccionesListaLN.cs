using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class cbDireccionesListaLN
    {
        #region "PATRON SINGLETON"
        private static cbDireccionesListaLN cbDireccionesLista = null;
        private cbDireccionesListaLN() { }
        public static cbDireccionesListaLN getInstance()
        {
            if (cbDireccionesLista == null)
            {
                cbDireccionesLista = new cbDireccionesListaLN();
            }
            return cbDireccionesLista;
        }
        #endregion

        public List<Barrio> ObtenerListaBarrios()
        {
            try
            {
                return cbDireccionesListaAD.getInstance().ObtenerListaBarrios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
