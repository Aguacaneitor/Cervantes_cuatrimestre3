using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_AccesoDatos;

namespace Capa_LogicaNegocio
{
    public class PagosLN
    {
        #region "PATRON SINGLETON"
        private static PagosLN lnPagos = null;
        private PagosLN() { }
        public static PagosLN getInstance()
        {
            if (lnPagos == null)
            {
                lnPagos = new PagosLN();
            }
            return lnPagos;
        }
        #endregion

        public bool GenerarCuponesCobro()
        {
            PagosAD.getInstance().GenerarCuponesCobro();
            return true;
        }

        public List<Comprobante> ObtenerListaComprobantes(int usuario_ID, String tipo_Comprobante)
        {
            return PagosAD.getInstance().ObtenerListaComprobantes(usuario_ID, tipo_Comprobante);
        }

        public List<Comprobante> ObtenerListaComprobantesPagos(int usuario_ID, String tipo_Comprobante)
        {
            return PagosAD.getInstance().ObtenerListaComprobantesPagos(usuario_ID, tipo_Comprobante);
        }
        public bool RegistrarCupon(Comprobante o_comprobante)
        {
            return PagosAD.getInstance().RegistrarCupon(o_comprobante);
        }
        public bool EliminarCupon(int com_id)
        {
            return PagosAD.getInstance().EliminarCupon(com_id);
        }
    }
}
