using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Comprobante
    {
        public int comp_id { get; set; }
        public DateTime comp_fecha { get; set; }
        public String comp_letra { get; set; }
        public int comp_suc { get; set; }
        public int comp_numero { get; set; }
        public float comp_neto { get; set; }
        public float comp_iva { get; set; }
        public float comp_total { get; set; }
        public Usuario com_Usuario { get; set; }
        public TipoComprobante com_TipoComprobante { get; set; }
        public Producto com_Producto { get; set; }
        public int com_Pagado { get; set; }
        public String comp_total_formateado { get; set; }
        public String comp_fecha_formateado { get; set; }
        public int comp_id_pago { get; set; }

        public Comprobante() { }

    }
}
