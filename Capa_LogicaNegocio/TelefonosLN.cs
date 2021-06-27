using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class TelefonosLN
    {
        #region "PATRON SINGLETON"
        private static TelefonosLN lnTelefonos = null;
        private TelefonosLN() { }
        public static TelefonosLN getInstance()
        {
            if (lnTelefonos == null)
            {
                lnTelefonos = new TelefonosLN();
            }
            return lnTelefonos;
        }
        #endregion

        public List<Telefono> ObtenerListaTelefonos(string usuario)
        {
            try
            {
                return TelefonosAD.getInstance().ObtenerListaTelefonos(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrarTelefono(Telefono o_telefono)
        {
            try
            {
                return TelefonosAD.getInstance().RegistrarTelefono(o_telefono);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditarTelefono(Telefono o_telefono)
        {
            try
            {
                return TelefonosAD.getInstance().EditarVerificandoTelegono(o_telefono);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public bool EliminarTelefono(Telefono o_telefono)
        {
            try
            {
                return TelefonosAD.getInstance().EliminarTelefono(o_telefono);
            }
            catch (Exception ex)
            {
                return false;
            }
        }        
    }
}
