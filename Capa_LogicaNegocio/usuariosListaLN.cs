using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class usuariosListaLN
    {
        #region "PATRON SINGLETON"
        private static usuariosListaLN lnUsuariosLista = null;
        private usuariosListaLN() { }
        public static usuariosListaLN getInstance()
        {
            if (lnUsuariosLista == null)
            {
                lnUsuariosLista = new usuariosListaLN();
            }
            return lnUsuariosLista;
        }
        #endregion

        public List<Usuario> ObtenerListaUsuarios()
        {
            try
            {
                return UsuariosListaAD.getInstance().ObtenerListaUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
