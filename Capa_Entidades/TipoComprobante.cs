using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class TipoComprobante
    {
        public int TC_id { get; set; }
        public String TC_nombre { get; set; }
        public int TC_signo { get; set; }

        public TipoComprobante() { }

        public TipoComprobante(int TC_id, String TC_nombre, int TC_signo)
        {
            this.TC_id = TC_id;
            this.TC_nombre = TC_nombre;
            this.TC_signo = TC_signo;
        }
    }
}
