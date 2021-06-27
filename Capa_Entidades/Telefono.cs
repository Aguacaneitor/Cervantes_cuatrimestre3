using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Telefono
    {
        public Int32 tel_id { get; set; }
        public string tel_tipo { get; set; }
        public string tel_nro { get; set; }
        public Int32 tel_prioridad { get; set; }
        public Usuario tel_usuario { get; set; }

        public Telefono() { }
    }
}
