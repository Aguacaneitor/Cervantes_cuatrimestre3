using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class UsuarioLN
    {
        #region "PATRON SINGLETON"
        private static UsuarioLN lnUsuario = null;
        private UsuarioLN() { }
        public static UsuarioLN getInstance()
        {
            if (lnUsuario == null)
            {
                lnUsuario = new UsuarioLN();
            }
            return lnUsuario;
        }
        #endregion

        public Usuario AccesoSistema(String usuario, String contrasena)
        {
            try
            {
                return UsuarioAD.getInstance().AccesoSistema(usuario, contrasena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public void RegistroUsuario(Usuario o_usuario)
        {
            UsuarioAD.getInstance().RegistrarUsuarioAD(o_usuario);
        }

        public bool CambiarEstadoUsuario(String editor, String usuario, int cambio)
        {
            return UsuarioAD.getInstance().CambiarEstadoUsuario(editor, usuario, cambio);
        }

        public bool EditarUsuario(Usuario o_usuario)
        {
            return UsuarioAD.getInstance().EditarUsuario(o_usuario);
        }

        public Usuario ObtenerUsuario(String usuario)
        {
            try
            {
                return UsuarioAD.getInstance().ObtenerUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
