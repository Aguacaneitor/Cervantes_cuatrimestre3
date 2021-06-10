using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class RolesListaLN
    {
        #region "PATRON SINGLETON"
        private static RolesListaLN lnRolesLista = null;
        private RolesListaLN() { }
        public static RolesListaLN getInstance()
        {
            if (lnRolesLista == null)
            {
                lnRolesLista = new RolesListaLN();
            }
            return lnRolesLista;
        }
        #endregion

        public List<string> ObtenerListaRoles()
        {
            try
            {
                return RolesListaAD.getInstance().ObtenerListaRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
